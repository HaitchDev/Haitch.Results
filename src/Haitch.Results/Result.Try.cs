namespace Haitch.Results;

public readonly partial struct Result
{
    #region Try

    /// <summary>
    /// Executes <paramref name="action"/>, capturing any thrown exception as a failed <see cref="Result"/>.
    /// </summary>
    /// <param name="action">The operation to execute.</param>
    /// <param name="onException">
    /// Optional converter from the caught exception to an <see cref="Error"/>. When omitted, the exception
    /// is mapped to an <see cref="ErrorType.Unexpected"/> error using its type name and message.
    /// </param>
    public static Result Try(Action action, Func<Exception, Error>? onException = null)
    {
        try
        {
            action();
            return Success();
        }
        catch (Exception exception)
        {
            return Failure(ToError(exception, onException));
        }
    }

    /// <summary>
    /// Executes <paramref name="func"/>, capturing its return value as a successful <see cref="Result{TValue}"/>
    /// or any thrown exception as a failure.
    /// </summary>
    /// <typeparam name="TValue">The type of the value produced on success.</typeparam>
    /// <param name="func">The value-producing operation to execute.</param>
    /// <param name="onException">
    /// Optional converter from the caught exception to an <see cref="Error"/>. When omitted, the exception
    /// is mapped to an <see cref="ErrorType.Unexpected"/> error using its type name and message.
    /// </param>
    public static Result<TValue> Try<TValue>(Func<TValue> func, Func<Exception, Error>? onException = null)
    {
        try
        {
            return Result<TValue>.Success(func());
        }
        catch (Exception exception)
        {
            return Result<TValue>.Failure(ToError(exception, onException));
        }
    }

    /// <summary>
    /// Awaits <paramref name="action"/>, capturing any thrown exception as a failed <see cref="Result"/>.
    /// </summary>
    /// <param name="action">The asynchronous operation to execute.</param>
    /// <param name="onException">
    /// Optional converter from the caught exception to an <see cref="Error"/>. When omitted, the exception
    /// is mapped to an <see cref="ErrorType.Unexpected"/> error using its type name and message.
    /// </param>
    public static async Task<Result> TryAsync(Func<Task> action, Func<Exception, Error>? onException = null)
    {
        try
        {
            await action().ConfigureAwait(false);
            return Success();
        }
        catch (Exception exception)
        {
            return Failure(ToError(exception, onException));
        }
    }

    /// <summary>
    /// Awaits <paramref name="func"/>, capturing its return value as a successful <see cref="Result{TValue}"/>
    /// or any thrown exception as a failure.
    /// </summary>
    /// <typeparam name="TValue">The type of the value produced on success.</typeparam>
    /// <param name="func">The asynchronous value-producing operation to execute.</param>
    /// <param name="onException">
    /// Optional converter from the caught exception to an <see cref="Error"/>. When omitted, the exception
    /// is mapped to an <see cref="ErrorType.Unexpected"/> error using its type name and message.
    /// </param>
    public static async Task<Result<TValue>> TryAsync<TValue>(
        Func<Task<TValue>> func,
        Func<Exception, Error>? onException = null)
    {
        try
        {
            return Result<TValue>.Success(await func().ConfigureAwait(false));
        }
        catch (Exception exception)
        {
            return Result<TValue>.Failure(ToError(exception, onException));
        }
    }

    /// <summary>
    /// Executes <paramref name="func"/>, capturing its return value as a successful
    /// <see cref="Result{TValue, TError}"/> or any thrown exception as a failure.
    /// </summary>
    /// <typeparam name="TValue">The type of the value produced on success.</typeparam>
    /// <typeparam name="TError">The error type produced on failure.</typeparam>
    /// <param name="func">The value-producing operation to execute.</param>
    /// <param name="onException">Converts the caught exception into a <typeparamref name="TError"/>.</param>
    public static Result<TValue, TError> Try<TValue, TError>(
        Func<TValue> func,
        Func<Exception, TError> onException)
    {
        try
        {
            return Result<TValue, TError>.Success(func());
        }
        catch (Exception exception)
        {
            return Result<TValue, TError>.Failure(onException(exception));
        }
    }

    /// <summary>
    /// Awaits <paramref name="func"/>, capturing its return value as a successful
    /// <see cref="Result{TValue, TError}"/> or any thrown exception as a failure.
    /// </summary>
    /// <typeparam name="TValue">The type of the value produced on success.</typeparam>
    /// <typeparam name="TError">The error type produced on failure.</typeparam>
    /// <param name="func">The asynchronous value-producing operation to execute.</param>
    /// <param name="onException">Converts the caught exception into a <typeparamref name="TError"/>.</param>
    public static async Task<Result<TValue, TError>> TryAsync<TValue, TError>(
        Func<Task<TValue>> func,
        Func<Exception, TError> onException)
    {
        try
        {
            return Result<TValue, TError>.Success(await func().ConfigureAwait(false));
        }
        catch (Exception exception)
        {
            return Result<TValue, TError>.Failure(onException(exception));
        }
    }

    #endregion

    private static Error ToError(Exception exception, Func<Exception, Error>? onException)
        => onException?.Invoke(exception) ?? Error.Unexpected(exception.GetType().Name, exception.Message);
}
