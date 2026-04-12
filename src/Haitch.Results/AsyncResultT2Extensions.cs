namespace Haitch.Results;

/// <summary>
/// Extension methods for async related operations on <see cref="Result{TValue, TError}"/>.
/// </summary>
public static class AsyncResultT2Extensions
{
    extension<TValue, TError>(Task<Result<TValue, TError>> source)
    {
        /// <summary>
        /// Invokes <paramref name="onSuccess"/> if the result is successful, otherwise <paramref name="onFailure"/>,
        /// and returns the result of the invoked delegate.
        /// </summary>
        public async Task<TOut> MatchAsync<TOut>(
            Func<TValue, Task<TOut>> onSuccess,
            Func<TError, Task<TOut>> onFailure)
        {
            var result = await source.ConfigureAwait(false);

            return result.IsSuccess
                ? await onSuccess(result.Value).ConfigureAwait(false)
                : await onFailure(result.Error).ConfigureAwait(false);
        }

        /// <summary>
        /// Invokes <paramref name="onSuccess"/> if the result is successful, otherwise <paramref name="onFailure"/>,
        /// and returns the result of the invoked delegate.
        /// </summary>
        public async Task<TOut> MatchAsync<TOut>(
            Func<TValue, Task<TOut>> onSuccess,
            Func<TError, TOut> onFailure)
        {
            var result = await source.ConfigureAwait(false);
            
            return result.IsSuccess
                ? await onSuccess(result.Value).ConfigureAwait(false)
                : onFailure(result.Error);
        }

        /// <summary>
        /// Invokes <paramref name="onSuccess"/> if the result is successful, otherwise <paramref name="onFailure"/>,
        /// and returns the result of the invoked delegate.
        /// </summary>
        public async Task<TOut> MatchAsync<TOut>(
            Func<TValue, TOut> onSuccess,
            Func<TError, Task<TOut>> onFailure)
        {
            var result = await source.ConfigureAwait(false);
            
            return result.IsSuccess
                ? onSuccess(result.Value)
                : await onFailure(result.Error).ConfigureAwait(false);
        }

        /// <summary>
        /// Invokes <paramref name="onSuccess"/> if the result is successful, otherwise <paramref name="onFailure"/>,
        /// and returns the result of the invoked delegate.
        /// </summary>
        public async Task<TOut> MatchAsync<TOut>(
            Func<TValue, TOut> onSuccess,
            Func<TError, TOut> onFailure)
        {
            var result = await source.ConfigureAwait(false);

            return result.IsSuccess
                ? onSuccess(result.Value)
                : onFailure(result.Error);
        }

        /// <summary>
        /// Transforms the success value using <paramref name="mapper"/> if the result is successful;
        /// otherwise propagates the existing error unchanged.
        /// </summary>
        /// <typeparam name="TOut">The type of the new success value.</typeparam>
        /// <param name="mapper">A function that transforms the success value.</param>
        public async Task<Result<TOut, TError>> MapAsync<TOut>(Func<TValue, Task<TOut>> mapper)
        {
            var result = await source.ConfigureAwait(false);

            return result.IsSuccess
                ? Result<TOut, TError>.Success(await mapper(result.Value).ConfigureAwait(false))
                : Result<TOut, TError>.Failure(result.Error);
        }

        /// <summary>
        /// Transforms the success value using <paramref name="mapper"/> if the result is successful;
        /// otherwise propagates the existing error unchanged.
        /// </summary>
        /// <typeparam name="TOut">The type of the new success value.</typeparam>
        /// <param name="mapper">A function that transforms the success value.</param>
        public async Task<Result<TOut, TError>> MapAsync<TOut>(Func<TValue, TOut> mapper)
        {
            var result = await source.ConfigureAwait(false);

            return result.IsSuccess
                ? Result<TOut, TError>.Success(mapper(result.Value))
                : Result<TOut, TError>.Failure(result.Error);
        }

