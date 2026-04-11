using System.Diagnostics.CodeAnalysis;

namespace Haitch.Results;

/// <summary>
/// Represents the outcome of an operation that either succeeds with no value
/// or fails with an <see cref="Haitch.Results.Error"/>.
/// </summary>
public readonly struct Result : IEquatable<Result>
{
    private readonly Error? _error;

    /// <summary>
    /// <see langword="true"/> if the operation succeeded; otherwise <see langword="false"/>.
    /// </summary>
    [MemberNotNullWhen(false, nameof(Error), nameof(_error))]
    public bool IsSuccess { get; }

    /// <summary>
    /// <see langword="true"/> if the operation failed; otherwise <see langword="false"/>.
    /// </summary>
    [MemberNotNullWhen(true, nameof(Error), nameof(_error))]
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// The failure error. Throws <see cref="InvalidOperationException"/> if the result is a success.
    /// </summary>
    public Error Error => IsFailure
        ? _error!
        : throw new InvalidOperationException("Cannot access Error on a successful result.");

    private Result(bool isSuccess, Error? error)
    {
        IsSuccess = isSuccess;
        _error = error;
    }

    /// <summary>
    /// Creates a successful result.
    /// </summary>
    public static Result Success() => new(true, null);

    /// <summary>
    /// Creates a failed result containing the specified error.
    /// </summary>
    public static Result Failure(Error error) => new(false, error);

    /// <summary>
    /// Implicitly converts an error into a failed <see cref="Result"/>.
    /// </summary>
    public static implicit operator Result(Error error) => Failure(error);

    /// <summary>
    /// Invokes <paramref name="onSuccess"/> if the result is successful, otherwise <paramref name="onFailure"/>,
    /// and returns the result of the invoked delegate.
    /// </summary>
    public TOut Match<TOut>(Func<TOut> onSuccess, Func<Error, TOut> onFailure)
        => IsSuccess ? onSuccess() : onFailure(_error!);
    
    /// <summary>
    /// Produces a value using <paramref name="mapper"/> if the result is successful;
    /// otherwise propagates the existing error unchanged.
    /// </summary>
    /// <typeparam name="TOut">The type of the value to produce on success.</typeparam>
    /// <param name="mapper">A function that produces the value to wrap in the result.</param>
    public Result<TOut> Map<TOut>(Func<TOut> mapper)
        => IsSuccess
            ? Result<TOut>.Success(mapper())
            : Result<TOut>.Failure(_error!);

    /// <inheritdoc />
    public bool Equals(Result other)
        => IsSuccess == other.IsSuccess
           && EqualityComparer<Error?>.Default.Equals(_error, other._error);

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is Result other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode() => HashCode.Combine(IsSuccess, _error);

    public static bool operator ==(Result left, Result right) => left.Equals(right);
    public static bool operator !=(Result left, Result right) => !left.Equals(right);

    #region Factories

    /// <summary>
    /// Creates a failed result with a generic <see cref="ErrorType.Failure"/> error.
    /// </summary>
    public static Result Fail(string code, string message)
        => new(false, Error.Failure(code, message));

    /// <summary>
    /// Creates a failed result with an <see cref="ErrorType.Validation"/> error.
    /// </summary>
    public static Result Validation(string code, string message)
        => new(false, Error.Validation(code, message));

    /// <summary>
    /// Creates a failed result with a <see cref="ErrorType.NotFound"/> error.
    /// </summary>
    public static Result NotFound(string code, string message)
        => new(false, Error.NotFound(code, message));

    /// <summary>
    /// Creates a failed result with a <see cref="ErrorType.Conflict"/> error.
    /// </summary>
    public static Result Conflict(string code, string message)
        => new(false, Error.Conflict(code, message));

    /// <summary>
    /// Creates a failed result with an <see cref="ErrorType.Unauthorized"/> error.
    /// </summary>
    public static Result Unauthorized(string code, string message)
        => new(false, Error.Unauthorized(code, message));

    /// <summary>
    /// Creates a failed result with a <see cref="ErrorType.Forbidden"/> error.
    /// </summary>
    public static Result Forbidden(string code, string message)
        => new(false, Error.Forbidden(code, message));

    /// <summary>
    /// Creates a failed result with an <see cref="ErrorType.Unexpected"/> error.
    /// </summary>
    public static Result Unexpected(string code, string message)
        => new(false, Error.Unexpected(code, message));
    
    #endregion
}