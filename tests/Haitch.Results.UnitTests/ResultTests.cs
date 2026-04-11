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
    public async Task Map_produces_value_on_success()
    {
        var result = Result.Success();

        var mapped = result.Map(() => 42);

        await Assert.That(mapped.IsSuccess).IsTrue();
        await Assert.That(mapped.Value).IsEqualTo(42);
    }

    [Test]
    public async Task Map_propagates_error_on_failure()
    {
        var error = Error.NotFound("user.not_found", "User not found");
        var result = Result.Failure(error);

        var mapped = result.Map(() => 42);

        await Assert.That(mapped.IsFailure).IsTrue();
        await Assert.That(mapped.Error).IsEqualTo(error);
    }

    [Test]
    public async Task Map_does_not_invoke_mapper_on_failure()
    {
        var result = Result.Failure(Error.Failure("oops", "Something went wrong"));
        var invoked = false;

        result.Map(() =>
        {
            invoked = true;
            return 42;
        });

        await Assert.That(invoked).IsFalse();
    }

    [Test]
    public async Task MapError_transforms_error_on_failure()
    {
        var original = Error.NotFound("user.not_found", "User not found");
        var result = Result.Failure(original);

        var mapped = result.MapError(e => Error.Validation("validation." + e.Code, e.Message));

        await Assert.That(mapped.IsFailure).IsTrue();
        await Assert.That(mapped.Error.Code).IsEqualTo("validation.user.not_found");
        await Assert.That(mapped.Error.Type).IsEqualTo(ErrorType.Validation);
    }

    [Test]
    public async Task MapError_propagates_success_unchanged()
    {
        var result = Result.Success();

        var mapped = result.MapError(_ => Error.Validation("x", "x"));

        await Assert.That(mapped.IsSuccess).IsTrue();
    }

    [Test]
    public async Task MapError_does_not_invoke_mapper_on_success()
    {
        var result = Result.Success();
        var invoked = false;

        result.MapError(e =>
        {
            invoked = true;
            return e;
        });

        await Assert.That(invoked).IsFalse();
    }

    [Test]
    public async Task Bind_to_void_result_chains_when_successful()
    {
        var result = Result.Success();
        var invoked = false;

        var bound = result.Bind(() =>
        {
            invoked = true;
            return Result.Success();
        });

        await Assert.That(invoked).IsTrue();
        await Assert.That(bound.IsSuccess).IsTrue();
    }

    [Test]
    public async Task Bind_to_void_result_propagates_error_from_outer_result()
    {
        var error = Error.Failure("oops", "Something went wrong");
        var result = Result.Failure(error);

        var bound = result.Bind(Result.Success);

        await Assert.That(bound.IsFailure).IsTrue();
        await Assert.That(bound.Error).IsEqualTo(error);
    }

    [Test]
    public async Task Bind_to_void_result_propagates_error_from_inner_result()
    {
        var innerError = Error.Validation("invalid", "Invalid value");
        var result = Result.Success();

        var bound = result.Bind(() => Result.Failure(innerError));

        await Assert.That(bound.IsFailure).IsTrue();
        await Assert.That(bound.Error).IsEqualTo(innerError);
    }

    [Test]
    public async Task Bind_to_void_result_does_not_invoke_binder_on_failure()
    {
        var result = Result.Failure(Error.Failure("oops", "Something went wrong"));
        var invoked = false;

        result.Bind(() =>
        {
            invoked = true;
            return Result.Success();
        });

        await Assert.That(invoked).IsFalse();
    }

    [Test]
    public async Task Bind_to_value_result_widens_type_on_success()
    {
        var result = Result.Success();

        var bound = result.Bind(() => Result<int>.Success(42));

        await Assert.That(bound.IsSuccess).IsTrue();
        await Assert.That(bound.Value).IsEqualTo(42);
    }

    [Test]
    public async Task Bind_to_value_result_propagates_error_from_outer_result()
    {
        var error = Error.NotFound("user.not_found", "User not found");
        var result = Result.Failure(error);

        var bound = result.Bind(() => Result<int>.Success(42));

        await Assert.That(bound.IsFailure).IsTrue();
        await Assert.That(bound.Error).IsEqualTo(error);
    }

    [Test]
    public async Task Bind_to_value_result_propagates_error_from_inner_result()
    {
        var innerError = Error.Validation("invalid", "Invalid value");
        var result = Result.Success();

        var bound = result.Bind(() => Result<int>.Failure(innerError));

        await Assert.That(bound.IsFailure).IsTrue();
        await Assert.That(bound.Error).IsEqualTo(innerError);
    }

    [Test]
    public async Task Bind_to_value_result_does_not_invoke_binder_on_failure()
    {
        var result = Result.Failure(Error.Failure("oops", "Something went wrong"));
        var invoked = false;

        result.Bind(() =>
        {
            invoked = true;
            return Result<int>.Success(42);
        });

        await Assert.That(invoked).IsFalse();
    }

    [Test]
    public async Task Tap_invokes_action_on_success()
    {
        var result = Result.Success();
        var invoked = false;

        var tapped = result.Tap(() => invoked = true);

        await Assert.That(invoked).IsTrue();
        await Assert.That(tapped).IsEqualTo(result);
    }

    [Test]
    public async Task Tap_does_not_invoke_action_on_failure()
    {
        var result = Result.Failure(Error.Failure("oops", "Something went wrong"));
        var invoked = false;

        var tapped = result.Tap(() => invoked = true);

        await Assert.That(invoked).IsFalse();
        await Assert.That(tapped).IsEqualTo(result);
    }

    [Test]
    public async Task TapError_invokes_action_on_failure()
    {
        var error = Error.NotFound("user.not_found", "User not found");
        var result = Result.Failure(error);
        Error? captured = null;

        var tapped = result.TapError(e => captured = e);

        await Assert.That(captured).IsEqualTo(error);
        await Assert.That(tapped).IsEqualTo(result);
    }

    [Test]
    public async Task TapError_does_not_invoke_action_on_success()
    {
        var result = Result.Success();
        var invoked = false;

        var tapped = result.TapError(_ => invoked = true);

        await Assert.That(invoked).IsFalse();
        await Assert.That(tapped).IsEqualTo(result);
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