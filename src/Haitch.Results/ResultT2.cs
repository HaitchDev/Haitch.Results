using System.Diagnostics.CodeAnalysis;

namespace Haitch.Results;

/// <summary>
/// Represents the outcome of an operation that either produces a value of type
/// <typeparamref name="TValue"/> on success or an error of type <typeparamref name="TError"/> on failure.
/// </summary>
/// <typeparam name="TValue">The type of the value returned on success.</typeparam>
/// <typeparam name="TError">The type of the error returned on failure.</typeparam>
public readonly struct Result<TValue, TError> : IEquatable<Result<TValue, TError>>
{
    private readonly TValue? _value;
    private readonly TError? _error;

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
    public TError Error => IsFailure
        ? _error!
        : throw new InvalidOperationException("Cannot access Error on a successful result.");

    private Result(TValue value)
    {
        IsSuccess = true;
        _value = value;
        _error = default;
    }

    private Result(TError error)
    {
        IsSuccess = false;
        _value = default;
        _error = error;
    }

    /// <summary>
    /// Creates a successful result containing the specified value.
    /// </summary>
    public static Result<TValue, TError> Success(TValue value) => new(value);

    /// <summary>
    /// Creates a failed result containing the specified error.
    /// </summary>
    public static Result<TValue, TError> Failure(TError error) => new(error);

    /// <summary>
    /// Implicitly converts a value into a successful <see cref="Result{TValue, TError}"/>.
    /// </summary>
    public static implicit operator Result<TValue, TError>(TValue value) => Success(value);

    /// <summary>
    /// Implicitly converts an error into a failed <see cref="Result{TValue, TError}"/>.
    /// </summary>
    public static implicit operator Result<TValue, TError>(TError error) => Failure(error);

    /// <summary>
    /// Invokes <paramref name="onSuccess"/> if the result is successful, otherwise <paramref name="onFailure"/>,
    /// and returns the result of the invoked delegate.
    /// </summary>
    public TOut Match<TOut>(Func<TValue, TOut> onSuccess, Func<TError, TOut> onFailure)
        => IsSuccess ? onSuccess(_value!) : onFailure(_error!);

    /// <inheritdoc />
    public bool Equals(Result<TValue, TError> other)
        => IsSuccess == other.IsSuccess
           && (IsSuccess
               ? EqualityComparer<TValue?>.Default.Equals(_value, other._value)
               : EqualityComparer<TError?>.Default.Equals(_error, other._error));

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is Result<TValue, TError> other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode()
        => IsSuccess
            ? HashCode.Combine(true, _value)
            : HashCode.Combine(false, _error);

    public static bool operator ==(Result<TValue, TError> left, Result<TValue, TError> right) => left.Equals(right);
    public static bool operator !=(Result<TValue, TError> left, Result<TValue, TError> right) => !left.Equals(right);
}