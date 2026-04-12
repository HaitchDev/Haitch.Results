namespace Haitch.Results;

/// <summary>
/// Extension methods for async related operations on <see cref="Result"/>.
/// </summary>
public static class AsyncResultExtensions
{
    extension(Task<Result> source)
    {
        /// <summary>
        /// Invokes <paramref name="onSuccess"/> if the result is successful, otherwise <paramref name="onFailure"/>,
        /// and returns the result of the invoked delegate.
        /// </summary>
        public async Task<TOut> MatchAsync<TOut>(
            Func<Task<TOut>> onSuccess,
            Func<Error, Task<TOut>> onFailure)
        {
            var result = await source.ConfigureAwait(false);

            return result.IsSuccess
                ? await onSuccess().ConfigureAwait(false)
                : await onFailure(result.Error).ConfigureAwait(false);
        }

        /// <summary>
        /// Invokes <paramref name="onSuccess"/> if the result is successful, otherwise <paramref name="onFailure"/>,
        /// and returns the result of the invoked delegate.
        /// </summary>
        public async Task<TOut> MatchAsync<TOut>(
            Func<Task<TOut>> onSuccess,
            Func<Error, TOut> onFailure)
        {
            var result = await source.ConfigureAwait(false);
            
            return result.IsSuccess
                ? await onSuccess().ConfigureAwait(false)
                : onFailure(result.Error);
        }

        /// <summary>
        /// Invokes <paramref name="onSuccess"/> if the result is successful, otherwise <paramref name="onFailure"/>,
        /// and returns the result of the invoked delegate.
        /// </summary>
        public async Task<TOut> MatchAsync<TOut>(
            Func<TOut> onSuccess,
            Func<Error, Task<TOut>> onFailure)
        {
            var result = await source.ConfigureAwait(false);
            
            return result.IsSuccess
                ? onSuccess()
                : await onFailure(result.Error).ConfigureAwait(false);
        }

        /// <summary>
        /// Invokes <paramref name="onSuccess"/> if the result is successful, otherwise <paramref name="onFailure"/>,
        /// and returns the result of the invoked delegate.
        /// </summary>
        public async Task<TOut> MatchAsync<TOut>(
            Func<TOut> onSuccess,
            Func<Error, TOut> onFailure)
        {
            var result = await source.ConfigureAwait(false);

            return result.IsSuccess
                ? onSuccess()
                : onFailure(result.Error);
        }

        /// <summary>
        /// Produces a value using <paramref name="mapper"/> if the result is successful;
        /// otherwise propagates the existing error unchanged.
        /// </summary>
        /// <typeparam name="TOut">The type of the value to produce on success.</typeparam>
        /// <param name="mapper">A function that produces the value to wrap in the result.</param>
        public async Task<Result<TOut>> MapAsync<TOut>(Func<Task<TOut>> mapper)
        {
            var result = await source.ConfigureAwait(false);

            return result.IsSuccess
                ? Result<TOut>.Success(await mapper().ConfigureAwait(false))
                : Result<TOut>.Failure(result.Error);
        }

        /// <summary>
        /// Produces a value using <paramref name="mapper"/> if the result is successful;
        /// otherwise propagates the existing error unchanged.
        /// </summary>
        /// <typeparam name="TOut">The type of the value to produce on success.</typeparam>
        /// <param name="mapper">A function that produces the value to wrap in the result.</param>
        public async Task<Result<TOut>> MapAsync<TOut>(Func<TOut> mapper)
        {
            var result = await source.ConfigureAwait(false);

            return result.IsSuccess
                ? Result<TOut>.Success(mapper())
                : Result<TOut>.Failure(result.Error);
        }

        /// <summary>
        /// Transforms the error using <paramref name="mapper"/> if this result is a failure;
        /// otherwise propagates success unchanged.
        /// </summary>
        /// <param name="mapper">A function that transforms the error.</param>
        public async Task<Result> MapErrorAsync(Func<Error, Task<Error>> mapper)
        {
            var result = await source.ConfigureAwait(false);

            return result.IsSuccess
                ? result
                : Result.Failure(await mapper(result.Error).ConfigureAwait(false));
        }

        /// <summary>
        /// Transforms the error using <paramref name="mapper"/> if this result is a failure;
        /// otherwise propagates success unchanged.
        /// </summary>
        /// <param name="mapper">A function that transforms the error.</param>
        public async Task<Result> MapErrorAsync(Func<Error, Error> mapper)
        {
            var result = await source.ConfigureAwait(false);

            return result.IsSuccess
                ? result
                : Result.Failure(mapper(result.Error));
        }

