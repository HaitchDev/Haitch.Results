namespace Haitch.Results.UnitTests;

public class ResultT2Tests
{
    [Test]
    public async Task Success_creates_a_successful_result()
    {
        var result = Result<int, string>.Success(42);

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.IsFailure).IsFalse();
        await Assert.That(result.Value).IsEqualTo(42);
    }

    [Test]
    public async Task Failure_creates_a_failed_result()
    {
        var result = Result<int, string>.Failure("oops");

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.IsSuccess).IsFalse();
        await Assert.That(result.Error).IsEqualTo("oops");
    }

    [Test]
    public async Task Default_constructed_result_is_a_failure()
    {
        var result = default(Result<int, string>);

        await Assert.That(result.IsSuccess).IsFalse();
    }

    [Test]
    public async Task Value_implicitly_converts_to_success_result()
    {
        Result<int, string> result = 42;

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.Value).IsEqualTo(42);
    }

    [Test]
    public async Task Error_implicitly_converts_to_failure_result()
    {
        Result<int, string> result = "oops";

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error).IsEqualTo("oops");
    }

    [Test]
    public async Task Accessing_value_on_failure_throws()
    {
        var result = Result<int, string>.Failure("oops");

        await Assert.That(() => result.Value).Throws<InvalidOperationException>();
    }

    [Test]
    public async Task Accessing_error_on_success_throws()
    {
        var result = Result<int, string>.Success(42);

        await Assert.That(() => result.Error).Throws<InvalidOperationException>();
    }

    [Test]
    public async Task Match_invokes_success_branch_when_successful()
    {
        var result = Result<int, string>.Success(42);

        var output = result.Match(
            onSuccess: v => $"value:{v}",
            onFailure: e => $"error:{e}");

        await Assert.That(output).IsEqualTo("value:42");
    }

    [Test]
    public async Task Match_invokes_failure_branch_when_failed()
    {
        var result = Result<int, string>.Failure("oops");

        var output = result.Match(
            onSuccess: v => $"value:{v}",
            onFailure: e => $"error:{e}");

        await Assert.That(output).IsEqualTo("error:oops");
    }
    
    [Test]
    public async Task Map_transforms_value_on_success()
    {
        var result = Result<int, string>.Success(42);

        var mapped = result.Map(v => v.ToString());

        await Assert.That(mapped.IsSuccess).IsTrue();
        await Assert.That(mapped.Value).IsEqualTo("42");
    }

    [Test]
    public async Task Map_propagates_error_on_failure()
    {
        var result = Result<int, string>.Failure("oops");

        var mapped = result.Map(v => v.ToString());

        await Assert.That(mapped.IsFailure).IsTrue();
        await Assert.That(mapped.Error).IsEqualTo("oops");
    }

    [Test]
    public async Task Map_does_not_invoke_mapper_on_failure()
    {
        var result = Result<int, string>.Failure("oops");
        var invoked = false;

        result.Map(v => { invoked = true; return v.ToString(); });

        await Assert.That(invoked).IsFalse();
    }

    [Test]
    public async Task Successes_with_same_value_are_equal()
    {
        var a = Result<int, string>.Success(42);
        var b = Result<int, string>.Success(42);

        await Assert.That(a).IsEqualTo(b);
        await Assert.That(a == b).IsTrue();
        await Assert.That(a.GetHashCode()).IsEqualTo(b.GetHashCode());
    }

    [Test]
    public async Task Failures_with_same_error_are_equal()
    {
        var a = Result<int, string>.Failure("oops");
        var b = Result<int, string>.Failure("oops");

        await Assert.That(a).IsEqualTo(b);
        await Assert.That(a == b).IsTrue();
        await Assert.That(a.GetHashCode()).IsEqualTo(b.GetHashCode());
    }

    [Test]
    public async Task Successes_with_different_values_are_not_equal()
    {
        var a = Result<int, string>.Success(42);
        var b = Result<int, string>.Success(43);

        await Assert.That(a).IsNotEqualTo(b);
        await Assert.That(a != b).IsTrue();
    }

    [Test]
    public async Task Success_and_failure_are_never_equal()
    {
        var success = Result<int, string>.Success(42);
        var failure = Result<int, string>.Failure("oops");

        await Assert.That(success).IsNotEqualTo(failure);
    }

    [Test]
    public async Task Works_with_reference_type_value()
    {
        var user = new User("Test");
        var result = Result<User, Error>.Success(user);

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.Value).IsEqualTo(user);
    }

    [Test]
    public async Task Works_with_haitch_error_as_terror()
    {
        var error = Error.NotFound("user.not_found", "User not found");
        Result<User, Error> result = error;

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error).IsEqualTo(error);
    }

    private sealed record User(string Name);
}