        /// <summary>
        /// Transforms the error using <paramref name="mapper"/> if this result is a failure;
        /// otherwise propagates the success value unchanged.
        /// </summary>
        /// <typeparam name="TOutError">The type of the new error.</typeparam>
        /// <param name="mapper">A function that transforms the error.</param>
        public async Task<Result<TValue, TOutError>> MapErrorAsync<TOutError>(
            Func<TError, Task<TOutError>> mapper)
        {
            var result = await source.ConfigureAwait(false);

            return result.IsSuccess
                ? Result<TValue, TOutError>.Success(result.Value)
                : Result<TValue, TOutError>.Failure(await mapper(result.Error).ConfigureAwait(false));
        }

        /// <summary>
        /// Transforms the error using <paramref name="mapper"/> if this result is a failure;
        /// otherwise propagates the success value unchanged.
        /// </summary>
        /// <typeparam name="TOutError">The type of the new error.</typeparam>
        /// <param name="mapper">A function that transforms the error.</param>
        public async Task<Result<TValue, TOutError>> MapErrorAsync<TOutError>(
            Func<TError, TOutError> mapper)
        {
            var result = await source.ConfigureAwait(false);

            return result.IsSuccess
                ? Result<TValue, TOutError>.Success(result.Value)
                : Result<TValue, TOutError>.Failure(mapper(result.Error));
        }

        /// <summary>
        /// Chains a result-returning operation if this result is successful;
        /// otherwise propagates the existing error unchanged.
        /// </summary>
        /// <typeparam name="TOut">The type of the new success value.</typeparam>
        /// <param name="binder">A function that produces the next result from the success value.</param>
        public async Task<Result<TOut, TError>> BindAsync<TOut>(
            Func<TValue, Task<Result<TOut, TError>>> binder)
        {
            var result = await source.ConfigureAwait(false);

            return result.IsSuccess
                ? await binder(result.Value).ConfigureAwait(false)
                : Result<TOut, TError>.Failure(result.Error);
        }

        /// <summary>
        /// Chains a result-returning operation if this result is successful;
        /// otherwise propagates the existing error unchanged.
        /// </summary>
        /// <typeparam name="TOut">The type of the new success value.</typeparam>
        /// <param name="binder">A function that produces the next result from the success value.</param>
        public async Task<Result<TOut, TError>> BindAsync<TOut>(
            Func<TValue, Result<TOut, TError>> binder)
        {
            var result = await source.ConfigureAwait(false);

            return result.IsSuccess
                ? binder(result.Value)
                : Result<TOut, TError>.Failure(result.Error);
        }

