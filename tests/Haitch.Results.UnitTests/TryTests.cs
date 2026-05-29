using Haitch.Results.TestHelpers;

namespace Haitch.Results.UnitTests;

public class TryTests
{
    [Test]
    public async Task Try_action_succeeds_when_no_exception()
    {
        var ran = false;

        var result = Result.Try(() => ran = true);

        await result.AssertSuccess();
        await Assert.That(ran).IsTrue();
    }

    [Test]
    public async Task Try_action_captures_exception_as_unexpected_error()
    {
        var result = Result.Try(() => throw new InvalidOperationException("boom"));

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error.Type).IsEqualTo(ErrorType.Unexpected);
        await Assert.That(result.Error.Code).IsEqualTo(nameof(InvalidOperationException));
        await Assert.That(result.Error.Message).IsEqualTo("boom");
    }

    [Test]
    public async Task Try_func_captures_value_on_success()
    {
        var result = Result.Try(() => 42);

        await result.AssertSuccess(42);
    }

    [Test]
    public async Task Try_func_captures_exception_as_failure()
    {
        var result = Result.Try<int>(() => throw new InvalidOperationException("boom"));

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error.Type).IsEqualTo(ErrorType.Unexpected);
    }

    [Test]
    public async Task Try_uses_custom_exception_converter()
    {
        var error = Error.Conflict("duplicate", "Already exists");

        var result = Result.Try<int>(() => throw new InvalidOperationException("boom"), _ => error);

        await result.AssertFailure(error);
    }

    [Test]
    public async Task TryAsync_action_succeeds_when_no_exception()
    {
        var result = await Result.TryAsync(() => Task.CompletedTask);

        await result.AssertSuccess();
    }

    [Test]
    public async Task TryAsync_action_captures_exception_as_failure()
    {
        var result = await Result.TryAsync(() => Task.FromException(new InvalidOperationException("boom")));

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error.Type).IsEqualTo(ErrorType.Unexpected);
    }

    [Test]
    public async Task TryAsync_func_captures_value_on_success()
    {
        var result = await Result.TryAsync(() => Task.FromResult(42));

        await result.AssertSuccess(42);
    }

    [Test]
    public async Task TryAsync_func_captures_exception_with_custom_converter()
    {
        var error = Error.Unexpected("custom", "mapped");

        var result = await Result.TryAsync<int>(
            () => Task.FromException<int>(new InvalidOperationException("boom")),
            _ => error);

        await result.AssertFailure(error);
    }

    [Test]
    public async Task Try_custom_error_captures_value_on_success()
    {
        var result = Result.Try(() => 42, ex => ex.Message);

        await result.AssertSuccess(42);
    }

    [Test]
    public async Task Try_custom_error_maps_exception()
    {
        var result = Result.Try<int, string>(
            () => throw new InvalidOperationException("boom"),
            ex => ex.Message);

        await result.AssertFailure("boom");
    }

    [Test]
    public async Task TryAsync_custom_error_maps_exception()
    {
        var result = await Result.TryAsync<int, string>(
            () => Task.FromException<int>(new InvalidOperationException("boom")),
            ex => ex.Message);

        await result.AssertFailure("boom");
    }
}
