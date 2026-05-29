namespace Haitch.Results;

public readonly partial struct Result
{
    #region Collect

    /// <summary>
    /// Combines a sequence of valueless results into one. Succeeds only if every result succeeds; otherwise
    /// collects the failures. A single failure is returned unchanged, while multiple failures are wrapped in
    /// an <see cref="ErrorType.Aggregate"/> error.
    /// </summary>
    /// <param name="results">The results to collect.</param>
    /// <param name="code">Optional top-level code for the aggregate error when more than one result fails.</param>
    /// <param name="message">Optional top-level message for the aggregate error when more than one result fails.</param>
    public static Result Collect(
        IEnumerable<Result> results,
        string? code = null,
        string? message = null)
    {
        List<Error>? errors = null;

        foreach (var result in results)
        {
            if (result.IsFailure)
            {
                (errors ??= []).Add(result.Error);
            }
        }

        return errors is null
            ? Success()
            : Failure(AggregateErrors(errors, code, message));
    }

    /// <summary>
    /// Combines a sequence of results of the same type into a single result whose value is an array of
    /// every success value, preserving order. Succeeds only if every result succeeds; otherwise collects
    /// the failures. A single failure is returned unchanged, while multiple failures are wrapped in an
    /// <see cref="ErrorType.Aggregate"/> error.
    /// </summary>
    /// <typeparam name="TValue">The value type shared by all results.</typeparam>
    /// <param name="results">The results to collect.</param>
    /// <param name="code">Optional top-level code for the aggregate error when more than one result fails.</param>
    /// <param name="message">Optional top-level message for the aggregate error when more than one result fails.</param>
    public static Result<TValue[]> Collect<TValue>(
        IEnumerable<Result<TValue>> results,
        string? code = null,
        string? message = null)
    {
        var values = new List<TValue>();
        List<Error>? errors = null;

        foreach (var result in results)
        {
            if (result.IsSuccess)
            {
                values.Add(result.Value);
            }
            else
            {
                (errors ??= []).Add(result.Error);
            }
        }

        return errors is null
            ? Result<TValue[]>.Success(values.ToArray())
            : Result<TValue[]>.Failure(AggregateErrors(errors, code, message));
    }

    /// <summary>
    /// Combines a sequence of results sharing a value type and error type into a single result whose value
    /// is an array of every success value, preserving order. Succeeds only if every result succeeds;
    /// otherwise the failed errors are passed to <paramref name="aggregator"/>, which produces the single
    /// error returned.
    /// </summary>
    /// <typeparam name="TValue">The value type shared by all results.</typeparam>
    /// <typeparam name="TError">The error type shared by all results.</typeparam>
    /// <param name="results">The results to collect.</param>
    /// <param name="aggregator">Combines the collected failures into a single error.</param>
    public static Result<TValue[], TError> Collect<TValue, TError>(
        IEnumerable<Result<TValue, TError>> results,
        Func<IReadOnlyList<TError>, TError> aggregator)
    {
        var values = new List<TValue>();
        List<TError>? errors = null;

        foreach (var result in results)
        {
            if (result.IsSuccess)
            {
                values.Add(result.Value);
            }
            else
            {
                (errors ??= []).Add(result.Error);
            }
        }

        return errors is null
            ? Result<TValue[], TError>.Success(values.ToArray())
            : Result<TValue[], TError>.Failure(aggregator(errors));
    }

    #endregion
}
