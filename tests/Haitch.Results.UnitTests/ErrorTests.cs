namespace Haitch.Results.UnitTests;

public class ErrorTests
{
    [Test]
    public async Task Default_constructor_uses_failure_type()
    {
        var error = new Error("some.code", "Some message");

        await Assert.That(error.Type).IsEqualTo(ErrorType.Failure);
    }

    [Test]
    [Arguments(nameof(Error.Failure), ErrorType.Failure)]
    [Arguments(nameof(Error.Validation), ErrorType.Validation)]
    [Arguments(nameof(Error.NotFound), ErrorType.NotFound)]
    [Arguments(nameof(Error.Conflict), ErrorType.Conflict)]
    [Arguments(nameof(Error.Unauthorized), ErrorType.Unauthorized)]
    [Arguments(nameof(Error.Forbidden), ErrorType.Forbidden)]
    [Arguments(nameof(Error.Unexpected), ErrorType.Unexpected)]
    public async Task Factory_methods_set_expected_type(string factoryName, ErrorType expectedType)
    {
        var factory = typeof(Error).GetMethod(factoryName, [typeof(string), typeof(string)])!;
        var error = (Error)factory.Invoke(null, ["code", "message"])!;

        await Assert.That(error.Code).IsEqualTo("code");
        await Assert.That(error.Message).IsEqualTo("message");
        await Assert.That(error.Type).IsEqualTo(expectedType);
    }

    [Test]
    public async Task Errors_with_same_values_are_equal()
    {
        var a = Error.Validation("required", "Field is required");
        var b = Error.Validation("required", "Field is required");

        await Assert.That(a).IsEqualTo(b);
        await Assert.That(a.GetHashCode()).IsEqualTo(b.GetHashCode());
    }

    [Test]
    public async Task Errors_with_different_types_are_not_equal()
    {
        var validation = Error.Validation("conflict", "Conflict occurred");
        var conflict = Error.Conflict("conflict", "Conflict occurred");

        await Assert.That(validation).IsNotEqualTo(conflict);
    }

    [Test]
    public async Task Metadata_defaults_to_null()
    {
        var error = Error.Failure("code", "message");

        await Assert.That(error.Metadata).IsNull();
    }

    [Test]
    public async Task Metadata_can_be_attached_via_with_expression()
    {
        var error = Error.Validation("required", "Email is required") with
        {
            Metadata = new Dictionary<string, object?> { ["field"] = "Email" }
        };

        await Assert.That(error.Metadata).IsNotNull();
        await Assert.That(error.Metadata!["field"]).IsEqualTo("Email");
    }
}