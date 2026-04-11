namespace Haitch.Results;

/// <summary>
/// Represents an error returned by an operation, with a machine-readable code,
/// a human-readable message, and a category indicating the kind of failure.
/// </summary>
/// <param name="Code">A short, stable identifier for this error (e.g. <c>"user.not_found"</c>).</param>
/// <param name="Message">A human-readable description of what went wrong.</param>
/// <param name="Type">The category of error. Defaults to <see cref="ErrorType.Failure"/>.</param>
public record Error(string Code, string Message, ErrorType Type = ErrorType.Failure)
{
    /// <summary>
    /// Optional contextual data attached to the error, such as the offending field name
    /// on a validation error or the resource identifier on a not-found error.
    /// </summary>
    public IReadOnlyDictionary<string, object?>? Metadata { get; init; }

    /// <summary>
    /// Creates a generic <see cref="Error"/> with <see cref="ErrorType.Failure"/>.
    /// Use this when no more specific category applies.
    /// </summary>
    /// <param name="code">A short, stable identifier for this error.</param>
    /// <param name="message">A human-readable description of what went wrong.</param>
    public static Error Failure(string code, string message)
        => new(code, message, ErrorType.Failure);

    /// <summary>
    /// Creates an <see cref="Error"/> representing invalid input or a violated rule,
    /// categorized as <see cref="ErrorType.Validation"/>.
    /// </summary>
    /// <param name="code">A short, stable identifier for this error.</param>
    /// <param name="message">A human-readable description of what went wrong.</param>
    public static Error Validation(string code, string message)
        => new(code, message, ErrorType.Validation);

    /// <summary>
    /// Creates an <see cref="Error"/> representing a missing resource,
    /// categorized as <see cref="ErrorType.NotFound"/>.
    /// </summary>
    /// <param name="code">A short, stable identifier for this error.</param>
    /// <param name="message">A human-readable description of what went wrong.</param>
    public static Error NotFound(string code, string message)
        => new(code, message, ErrorType.NotFound);

    /// <summary>
    /// Creates an <see cref="Error"/> representing a conflict with existing state,
    /// such as a duplicate or concurrency violation, categorized as <see cref="ErrorType.Conflict"/>.
    /// </summary>
    /// <param name="code">A short, stable identifier for this error.</param>
    /// <param name="message">A human-readable description of what went wrong.</param>
    public static Error Conflict(string code, string message)
        => new(code, message, ErrorType.Conflict);

    /// <summary>
    /// Creates an <see cref="Error"/> representing missing or invalid authentication,
    /// categorized as <see cref="ErrorType.Unauthorized"/>.
    /// </summary>
    /// <param name="code">A short, stable identifier for this error.</param>
    /// <param name="message">A human-readable description of what went wrong.</param>
    public static Error Unauthorized(string code, string message)
        => new(code, message, ErrorType.Unauthorized);

    /// <summary>
    /// Creates an <see cref="Error"/> representing an authenticated caller who lacks
    /// permission to perform the operation, categorized as <see cref="ErrorType.Forbidden"/>.
    /// </summary>
    /// <param name="code">A short, stable identifier for this error.</param>
    /// <param name="message">A human-readable description of what went wrong.</param>
    public static Error Forbidden(string code, string message)
        => new(code, message, ErrorType.Forbidden);

    /// <summary>
    /// Creates an <see cref="Error"/> representing an unexpected or unhandled failure,
    /// categorized as <see cref="ErrorType.Unexpected"/>. Typically used when bridging
    /// caught exceptions into the result pipeline.
    /// </summary>
    /// <param name="code">A short, stable identifier for this error.</param>
    /// <param name="message">A human-readable description of what went wrong.</param>
    public static Error Unexpected(string code, string message)
        => new(code, message, ErrorType.Unexpected);
}