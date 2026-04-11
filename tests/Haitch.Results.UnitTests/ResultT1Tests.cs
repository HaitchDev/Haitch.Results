namespace Haitch.Results.UnitTests;

public class ResultT1Tests
{
    [Test]
    public async Task Success_creates_a_successful_result()
    {
        var result = Result<int>.Success(42);

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.IsFailure).IsFalse();
        await Assert.That(result.Value).IsEqualTo(42);
    }

    [Test]
    public async Task Failure_creates_a_failed_result()
    {
        var error = Error.Failure("oops", "Something went wrong");
        var result = Result<int>.Failure(error);

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.IsSuccess).IsFalse();
        await Assert.That(result.Error).IsEqualTo(error);
    }

    [Test]
    public async Task Default_constructed_result_is_a_failure()
    {
        var result = default(Result<int>);

        await Assert.That(result.IsSuccess).IsFalse();
    }

    [Test]
    public async Task Value_implicitly_converts_to_success_result()
    {
        Result<int> result = 42;

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.Value).IsEqualTo(42);
    }

    [Test]
    public async Task Error_implicitly_converts_to_failure_result()
    {
        var error = Error.Failure("oops", "Something went wrong");
        Result<int> result = error;

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error).IsEqualTo(error);
    }

    [Test]
    public async Task Accessing_value_on_failure_throws()
    {
        var result = Result<int>.Failure(Error.Failure("oops", "Something went wrong"));

        await Assert.That(() => result.Value).Throws<InvalidOperationException>();
    }

    [Test]
    public async Task Accessing_error_on_success_throws()
    {
        var result = Result<int>.Success(42);

        await Assert.That(() => result.Error).Throws<InvalidOperationException>();
    }

    [Test]
    public async Task Match_invokes_success_branch_when_successful()
    {
        var result = Result<int>.Success(42);

        var output = result.Match(
            onSuccess: v => $"value:{v}",
            onFailure: e => $"error:{e.Code}");

        await Assert.That(output).IsEqualTo("value:42");
    }

    [Test]
    public async Task Match_invokes_failure_branch_when_failed()
    {
        var result = Result<int>.Failure(Error.Failure("oops", "Something went wrong"));

        var output = result.Match(
            onSuccess: v => $"value:{v}",
            onFailure: e => $"error:{e.Code}");

        await Assert.That(output).IsEqualTo("error:oops");
    }
    
    [Test]
    public async Task Map_transforms_value_on_success()
    {
        var result = Result<int>.Success(42);

        var mapped = result.Map(v => v.ToString());

        await Assert.That(mapped.IsSuccess).IsTrue();
        await Assert.That(mapped.Value).IsEqualTo("42");
    }

    [Test]
    public async Task Map_propagates_error_on_failure()
    {
        var error = Error.NotFound("user.not_found", "User not found");
        var result = Result<int>.Failure(error);

        var mapped = result.Map(v => v.ToString());

        await Assert.That(mapped.IsFailure).IsTrue();
        await Assert.That(mapped.Error).IsEqualTo(error);
    }

    [Test]
    public async Task Map_does_not_invoke_mapper_on_failure()
    {
        var result = Result<int>.Failure(Error.Failure("oops", "Something went wrong"));
        var invoked = false;

        result.Map(v => { invoked = true; return v.ToString(); });

        await Assert.That(invoked).IsFalse();
    }

    [Test]
    public async Task MapError_transforms_error_on_failure()
    {
        var original = Error.NotFound("user.not_found", "User not found");
        var result = Result<int>.Failure(original);

        var mapped = result.MapError(e => Error.Validation("validation." + e.Code, e.Message));

        await Assert.That(mapped.IsFailure).IsTrue();
        await Assert.That(mapped.Error.Code).IsEqualTo("validation.user.not_found");
        await Assert.That(mapped.Error.Type).IsEqualTo(ErrorType.Validation);
    }

    [Test]
    public async Task MapError_propagates_value_on_success()
    {
        var result = Result<int>.Success(42);

        var mapped = result.MapError(_ => Error.Validation("x", "x"));

        await Assert.That(mapped.IsSuccess).IsTrue();
        await Assert.That(mapped.Value).IsEqualTo(42);
    }

    [Test]
    public async Task MapError_does_not_invoke_mapper_on_success()
    {
        var result = Result<int>.Success(42);
        var invoked = false;

        result.MapError(e => { invoked = true; return e; });

        await Assert.That(invoked).IsFalse();
    }

    [Test]
    public async Task Successes_with_same_value_are_equal()
    {
        var a = Result<int>.Success(42);
        var b = Result<int>.Success(42);

        await Assert.That(a).IsEqualTo(b);
        await Assert.That(a == b).IsTrue();
        await Assert.That(a.GetHashCode()).IsEqualTo(b.GetHashCode());
    }

    [Test]
    public async Task Failures_with_same_error_are_equal()
    {
        var a = Result<int>.Failure(Error.Failure("oops", "Something went wrong"));
        var b = Result<int>.Failure(Error.Failure("oops", "Something went wrong"));

        await Assert.That(a).IsEqualTo(b);
        await Assert.That(a == b).IsTrue();
        await Assert.That(a.GetHashCode()).IsEqualTo(b.GetHashCode());
    }

    [Test]
    public async Task Successes_with_different_values_are_not_equal()
    {
        var a = Result<int>.Success(42);
        var b = Result<int>.Success(43);

        await Assert.That(a).IsNotEqualTo(b);
        await Assert.That(a != b).IsTrue();
    }

    [Test]
    public async Task Success_and_failure_are_never_equal()
    {
        var success = Result<int>.Success(42);
        var failure = Result<int>.Failure(Error.Failure("oops", "Something went wrong"));

        await Assert.That(success).IsNotEqualTo(failure);
    }

    [Test]
    public async Task Works_with_reference_type_value()
    {
        var user = new User("Test");
        var result = Result<User>.Success(user);

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.Value).IsEqualTo(user);
    }

    [Test]
    [Arguments(nameof(Result<>.Fail), ErrorType.Failure)]
    [Arguments(nameof(Result<>.Validation), ErrorType.Validation)]
    [Arguments(nameof(Result<>.NotFound), ErrorType.NotFound)]
    [Arguments(nameof(Result<>.Conflict), ErrorType.Conflict)]
    [Arguments(nameof(Result<>.Unauthorized), ErrorType.Unauthorized)]
    [Arguments(nameof(Result<>.Forbidden), ErrorType.Forbidden)]
    [Arguments(nameof(Result<>.Unexpected), ErrorType.Unexpected)]
    public async Task Convenience_factories_create_failure_with_expected_error_type(
        string factoryName, ErrorType expectedType)
    {
        var factory = typeof(Result<int>).GetMethod(
            factoryName,
            [typeof(string), typeof(string)])!;
        var result = (Result<int>)factory.Invoke(null, ["code", "message"])!;

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error.Code).IsEqualTo("code");
        await Assert.That(result.Error.Message).IsEqualTo("message");
        await Assert.That(result.Error.Type).IsEqualTo(expectedType);
    }

    private sealed record User(string Name);
}