        /// <summary>
        /// Chains a result-returning operation if this result is successful;
        /// otherwise propagates the existing error unchanged.
        /// </summary>
        /// <param name="binder">A function that produces the next result.</param>
        public async Task<Result> BindAsync(Func<Task<Result>> binder)
        {
            var result = await source.ConfigureAwait(false);

            return result.IsSuccess
                ? await binder().ConfigureAwait(false)
                : result;
        }

        /// <summary>
        /// Chains a result-returning operation if this result is successful;
        /// otherwise propagates the existing error unchanged.
        /// </summary>
        /// <param name="binder">A function that produces the next result.</param>
        public async Task<Result> BindAsync(Func<Result> binder)
        {
            var result = await source.ConfigureAwait(false);

            return result.IsSuccess
                ? binder()
                : result;
        }

        /// <summary>
        /// Chains a result-returning operation that produces a value if this result is successful;
        /// otherwise propagates the existing error unchanged.
        /// </summary>
        /// <typeparam name="TOut">The type of the value produced on success.</typeparam>
        /// <param name="binder">A function that produces the next result.</param>
        public async Task<Result<TOut>> BindAsync<TOut>(Func<Task<Result<TOut>>> binder)
        {
            var result = await source.ConfigureAwait(false);

            return result.IsSuccess
                ? await binder().ConfigureAwait(false)
                : Result<TOut>.Failure(result.Error);
        }

        /// <summary>
        /// Chains a result-returning operation that produces a value if this result is successful;
        /// otherwise propagates the existing error unchanged.
        /// </summary>
        /// <typeparam name="TOut">The type of the value produced on success.</typeparam>
        /// <param name="binder">A function that produces the next result.</param>
        public async Task<Result<TOut>> BindAsync<TOut>(Func<Result<TOut>> binder)
        {
            var result = await source.ConfigureAwait(false);

            return result.IsSuccess
                ? binder()
                : Result<TOut>.Failure(result.Error);
        }

        /// <summary>
        /// Invokes <paramref name="action"/> if this result is successful,
        /// then returns the result unchanged.
        /// </summary>
        /// <param name="action">A side-effecting action to perform on success.</param>
        public async Task<Result> TapAsync(Func<Task> action)
        {
            var result = await source.ConfigureAwait(false);

            if (result.IsSuccess)
            {
                await action().ConfigureAwait(false);
            }

            return result;
        }

        /// <summary>
        /// Invokes <paramref name="action"/> if this result is successful,
        /// then returns the result unchanged.
        /// </summary>
        /// <param name="action">A side-effecting action to perform on success.</param>
        public async Task<Result> TapAsync(Action action)
        {
            var result = await source.ConfigureAwait(false);

            if (result.IsSuccess)
            {
                action();
            }

            return result;
        }

        /// <summary>
        /// Invokes <paramref name="action"/> on the error if this result is a failure,
        /// then returns the result unchanged.
        /// </summary>
        /// <param name="action">A side-effecting action to perform on the error.</param>
        public async Task<Result> TapErrorAsync(Func<Error, Task> action)
        {
            var result = await source.ConfigureAwait(false);

            if (result.IsFailure)
            {
                await action(result.Error).ConfigureAwait(false);
            }

            return result;
        }

        /// <summary>
        /// Invokes <paramref name="action"/> on the error if this result is a failure,
        /// then returns the result unchanged.
        /// </summary>
        /// <param name="action">A side-effecting action to perform on the error.</param>
        public async Task<Result> TapErrorAsync(Action<Error> action)
        {
            var result = await source.ConfigureAwait(false);

            if (result.IsFailure)
            {
                action(result.Error);
            }

            return result;
        }

        /// <summary>
        /// Converts a successful result into a failure if <paramref name="predicate"/> returns
        /// <see langword="false"/>; otherwise returns the result unchanged.
        /// </summary>
        /// <param name="predicate">A predicate evaluated when the result is successful.</param>
        /// <param name="error">The error to use if the predicate fails.</param>
        public async Task<Result> EnsureAsync(Func<Task<bool>> predicate, Error error)
        {
            var result = await source.ConfigureAwait(false);

            return result.IsSuccess && !await predicate().ConfigureAwait(false)
                ? Result.Failure(error)
                : result;
        }

