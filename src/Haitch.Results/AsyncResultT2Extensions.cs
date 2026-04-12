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
    }
}