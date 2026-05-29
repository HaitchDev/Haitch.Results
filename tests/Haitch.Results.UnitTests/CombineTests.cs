using Haitch.Results.TestHelpers;

namespace Haitch.Results.UnitTests;

public class CombineTests
{
    [Test]
    public async Task Combine_valueless_succeeds_when_all_succeed()
    {
        var combined = Result.Combine(Result.Success(), Result.Success(), Result.Success());

        await combined.AssertSuccess();
    }

    [Test]
    public async Task Combine_valueless_returns_single_failure_unchanged()
    {
        var error = Error.NotFound("user.not_found", "User not found");

        var combined = Result.Combine(Result.Success(), Result.Failure(error));

        await combined.AssertFailure(error);
    }

    [Test]
    public async Task Combine_valueless_aggregates_multiple_failures()
    {
        var first = Error.Failure("a", "first");
        var second = Error.Failure("b", "second");

        var combined = Result.Combine(Result.Failure(first), Result.Success(), Result.Failure(second));

        await Assert.That(combined.IsFailure).IsTrue();
        await Assert.That(combined.Error.Type).IsEqualTo(ErrorType.Aggregate);
        await Assert.That(combined.Error.Code).IsEqualTo(Error.DefaultAggregateCode);
        await Assert.That(combined.Error.Message).IsEqualTo(Error.DefaultAggregateMessage);
        await Assert.That(combined.Error.ChildErrors).IsEquivalentTo([first, second]);
    }

    [Test]
    public async Task Combine_valueless_uses_supplied_code_and_message_for_aggregate()
    {
        var combined = Result.Combine(
            Result.Failure(Error.Failure("a", "first")),
            Result.Failure(Error.Failure("b", "second")),
            code: "form.invalid",
            message: "The form is invalid.");

        await Assert.That(combined.Error.Code).IsEqualTo("form.invalid");
        await Assert.That(combined.Error.Message).IsEqualTo("The form is invalid.");
    }

    [Test]
    public async Task Combine_valueless_supports_eight_results()
    {
        var combined = Result.Combine(
            Result.Success(),
            Result.Success(),
            Result.Success(),
            Result.Success(),
            Result.Success(),
            Result.Success(),
            Result.Success(),
            Result.Success());

        await combined.AssertSuccess();
    }

    [Test]
    public async Task Combine_tuple_succeeds_with_all_values()
    {
        var combined = Result.Combine(Result<int>.Success(1), Result<string>.Success("two"));

        await combined.AssertSuccess((1, "two"));
    }

    [Test]
    public async Task Combine_tuple_returns_single_failure_unchanged()
    {
        var error = Error.NotFound("missing", "Missing");

        var combined = Result.Combine(Result<int>.Success(1), Result<string>.Failure(error));

        await combined.AssertFailure(error);
    }

    [Test]
    public async Task Combine_tuple_aggregates_multiple_failures()
    {
        var first = Error.Failure("a", "first");
        var second = Error.Failure("b", "second");

        var combined = Result.Combine(Result<int>.Failure(first), Result<string>.Failure(second));

        await Assert.That(combined.IsFailure).IsTrue();
        await Assert.That(combined.Error.Type).IsEqualTo(ErrorType.Aggregate);
        await Assert.That(combined.Error.ChildErrors).IsEquivalentTo([first, second]);
    }

    [Test]
    public async Task Combine_tuple_uses_supplied_code_and_message()
    {
        var combined = Result.Combine(
            Result<int>.Failure(Error.Failure("a", "first")),
            Result<string>.Failure(Error.Failure("b", "second")),
            code: "custom",
            message: "Custom message");

        await Assert.That(combined.Error.Code).IsEqualTo("custom");
        await Assert.That(combined.Error.Message).IsEqualTo("Custom message");
    }

    [Test]
    public async Task Combine_tuple_supports_eight_results()
    {
        var combined = Result.Combine(
            Result<int>.Success(1),
            Result<int>.Success(2),
            Result<int>.Success(3),
            Result<int>.Success(4),
            Result<int>.Success(5),
            Result<int>.Success(6),
            Result<int>.Success(7),
            Result<int>.Success(8));

        await combined.AssertSuccess((1, 2, 3, 4, 5, 6, 7, 8));
    }

    [Test]
    public async Task Combine_custom_error_succeeds_with_all_values()
    {
        var combined = Result.Combine(
            Result<int, string>.Success(1),
            Result<bool, string>.Success(true),
            errors => string.Join(", ", errors));

        await combined.AssertSuccess((1, true));
    }

    [Test]
    public async Task Combine_custom_error_invokes_aggregator_with_all_failures()
    {
        var combined = Result.Combine(
            Result<int, string>.Failure("first"),
            Result<bool, string>.Failure("second"),
            errors => string.Join(", ", errors));

        await combined.AssertFailure("first, second");
    }

    [Test]
    public async Task Combine_custom_error_aggregator_receives_only_failures()
    {
        var combined = Result.Combine(
            Result<int, string>.Success(1),
            Result<bool, string>.Failure("only"),
            errors => $"count:{errors.Count}|{string.Join(",", errors)}");

        await combined.AssertFailure("count:1|only");
    }
}