        /// <summary>
        /// Converts a successful result into a failure if <paramref name="predicate"/> returns
        /// <see langword="false"/>; otherwise returns the result unchanged.
        /// </summary>
        /// <param name="predicate">A predicate evaluated when the result is successful.</param>
        /// <param name="error">The error to use if the predicate fails.</param>
        public async Task<Result> EnsureAsync(Func<bool> predicate, Error error)
        {
            var result = await source.ConfigureAwait(false);

            return result.IsSuccess && !predicate()
                ? Result.Failure(error)
                : result;
        }
    }

    extension(Result source)
    {
        /// <summary>
        /// Invokes <paramref name="onSuccess"/> if the result is successful, otherwise <paramref name="onFailure"/>,
        /// and returns the result of the invoked delegate.
        /// </summary>
        public async Task<TOut> MatchAsync<TOut>(
            Func<Task<TOut>> onSuccess,
            Func<Error, Task<TOut>> onFailure)
        {
            return source.IsSuccess 
                ? await onSuccess().ConfigureAwait(false)
                : await onFailure(source.Error).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Invokes <paramref name="onSuccess"/> if the result is successful, otherwise <paramref name="onFailure"/>,
        /// and returns the result of the invoked delegate.
        /// </summary>
        public async Task<TOut> MatchAsync<TOut>(
            Func<Task<TOut>> onSuccess,
            Func<Error, TOut> onFailure)
        {
            return source.IsSuccess 
                ? await onSuccess().ConfigureAwait(false)
                : onFailure(source.Error);
        }
        
        /// <summary>
        /// Invokes <paramref name="onSuccess"/> if the result is successful, otherwise <paramref name="onFailure"/>,
        /// and returns the result of the invoked delegate.
        /// </summary>
        public async Task<TOut> MatchAsync<TOut>(
            Func<TOut> onSuccess,
            Func<Error, Task<TOut>> onFailure)
        {
            return source.IsSuccess 
                ? onSuccess()
                : await onFailure(source.Error).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Invokes <paramref name="onSuccess"/> if the result is successful, otherwise <paramref name="onFailure"/>,
        /// and returns the result of the invoked delegate.
        /// </summary>
        public Task<TOut> MatchAsync<TOut>(
            Func<TOut> onSuccess,
            Func<Error, TOut> onFailure)
        {
            var result = source.IsSuccess
                ? onSuccess()
                : onFailure(source.Error);

            return Task.FromResult(result);
        }

        /// <summary>
        /// Produces a value using <paramref name="mapper"/> if the result is successful;
        /// otherwise propagates the existing error unchanged.
        /// </summary>
        /// <typeparam name="TOut">The type of the value to produce on success.</typeparam>
        /// <param name="mapper">A function that produces the value to wrap in the result.</param>
        public async Task<Result<TOut>> MapAsync<TOut>(Func<Task<TOut>> mapper)
        {
            return source.IsSuccess
                ? Result<TOut>.Success(await mapper().ConfigureAwait(false))
                : Result<TOut>.Failure(source.Error);
        }

        /// <summary>
        /// Produces a value using <paramref name="mapper"/> if the result is successful;
        /// otherwise propagates the existing error unchanged.
        /// </summary>
        /// <typeparam name="TOut">The type of the value to produce on success.</typeparam>
        /// <param name="mapper">A function that produces the value to wrap in the result.</param>
        public Task<Result<TOut>> MapAsync<TOut>(Func<TOut> mapper)
        {
            var result = source.IsSuccess
                ? Result<TOut>.Success(mapper())
                : Result<TOut>.Failure(source.Error);

            return Task.FromResult(result);
        }

        /// <summary>
        /// Transforms the error using <paramref name="mapper"/> if this result is a failure;
        /// otherwise propagates success unchanged.
        /// </summary>
        /// <param name="mapper">A function that transforms the error.</param>
        public async Task<Result> MapErrorAsync(Func<Error, Task<Error>> mapper)
        {
            return source.IsSuccess
                ? source
                : Result.Failure(await mapper(source.Error).ConfigureAwait(false));
        }

        /// <summary>
        /// Transforms the error using <paramref name="mapper"/> if this result is a failure;
        /// otherwise propagates success unchanged.
        /// </summary>
        /// <param name="mapper">A function that transforms the error.</param>
        public Task<Result> MapErrorAsync(Func<Error, Error> mapper)
        {
            var result = source.IsSuccess
                ? source
                : Result.Failure(mapper(source.Error));

            return Task.FromResult(result);
        }

        /// <summary>
        /// Chains a result-returning operation if this result is successful;
        /// otherwise propagates the existing error unchanged.
        /// </summary>
        /// <param name="binder">A function that produces the next result.</param>
        public async Task<Result> BindAsync(Func<Task<Result>> binder)
        {
            return source.IsSuccess
                ? await binder().ConfigureAwait(false)
                : source;
        }

        /// <summary>
        /// Chains a result-returning operation if this result is successful;
        /// otherwise propagates the existing error unchanged.
        /// </summary>
        /// <param name="binder">A function that produces the next result.</param>
        public Task<Result> BindAsync(Func<Result> binder)
        {
            var result = source.IsSuccess
                ? binder()
                : source;

            return Task.FromResult(result);
        }

        /// <summary>
        /// Chains a result-returning operation that produces a value if this result is successful;
        /// otherwise propagates the existing error unchanged.
        /// </summary>
        /// <typeparam name="TOut">The type of the value produced on success.</typeparam>
        /// <param name="binder">A function that produces the next result.</param>
        public async Task<Result<TOut>> BindAsync<TOut>(Func<Task<Result<TOut>>> binder)
        {
            return source.IsSuccess
                ? await binder().ConfigureAwait(false)
                : Result<TOut>.Failure(source.Error);
        }

        /// <summary>
        /// Chains a result-returning operation that produces a value if this result is successful;
        /// otherwise propagates the existing error unchanged.
        /// </summary>
        /// <typeparam name="TOut">The type of the value produced on success.</typeparam>
        /// <param name="binder">A function that produces the next result.</param>
        public Task<Result<TOut>> BindAsync<TOut>(Func<Result<TOut>> binder)
        {
            var result = source.IsSuccess
                ? binder()
                : Result<TOut>.Failure(source.Error);

            return Task.FromResult(result);
        }

        /// <summary>
        /// Invokes <paramref name="action"/> if this result is successful,
        /// then returns the result unchanged.
        /// </summary>
        /// <param name="action">A side-effecting action to perform on success.</param>
        public async Task<Result> TapAsync(Func<Task> action)
        {
            if (source.IsSuccess)
            {
                await action().ConfigureAwait(false);
            }

            return source;
        }

        /// <summary>
        /// Invokes <paramref name="action"/> if this result is successful,
        /// then returns the result unchanged.
        /// </summary>
        /// <param name="action">A side-effecting action to perform on success.</param>
        public Task<Result> TapAsync(Action action)
        {
            if (source.IsSuccess)
            {
                action();
            }

            return Task.FromResult(source);
        }

        /// <summary>
        /// Invokes <paramref name="action"/> on the error if this result is a failure,
        /// then returns the result unchanged.
        /// </summary>
        /// <param name="action">A side-effecting action to perform on the error.</param>
        public async Task<Result> TapErrorAsync(Func<Error, Task> action)
        {
            if (source.IsFailure)
            {
                await action(source.Error).ConfigureAwait(false);
            }

            return source;
        }

        /// <summary>
        /// Invokes <paramref name="action"/> on the error if this result is a failure,
        /// then returns the result unchanged.
        /// </summary>
        /// <param name="action">A side-effecting action to perform on the error.</param>
        public Task<Result> TapErrorAsync(Action<Error> action)
        {
            if (source.IsFailure)
            {
                action(source.Error);
            }

            return Task.FromResult(source);
        }

        /// <summary>
        /// Converts a successful result into a failure if <paramref name="predicate"/> returns
        /// <see langword="false"/>; otherwise returns the result unchanged.
        /// </summary>
        /// <param name="predicate">A predicate evaluated when the result is successful.</param>
        /// <param name="error">The error to use if the predicate fails.</param>
        public async Task<Result> EnsureAsync(Func<Task<bool>> predicate, Error error)
        {
            return source.IsSuccess && !await predicate().ConfigureAwait(false)
                ? Result.Failure(error)
                : source;
        }

        /// <summary>
        /// Converts a successful result into a failure if <paramref name="predicate"/> returns
        /// <see langword="false"/>; otherwise returns the result unchanged.
        /// </summary>
        /// <param name="predicate">A predicate evaluated when the result is successful.</param>
        /// <param name="error">The error to use if the predicate fails.</param>
        public Task<Result> EnsureAsync(Func<bool> predicate, Error error)
        {
            var result = source.IsSuccess && !predicate()
                ? Result.Failure(error)
                : source;

            return Task.FromResult(result);
        }
    }
}