        /// <summary>
        /// Invokes <paramref name="action"/> on the success value if this result is successful,
        /// then returns the result unchanged.
        /// </summary>
        /// <param name="action">A side-effecting action to perform on the success value.</param>
        public async Task<Result<TValue, TError>> TapAsync(Func<TValue, Task> action)
        {
            var result = await source.ConfigureAwait(false);

            if (result.IsSuccess) await action(result.Value).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Invokes <paramref name="action"/> on the success value if this result is successful,
        /// then returns the result unchanged.
        /// </summary>
        /// <param name="action">A side-effecting action to perform on the success value.</param>
        public async Task<Result<TValue, TError>> TapAsync(Action<TValue> action)
        {
            var result = await source.ConfigureAwait(false);

            if (result.IsSuccess) action(result.Value);

            return result;
        }

        /// <summary>
        /// Invokes <paramref name="action"/> on the error if this result is a failure,
        /// then returns the result unchanged.
        /// </summary>
        /// <param name="action">A side-effecting action to perform on the error.</param>
        public async Task<Result<TValue, TError>> TapErrorAsync(Func<TError, Task> action)
        {
            var result = await source.ConfigureAwait(false);

            if (result.IsFailure) await action(result.Error).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Invokes <paramref name="action"/> on the error if this result is a failure,
        /// then returns the result unchanged.
        /// </summary>
        /// <param name="action">A side-effecting action to perform on the error.</param>
        public async Task<Result<TValue, TError>> TapErrorAsync(Action<TError> action)
        {
            var result = await source.ConfigureAwait(false);

            if (result.IsFailure) action(result.Error);

            return result;
        }
    }

    extension<TValue, TError>(Result<TValue, TError> source)
    {
        /// <summary>
        /// Invokes <paramref name="onSuccess"/> if the result is successful, otherwise <paramref name="onFailure"/>,
        /// and returns the result of the invoked delegate.
        /// </summary>
        public async Task<TOut> MatchAsync<TOut>(
            Func<TValue, Task<TOut>> onSuccess,
            Func<TError, Task<TOut>> onFailure)
        {
            return source.IsSuccess 
                ? await onSuccess(source.Value).ConfigureAwait(false)
                : await onFailure(source.Error).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Invokes <paramref name="onSuccess"/> if the result is successful, otherwise <paramref name="onFailure"/>,
        /// and returns the result of the invoked delegate.
        /// </summary>
        public async Task<TOut> MatchAsync<TOut>(
            Func<TValue, Task<TOut>> onSuccess,
            Func<TError, TOut> onFailure)
        {
            return source.IsSuccess 
                ? await onSuccess(source.Value).ConfigureAwait(false)
                : onFailure(source.Error);
        }
        
        /// <summary>
        /// Invokes <paramref name="onSuccess"/> if the result is successful, otherwise <paramref name="onFailure"/>,
        /// and returns the result of the invoked delegate.
        /// </summary>
        public async Task<TOut> MatchAsync<TOut>(
            Func<TValue, TOut> onSuccess,
            Func<TError, Task<TOut>> onFailure)
        {
            return source.IsSuccess 
                ? onSuccess(source.Value)
                : await onFailure(source.Error).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Invokes <paramref name="onSuccess"/> if the result is successful, otherwise <paramref name="onFailure"/>,
        /// and returns the result of the invoked delegate.
        /// </summary>
        public Task<TOut> MatchAsync<TOut>(
            Func<TValue, TOut> onSuccess,
            Func<TError, TOut> onFailure)
        {
            var result = source.IsSuccess
                ? onSuccess(source.Value)
                : onFailure(source.Error);

            return Task.FromResult(result);
        }

        /// <summary>
        /// Transforms the success value using <paramref name="mapper"/> if the result is successful;
        /// otherwise propagates the existing error unchanged.
        /// </summary>
        /// <typeparam name="TOut">The type of the new success value.</typeparam>
        /// <param name="mapper">A function that transforms the success value.</param>
        public async Task<Result<TOut, TError>> MapAsync<TOut>(Func<TValue, Task<TOut>> mapper)
        {
            return source.IsSuccess
                ? Result<TOut, TError>.Success(await mapper(source.Value).ConfigureAwait(false))
                : Result<TOut, TError>.Failure(source.Error);
        }

        /// <summary>
        /// Transforms the success value using <paramref name="mapper"/> if the result is successful;
        /// otherwise propagates the existing error unchanged.
        /// </summary>
        /// <typeparam name="TOut">The type of the new success value.</typeparam>
        /// <param name="mapper">A function that transforms the success value.</param>
        public Task<Result<TOut, TError>> MapAsync<TOut>(Func<TValue, TOut> mapper)
        {
            var result = source.IsSuccess
                ? Result<TOut, TError>.Success(mapper(source.Value))
                : Result<TOut, TError>.Failure(source.Error);

            return Task.FromResult(result);
        }

        /// <summary>
        /// Transforms the error using <paramref name="mapper"/> if this result is a failure;
        /// otherwise propagates the success value unchanged.
        /// </summary>
        /// <typeparam name="TOutError">The type of the new error.</typeparam>
        /// <param name="mapper">A function that transforms the error.</param>
        public async Task<Result<TValue, TOutError>> MapErrorAsync<TOutError>(
            Func<TError, Task<TOutError>> mapper)
        {
            return source.IsSuccess
                ? Result<TValue, TOutError>.Success(source.Value)
                : Result<TValue, TOutError>.Failure(await mapper(source.Error).ConfigureAwait(false));
        }

        /// <summary>
        /// Transforms the error using <paramref name="mapper"/> if this result is a failure;
        /// otherwise propagates the success value unchanged.
        /// </summary>
        /// <typeparam name="TOutError">The type of the new error.</typeparam>
        /// <param name="mapper">A function that transforms the error.</param>
        public Task<Result<TValue, TOutError>> MapErrorAsync<TOutError>(
            Func<TError, TOutError> mapper)
        {
            var result = source.IsSuccess
                ? Result<TValue, TOutError>.Success(source.Value)
                : Result<TValue, TOutError>.Failure(mapper(source.Error));

            return Task.FromResult(result);
        }

        /// <summary>
        /// Chains a result-returning operation if this result is successful;
        /// otherwise propagates the existing error unchanged.
        /// </summary>
        /// <typeparam name="TOut">The type of the new success value.</typeparam>
        /// <param name="binder">A function that produces the next result from the success value.</param>
        public async Task<Result<TOut, TError>> BindAsync<TOut>(
            Func<TValue, Task<Result<TOut, TError>>> binder)
        {
            return source.IsSuccess
                ? await binder(source.Value).ConfigureAwait(false)
                : Result<TOut, TError>.Failure(source.Error);
        }

        /// <summary>
        /// Chains a result-returning operation if this result is successful;
        /// otherwise propagates the existing error unchanged.
        /// </summary>
        /// <typeparam name="TOut">The type of the new success value.</typeparam>
        /// <param name="binder">A function that produces the next result from the success value.</param>
        public Task<Result<TOut, TError>> BindAsync<TOut>(
            Func<TValue, Result<TOut, TError>> binder)
        {
            var result = source.IsSuccess
                ? binder(source.Value)
                : Result<TOut, TError>.Failure(source.Error);

            return Task.FromResult(result);
        }

        /// <summary>
        /// Invokes <paramref name="action"/> on the success value if this result is successful,
        /// then returns the result unchanged.
        /// </summary>
        /// <param name="action">A side-effecting action to perform on the success value.</param>
        public async Task<Result<TValue, TError>> TapAsync(Func<TValue, Task> action)
        {
            if (source.IsSuccess) await action(source.Value).ConfigureAwait(false);

            return source;
        }

        /// <summary>
        /// Invokes <paramref name="action"/> on the success value if this result is successful,
        /// then returns the result unchanged.
        /// </summary>
        /// <param name="action">A side-effecting action to perform on the success value.</param>
        public Task<Result<TValue, TError>> TapAsync(Action<TValue> action)
        {
            if (source.IsSuccess) action(source.Value);

            return Task.FromResult(source);
        }

        /// <summary>
        /// Invokes <paramref name="action"/> on the error if this result is a failure,
        /// then returns the result unchanged.
        /// </summary>
        /// <param name="action">A side-effecting action to perform on the error.</param>
        public async Task<Result<TValue, TError>> TapErrorAsync(Func<TError, Task> action)
        {
            if (source.IsFailure) await action(source.Error).ConfigureAwait(false);

            return source;
        }

        /// <summary>
        /// Invokes <paramref name="action"/> on the error if this result is a failure,
        /// then returns the result unchanged.
        /// </summary>
        /// <param name="action">A side-effecting action to perform on the error.</param>
        public Task<Result<TValue, TError>> TapErrorAsync(Action<TError> action)
        {
            if (source.IsFailure) action(source.Error);

            return Task.FromResult(source);
        }
    }
}