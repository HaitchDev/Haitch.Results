using Haitch.Results.TestHelpers;

namespace Haitch.Results.UnitTests;

public class CollectTests
{
    [Test]
    public async Task Collect_succeeds_with_all_values_in_order()
    {
        var results = new[] { Result<int>.Success(1), Result<int>.Success(2), Result<int>.Success(3) };

        var collected = Result.Collect(results);

        await Assert.That(collected.IsSuccess).IsTrue();
        await Assert.That(collected.Value).IsEquivalentTo([1, 2, 3]);
    }

    [Test]
    public async Task Collect_of_empty_sequence_succeeds_with_empty_array()
    {
        var collected = Result.Collect(Array.Empty<Result<int>>());

        await Assert.That(collected.IsSuccess).IsTrue();
        await Assert.That(collected.Value).IsEmpty();
    }

    [Test]
    public async Task Collect_returns_single_failure_unchanged()
    {
        var error = Error.NotFound("missing", "Missing");
        var results = new[] { Result<int>.Success(1), Result<int>.Failure(error) };

        var collected = Result.Collect(results);

        await collected.AssertFailure(error);
    }

    [Test]
    public async Task Collect_aggregates_multiple_failures()
    {
        var first = Error.Failure("a", "first");
        var second = Error.Failure("b", "second");
        var results = new[]
        {
            Result<int>.Failure(first),
            Result<int>.Success(2),
            Result<int>.Failure(second),
        };

        var collected = Result.Collect(results);

        await Assert.That(collected.IsFailure).IsTrue();
        await Assert.That(collected.Error.Type).IsEqualTo(ErrorType.Aggregate);
        await Assert.That(collected.Error.Code).IsEqualTo(Error.DefaultAggregateCode);
        await Assert.That(collected.Error.ChildErrors).IsEquivalentTo([first, second]);
    }

    [Test]
    public async Task Collect_uses_supplied_code_and_message()
    {
        var results = new[]
        {
            Result<int>.Failure(Error.Failure("a", "first")),
            Result<int>.Failure(Error.Failure("b", "second")),
        };

        var collected = Result.Collect(results, code: "batch.invalid", message: "Batch failed.");

        await Assert.That(collected.Error.Code).IsEqualTo("batch.invalid");
        await Assert.That(collected.Error.Message).IsEqualTo("Batch failed.");
    }

    [Test]
    public async Task Collect_valueless_succeeds_when_all_succeed()
    {
        var collected = Result.Collect(new[] { Result.Success(), Result.Success() });

        await collected.AssertSuccess();
    }

    [Test]
    public async Task Collect_valueless_aggregates_multiple_failures()
    {
        var first = Error.Failure("a", "first");
        var second = Error.Failure("b", "second");

        var collected = Result.Collect(new[] { Result.Failure(first), Result.Failure(second) });

        await Assert.That(collected.Error.Type).IsEqualTo(ErrorType.Aggregate);
        await Assert.That(collected.Error.ChildErrors).IsEquivalentTo([first, second]);
    }

    [Test]
    public async Task Collect_custom_error_succeeds_with_all_values()
    {
        var results = new[] { Result<int, string>.Success(1), Result<int, string>.Success(2) };

        var collected = Result.Collect(results, errors => string.Join(", ", errors));

        await Assert.That(collected.IsSuccess).IsTrue();
        await Assert.That(collected.Value).IsEquivalentTo([1, 2]);
    }

    [Test]
    public async Task Collect_custom_error_invokes_aggregator_with_failures()
    {
        var results = new[]
        {
            Result<int, string>.Failure("first"),
            Result<int, string>.Success(2),
            Result<int, string>.Failure("second"),
        };

        var collected = Result.Collect(results, errors => string.Join(", ", errors));

        await collected.AssertFailure("first, second");
    }
}
