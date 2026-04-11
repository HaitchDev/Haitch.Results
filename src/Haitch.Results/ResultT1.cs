using System.Diagnostics.CodeAnalysis;

namespace Haitch.Results;

/// <summary>
/// Represents the outcome of an operation that either produces a value of type
/// <typeparamref name="TValue"/> on success or an <see cref="Haitch.Results.Error"/> on failure.
/// </summary>
/// <typeparam name="TValue">The type of the value returned on success.</typeparam>
public readonly struct Result<TValue> : IEquatable<Result<TValue>>
{
    private readonly TValue? _value;
    private readonly Error? _error;

    /// <summary>
    /// <see langword="true"/> if the operation succeeded; otherwise <see langword="false"/>.
    /// </summary>
    [MemberNotNullWhen(true, nameof(Value), nameof(_value))]
    [MemberNotNullWhen(false, nameof(Error), nameof(_error))]
    public bool IsSuccess { get; }

    /// <summary>
    /// <see langword="true"/> if the operation failed; otherwise <see langword="false"/>.
    /// </summary>
    [MemberNotNullWhen(false, nameof(Value), nameof(_value))]
    [MemberNotNullWhen(true, nameof(Error), nameof(_error))]
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// The success value. Throws <see cref="InvalidOperationException"/> if the result is a failure.
    /// </summary>
    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("Cannot access Value on a failed result.");

    /// <summary>
    /// The failure error. Throws <see cref="InvalidOperationException"/> if the result is a success.
    /// </summary>
    public Error Error => IsFailure
        ? _error!
        : throw new InvalidOperationException("Cannot access Error on a successful result.");

    private Result(TValue value)
    {
        IsSuccess = true;
        _value = value;
        _error = null;
    }

    private Result(Error error)
    {
        IsSuccess = false;
        _value = default;
        _error = error;
    }

    /// <summary>
    /// Creates a successful result containing the specified value.
    /// </summary>
    public static Result<TValue> Success(TValue value) => new(value);

    /// <summary>
    /// Creates a failed result containing the specified error.
    /// </summary>
    public static Result<TValue> Failure(Error error) => new(error);

    /// <summary>
    /// Implicitly converts a value into a successful <see cref="Result{TValue}"/>.
    /// </summary>
    public static implicit operator Result<TValue>(TValue value) => Success(value);

    /// <summary>
    /// Implicitly converts an error into a failed <see cref="Result{TValue}"/>.
    /// </summary>
    public static implicit operator Result<TValue>(Error error) => Failure(error);

    /// <summary>
    /// Invokes <paramref name="onSuccess"/> if the result is successful, otherwise <paramref name="onFailure"/>,
    /// and returns the result of the invoked delegate.
    /// </summary>
    public TOut Match<TOut>(Func<TValue, TOut> onSuccess, Func<Error, TOut> onFailure)
        => IsSuccess ? onSuccess(_value!) : onFailure(_error!);
    
    /// <summary>
    /// Transforms the success value using <paramref name="mapper"/> if the result is successful;
    /// otherwise propagates the existing error unchanged.
    /// </summary>
    /// <typeparam name="TOut">The type of the new success value.</typeparam>
    /// <param name="mapper">A function that transforms the success value.</param>
    public Result<TOut> Map<TOut>(Func<TValue, TOut> mapper)
        => IsSuccess
            ? Result<TOut>.Success(mapper(_value!))
            : Result<TOut>.Failure(_error!);

    /// <summary>
    /// Transforms the error using <paramref name="mapper"/> if this result is a failure;
    /// otherwise propagates the success value unchanged.
    /// </summary>
    /// <param name="mapper">A function that transforms the error.</param>
    public Result<TValue> MapError(Func<Error, Error> mapper)
        => IsSuccess
            ? this
            : Failure(mapper(_error!));
    
    /// <summary>
    /// Chains a result-returning operation if this result is successful;
    /// otherwise propagates the existing error unchanged.
    /// </summary>
    /// <typeparam name="TOut">The type of the new success value.</typeparam>
    /// <param name="binder">A function that produces the next result from the success value.</param>
    public Result<TOut> Bind<TOut>(Func<TValue, Result<TOut>> binder)
        => IsSuccess
            ? binder(_value!)
            : Result<TOut>.Failure(_error!);

    /// <summary>
    /// Invokes <paramref name="action"/> on the success value if this result is successful,
    /// then returns the result unchanged.
    /// </summary>
    /// <param name="action">A side-effecting action to perform on the success value.</param>
    public Result<TValue> Tap(Action<TValue> action)
    {
        if (IsSuccess) action(_value!);
        return this;
    }

    /// <summary>
    /// Invokes <paramref name="action"/> on the error if this result is a failure,
    /// then returns the result unchanged.
    /// </summary>
    /// <param name="action">A side-effecting action to perform on the error.</param>
    public Result<TValue> TapError(Action<Error> action)
    {
        if (IsFailure) action(_error!);
        return this;
    }

    /// <summary>
    /// Converts a successful result into a failure if <paramref name="predicate"/> returns
    /// <see langword="false"/>; otherwise returns the result unchanged.
    /// </summary>
    /// <param name="predicate">A predicate evaluated against the success value.</param>
    /// <param name="error">The error to use if the predicate fails.</param>
    public Result<TValue> Ensure(Func<TValue, bool> predicate, Error error)
        => IsSuccess && !predicate(_value!)
            ? Failure(error)
            : this;

    /// <inheritdoc />
    public bool Equals(Result<TValue> other)
        => IsSuccess == other.IsSuccess
           && (IsSuccess
               ? EqualityComparer<TValue?>.Default.Equals(_value, other._value)
               : EqualityComparer<Error?>.Default.Equals(_error, other._error));

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is Result<TValue> other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode()
        => IsSuccess
            ? HashCode.Combine(true, _value)
            : HashCode.Combine(false, _error);

    public static bool operator ==(Result<TValue> left, Result<TValue> right) => left.Equals(right);
    public static bool operator !=(Result<TValue> left, Result<TValue> right) => !left.Equals(right);

    #region Factories

    /// <summary>
    /// Creates a failed result with a generic <see cref="ErrorType.Failure"/> error.
    /// </summary>
    public static Result<TValue> Fail(string code, string message)
        => new(Error.Failure(code, message));

    /// <summary>
    /// Creates a failed result with an <see cref="ErrorType.Validation"/> error.
    /// </summary>
    public static Result<TValue> Validation(string code, string message)
        => new(Error.Validation(code, message));

    /// <summary>
    /// Creates a failed result with a <see cref="ErrorType.NotFound"/> error.
    /// </summary>
    public static Result<TValue> NotFound(string code, string message)
        => new(Error.NotFound(code, message));

    /// <summary>
    /// Creates a failed result with a <see cref="ErrorType.Conflict"/> error.
    /// </summary>
    public static Result<TValue> Conflict(string code, string message)
        => new(Error.Conflict(code, message));

    /// <summary>
    /// Creates a failed result with an <see cref="ErrorType.Unauthorized"/> error.
    /// </summary>
    public static Result<TValue> Unauthorized(string code, string message)
        => new(Error.Unauthorized(code, message));

    /// <summary>
    /// Creates a failed result with a <see cref="ErrorType.Forbidden"/> error.
    /// </summary>
    public static Result<TValue> Forbidden(string code, string message)
        => new(Error.Forbidden(code, message));

    /// <summary>
    /// Creates a failed result with an <see cref="ErrorType.Unexpected"/> error.
    /// </summary>
    public static Result<TValue> Unexpected(string code, string message)
        => new(Error.Unexpected(code, message));
    
    #endregion
}