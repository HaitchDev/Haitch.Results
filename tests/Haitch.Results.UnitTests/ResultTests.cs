namespace Haitch.Results.UnitTests;

public class ResultTests
{
    [Test]
    public async Task Success_creates_a_successful_result()
    {
        var result = Result.Success();

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.IsFailure).IsFalse();
    }

    [Test]
    public async Task Failure_creates_a_failed_result()
    {
        var error = Error.Failure("oops", "Something went wrong");
        var result = Result.Failure(error);

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.IsSuccess).IsFalse();
        await Assert.That(result.Error).IsEqualTo(error);
    }

    [Test]
    public async Task Default_constructed_result_is_a_failure()
    {
        var result = default(Result);

        await Assert.That(result.IsSuccess).IsFalse();
    }

    [Test]
    public async Task Error_implicitly_converts_to_failure_result()
    {
        var error = Error.Failure("oops", "Something went wrong");
        Result result = error;

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error).IsEqualTo(error);
    }

    [Test]
    public async Task Accessing_error_on_success_throws()
    {
        var result = Result.Success();

        await Assert.That(() => result.Error).Throws<InvalidOperationException>();
    }

    [Test]
    public async Task Match_invokes_success_branch_when_successful()
    {
        var result = Result.Success();

        var output = result.Match(
            onSuccess: () => "ok",
            onFailure: e => $"error:{e.Code}");

        await Assert.That(output).IsEqualTo("ok");
    }

    [Test]
    public async Task Match_invokes_failure_branch_when_failed()
    {
        var result = Result.Failure(Error.Failure("oops", "Something went wrong"));

        var output = result.Match(
            onSuccess: () => "ok",
            onFailure: e => $"error:{e.Code}");

        await Assert.That(output).IsEqualTo("error:oops");
    }

    [Test]
    public async Task Successes_are_equal()
    {
        var a = Result.Success();
        var b = Result.Success();

        await Assert.That(a).IsEqualTo(b);
        await Assert.That(a == b).IsTrue();
        await Assert.That(a.GetHashCode()).IsEqualTo(b.GetHashCode());
    }

    [Test]
    public async Task Failures_with_same_error_are_equal()
    {
        var a = Result.Failure(Error.Failure("oops", "Something went wrong"));
        var b = Result.Failure(Error.Failure("oops", "Something went wrong"));

        await Assert.That(a).IsEqualTo(b);
        await Assert.That(a == b).IsTrue();
        await Assert.That(a.GetHashCode()).IsEqualTo(b.GetHashCode());
    }

    [Test]
    public async Task Success_and_failure_are_never_equal()
    {
        var success = Result.Success();
        var failure = Result.Failure(Error.Failure("oops", "Something went wrong"));

        await Assert.That(success).IsNotEqualTo(failure);
    }

    [Test]
    [Arguments(nameof(Result.Fail), ErrorType.Failure)]
    [Arguments(nameof(Result.Validation), ErrorType.Validation)]
    [Arguments(nameof(Result.NotFound), ErrorType.NotFound)]
    [Arguments(nameof(Result.Conflict), ErrorType.Conflict)]
    [Arguments(nameof(Result.Unauthorized), ErrorType.Unauthorized)]
    [Arguments(nameof(Result.Forbidden), ErrorType.Forbidden)]
    [Arguments(nameof(Result.Unexpected), ErrorType.Unexpected)]
    public async Task Convenience_factories_create_failure_with_expected_error_type(
        string factoryName, ErrorType expectedType)
    {
        var factory = typeof(Result).GetMethod(
            factoryName,
            [typeof(string), typeof(string)])!;
        var result = (Result)factory.Invoke(null, ["code", "message"])!;

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error.Code).IsEqualTo("code");
        await Assert.That(result.Error.Message).IsEqualTo("message");
        await Assert.That(result.Error.Type).IsEqualTo(expectedType);
    }
}