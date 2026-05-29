namespace Haitch.Results;

/// <summary>
/// Represents an error returned by an operation, with a machine-readable code,
/// a human-readable message, and a category indicating the kind of failure.
/// </summary>
/// <param name="Code">A short, stable identifier for this error (e.g. <c>"user.not_found"</c>).</param>
/// <param name="Message">A human-readable description of what went wrong.</param>
/// <param name="Type">The category of error. Defaults to <see cref="ErrorType.Failure"/>.</param>
public readonly record struct Error(string Code, string Message, ErrorType Type = ErrorType.Failure)
{
    /// <summary>
    /// The default <see cref="Code"/> used for an aggregate error when no override is supplied.
    /// </summary>
    public const string DefaultAggregateCode = "general.aggregate";

    /// <summary>
    /// The default <see cref="Message"/> used for an aggregate error when no override is supplied.
    /// </summary>
    public const string DefaultAggregateMessage = "One or more errors occurred.";

    /// <summary>
    /// Optional field holding all the child errors occurred along with the error.
    /// This effectively makes this <see cref="Error"/> object an aggregate of errors.
    /// </summary>
    public Error[]? ChildErrors { get; init; }
    
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
    /// <param name="childErrors">the child errors that have ocurred.</param>
    public static Error Validation(string code, string message, Error[] childErrors)
        => new(code, message, ErrorType.Validation) { ChildErrors =  childErrors };

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

    /// <summary>
    /// Creates an aggregate <see cref="Error"/> wrapping the supplied <paramref name="childErrors"/>,
    /// categorized as <see cref="ErrorType.Aggregate"/>, using the default
    /// <see cref="DefaultAggregateCode"/> and <see cref="DefaultAggregateMessage"/>.
    /// </summary>
    /// <param name="childErrors">The underlying errors to aggregate.</param>
    public static Error Aggregate(Error[] childErrors)
        => new(DefaultAggregateCode, DefaultAggregateMessage, ErrorType.Aggregate) { ChildErrors = childErrors };

    /// <summary>
    /// Creates an aggregate <see cref="Error"/> wrapping the supplied <paramref name="childErrors"/>,
    /// categorized as <see cref="ErrorType.Aggregate"/>, with a custom top-level code and message.
    /// </summary>
    /// <param name="code">A short, stable identifier for the aggregate error.</param>
    /// <param name="message">A human-readable description of the aggregate failure.</param>
    /// <param name="childErrors">The underlying errors to aggregate.</param>
    public static Error Aggregate(string code, string message, Error[] childErrors)
        => new(code, message, ErrorType.Aggregate) { ChildErrors = childErrors };

    /// <inheritdoc/>
    public bool Equals(Error other)
    {
        if (Code != other.Code || Message != other.Message || Type != other.Type)
        {
            return false;
        }

        return ArraysEqual(ChildErrors, other.ChildErrors) &&
               DictionariesEqual(Metadata, other.Metadata);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(Code);
        hash.Add(Message);
        hash.Add(Type);
        
        if (ChildErrors is not null)
        {
            foreach (var error in ChildErrors)
            {
                hash.Add(error);
            }
        }

        if (Metadata is null)
        {
            return hash.ToHashCode();
        }

        foreach (var kvp in Metadata)
        {
            hash.Add(kvp.Key);
            hash.Add(kvp.Value);
        }

        return hash.ToHashCode();
    }
    
    private static bool ArraysEqual(Error[]? a, Error[]? b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.SequenceEqual(b);
    }

    private static bool DictionariesEqual(
        IReadOnlyDictionary<string, object?>? a,
        IReadOnlyDictionary<string, object?>? b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        if (a.Count != b.Count)
        {
            return false;
        }

        foreach (var kvp in a)
        {
            if (!b.TryGetValue(kvp.Key, out var value) || !Equals(kvp.Value, value))
            {
                return false;
            }
        }

        return true;
    }
}