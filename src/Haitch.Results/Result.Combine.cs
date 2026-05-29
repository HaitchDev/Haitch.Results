namespace Haitch.Results;

public readonly partial struct Result
{
    #region Combine (no value)

    /// <summary>
    /// Combines 2 valueless results into one. Succeeds only if every result succeeds; otherwise
    /// collects the failures. A single failure is returned unchanged, while multiple failures are
    /// wrapped in an <see cref="ErrorType.Aggregate"/> error.
    /// </summary>
    /// <param name="result1">The first result to combine.</param>
    /// <param name="result2">The second result to combine.</param>
    /// <param name="code">Optional top-level code for the aggregate error when more than one result fails.</param>
    /// <param name="message">Optional top-level message for the aggregate error when more than one result fails.</param>
    public static Result Combine(
        Result result1,
        Result result2,
        string? code = null,
        string? message = null)
    {
        if (result1.IsSuccess && result2.IsSuccess)
        {
            return Success();
        }

        var errors = new List<Error>(2);
        if (result1.IsFailure) { errors.Add(result1.Error); }
        if (result2.IsFailure) { errors.Add(result2.Error); }

        return Failure(AggregateErrors(errors, code, message));
    }

    /// <summary>
    /// Combines 3 valueless results into one. Succeeds only if every result succeeds; otherwise
    /// collects the failures. A single failure is returned unchanged, while multiple failures are
    /// wrapped in an <see cref="ErrorType.Aggregate"/> error.
    /// </summary>
    /// <param name="result1">The first result to combine.</param>
    /// <param name="result2">The second result to combine.</param>
    /// <param name="result3">The third result to combine.</param>
    /// <param name="code">Optional top-level code for the aggregate error when more than one result fails.</param>
    /// <param name="message">Optional top-level message for the aggregate error when more than one result fails.</param>
    public static Result Combine(
        Result result1,
        Result result2,
        Result result3,
        string? code = null,
        string? message = null)
    {
        if (result1.IsSuccess && result2.IsSuccess && result3.IsSuccess)
        {
            return Success();
        }

        var errors = new List<Error>(3);
        if (result1.IsFailure) { errors.Add(result1.Error); }
        if (result2.IsFailure) { errors.Add(result2.Error); }
        if (result3.IsFailure) { errors.Add(result3.Error); }

        return Failure(AggregateErrors(errors, code, message));
    }

    /// <summary>
    /// Combines 4 valueless results into one. Succeeds only if every result succeeds; otherwise
    /// collects the failures. A single failure is returned unchanged, while multiple failures are
    /// wrapped in an <see cref="ErrorType.Aggregate"/> error.
    /// </summary>
    /// <param name="result1">The first result to combine.</param>
    /// <param name="result2">The second result to combine.</param>
    /// <param name="result3">The third result to combine.</param>
    /// <param name="result4">The fourth result to combine.</param>
    /// <param name="code">Optional top-level code for the aggregate error when more than one result fails.</param>
    /// <param name="message">Optional top-level message for the aggregate error when more than one result fails.</param>
    public static Result Combine(
        Result result1,
        Result result2,
        Result result3,
        Result result4,
        string? code = null,
        string? message = null)
    {
        if (result1.IsSuccess && result2.IsSuccess && result3.IsSuccess && result4.IsSuccess)
        {
            return Success();
        }

        var errors = new List<Error>(4);
        if (result1.IsFailure) { errors.Add(result1.Error); }
        if (result2.IsFailure) { errors.Add(result2.Error); }
        if (result3.IsFailure) { errors.Add(result3.Error); }
        if (result4.IsFailure) { errors.Add(result4.Error); }

        return Failure(AggregateErrors(errors, code, message));
    }

    /// <summary>
    /// Combines 5 valueless results into one. Succeeds only if every result succeeds; otherwise
    /// collects the failures. A single failure is returned unchanged, while multiple failures are
    /// wrapped in an <see cref="ErrorType.Aggregate"/> error.
    /// </summary>
    /// <param name="result1">The first result to combine.</param>
    /// <param name="result2">The second result to combine.</param>
    /// <param name="result3">The third result to combine.</param>
    /// <param name="result4">The fourth result to combine.</param>
    /// <param name="result5">The fifth result to combine.</param>
    /// <param name="code">Optional top-level code for the aggregate error when more than one result fails.</param>
    /// <param name="message">Optional top-level message for the aggregate error when more than one result fails.</param>
    public static Result Combine(
        Result result1,
        Result result2,
        Result result3,
        Result result4,
        Result result5,
        string? code = null,
        string? message = null)
    {
        if (result1.IsSuccess && result2.IsSuccess && result3.IsSuccess && result4.IsSuccess && result5.IsSuccess)
        {
            return Success();
        }

        var errors = new List<Error>(5);
        if (result1.IsFailure) { errors.Add(result1.Error); }
        if (result2.IsFailure) { errors.Add(result2.Error); }
        if (result3.IsFailure) { errors.Add(result3.Error); }
        if (result4.IsFailure) { errors.Add(result4.Error); }
        if (result5.IsFailure) { errors.Add(result5.Error); }

        return Failure(AggregateErrors(errors, code, message));
    }

    /// <summary>
    /// Combines 6 valueless results into one. Succeeds only if every result succeeds; otherwise
    /// collects the failures. A single failure is returned unchanged, while multiple failures are
    /// wrapped in an <see cref="ErrorType.Aggregate"/> error.
    /// </summary>
    /// <param name="result1">The first result to combine.</param>
    /// <param name="result2">The second result to combine.</param>
    /// <param name="result3">The third result to combine.</param>
    /// <param name="result4">The fourth result to combine.</param>
    /// <param name="result5">The fifth result to combine.</param>
    /// <param name="result6">The sixth result to combine.</param>
    /// <param name="code">Optional top-level code for the aggregate error when more than one result fails.</param>
    /// <param name="message">Optional top-level message for the aggregate error when more than one result fails.</param>
    public static Result Combine(
        Result result1,
        Result result2,
        Result result3,
        Result result4,
        Result result5,
        Result result6,
        string? code = null,
        string? message = null)
    {
        if (result1.IsSuccess && result2.IsSuccess && result3.IsSuccess && result4.IsSuccess && result5.IsSuccess && result6.IsSuccess)
        {
            return Success();
        }

        var errors = new List<Error>(6);
        if (result1.IsFailure) { errors.Add(result1.Error); }
        if (result2.IsFailure) { errors.Add(result2.Error); }
        if (result3.IsFailure) { errors.Add(result3.Error); }
        if (result4.IsFailure) { errors.Add(result4.Error); }
        if (result5.IsFailure) { errors.Add(result5.Error); }
        if (result6.IsFailure) { errors.Add(result6.Error); }

        return Failure(AggregateErrors(errors, code, message));
    }

    /// <summary>
    /// Combines 7 valueless results into one. Succeeds only if every result succeeds; otherwise
    /// collects the failures. A single failure is returned unchanged, while multiple failures are
    /// wrapped in an <see cref="ErrorType.Aggregate"/> error.
    /// </summary>
    /// <param name="result1">The first result to combine.</param>
    /// <param name="result2">The second result to combine.</param>
    /// <param name="result3">The third result to combine.</param>
    /// <param name="result4">The fourth result to combine.</param>
    /// <param name="result5">The fifth result to combine.</param>
    /// <param name="result6">The sixth result to combine.</param>
    /// <param name="result7">The seventh result to combine.</param>
    /// <param name="code">Optional top-level code for the aggregate error when more than one result fails.</param>
    /// <param name="message">Optional top-level message for the aggregate error when more than one result fails.</param>
    public static Result Combine(
        Result result1,
        Result result2,
        Result result3,
        Result result4,
        Result result5,
        Result result6,
        Result result7,
        string? code = null,
        string? message = null)
    {
        if (result1.IsSuccess && result2.IsSuccess && result3.IsSuccess && result4.IsSuccess && result5.IsSuccess && result6.IsSuccess && result7.IsSuccess)
        {
            return Success();
        }

        var errors = new List<Error>(7);
        if (result1.IsFailure) { errors.Add(result1.Error); }
        if (result2.IsFailure) { errors.Add(result2.Error); }
        if (result3.IsFailure) { errors.Add(result3.Error); }
        if (result4.IsFailure) { errors.Add(result4.Error); }
        if (result5.IsFailure) { errors.Add(result5.Error); }
        if (result6.IsFailure) { errors.Add(result6.Error); }
        if (result7.IsFailure) { errors.Add(result7.Error); }

        return Failure(AggregateErrors(errors, code, message));
    }

    /// <summary>
    /// Combines 8 valueless results into one. Succeeds only if every result succeeds; otherwise
    /// collects the failures. A single failure is returned unchanged, while multiple failures are
    /// wrapped in an <see cref="ErrorType.Aggregate"/> error.
    /// </summary>
    /// <param name="result1">The first result to combine.</param>
    /// <param name="result2">The second result to combine.</param>
    /// <param name="result3">The third result to combine.</param>
    /// <param name="result4">The fourth result to combine.</param>
    /// <param name="result5">The fifth result to combine.</param>
    /// <param name="result6">The sixth result to combine.</param>
    /// <param name="result7">The seventh result to combine.</param>
    /// <param name="result8">The eighth result to combine.</param>
    /// <param name="code">Optional top-level code for the aggregate error when more than one result fails.</param>
    /// <param name="message">Optional top-level message for the aggregate error when more than one result fails.</param>
    public static Result Combine(
        Result result1,
        Result result2,
        Result result3,
        Result result4,
        Result result5,
        Result result6,
        Result result7,
        Result result8,
        string? code = null,
        string? message = null)
    {
        if (result1.IsSuccess && result2.IsSuccess && result3.IsSuccess && result4.IsSuccess && result5.IsSuccess && result6.IsSuccess && result7.IsSuccess && result8.IsSuccess)
        {
            return Success();
        }

        var errors = new List<Error>(8);
        if (result1.IsFailure) { errors.Add(result1.Error); }
        if (result2.IsFailure) { errors.Add(result2.Error); }
        if (result3.IsFailure) { errors.Add(result3.Error); }
        if (result4.IsFailure) { errors.Add(result4.Error); }
        if (result5.IsFailure) { errors.Add(result5.Error); }
        if (result6.IsFailure) { errors.Add(result6.Error); }
        if (result7.IsFailure) { errors.Add(result7.Error); }
        if (result8.IsFailure) { errors.Add(result8.Error); }

        return Failure(AggregateErrors(errors, code, message));
    }

    #endregion

    #region Combine (Result<T>)

    /// <summary>
    /// Combines 2 results into a single result whose value is a tuple of all 2 values.
    /// Succeeds only if every result succeeds; otherwise collects the failures. A single failure
    /// is returned unchanged, while multiple failures are wrapped in an <see cref="ErrorType.Aggregate"/> error.
    /// </summary>
    /// <typeparam name="T1">The value type of the first result.</typeparam>
    /// <typeparam name="T2">The value type of the second result.</typeparam>
    /// <param name="result1">The first result to combine.</param>
    /// <param name="result2">The second result to combine.</param>
    /// <param name="code">Optional top-level code for the aggregate error when more than one result fails.</param>
    /// <param name="message">Optional top-level message for the aggregate error when more than one result fails.</param>
    public static Result<(T1, T2)> Combine<T1, T2>(
        Result<T1> result1,
        Result<T2> result2,
        string? code = null,
        string? message = null)
    {
        if (result1.IsSuccess && result2.IsSuccess)
        {
            return Result<(T1, T2)>.Success((result1.Value, result2.Value));
        }

        var errors = new List<Error>(2);
        if (result1.IsFailure) { errors.Add(result1.Error); }
        if (result2.IsFailure) { errors.Add(result2.Error); }

        return Result<(T1, T2)>.Failure(AggregateErrors(errors, code, message));
    }

    /// <summary>
    /// Combines 3 results into a single result whose value is a tuple of all 3 values.
    /// Succeeds only if every result succeeds; otherwise collects the failures. A single failure
    /// is returned unchanged, while multiple failures are wrapped in an <see cref="ErrorType.Aggregate"/> error.
    /// </summary>
    /// <typeparam name="T1">The value type of the first result.</typeparam>
    /// <typeparam name="T2">The value type of the second result.</typeparam>
    /// <typeparam name="T3">The value type of the third result.</typeparam>
    /// <param name="result1">The first result to combine.</param>
    /// <param name="result2">The second result to combine.</param>
    /// <param name="result3">The third result to combine.</param>
    /// <param name="code">Optional top-level code for the aggregate error when more than one result fails.</param>
    /// <param name="message">Optional top-level message for the aggregate error when more than one result fails.</param>
    public static Result<(T1, T2, T3)> Combine<T1, T2, T3>(
        Result<T1> result1,
        Result<T2> result2,
        Result<T3> result3,
        string? code = null,
        string? message = null)
    {
        if (result1.IsSuccess && result2.IsSuccess && result3.IsSuccess)
        {
            return Result<(T1, T2, T3)>.Success((result1.Value, result2.Value, result3.Value));
        }

        var errors = new List<Error>(3);
        if (result1.IsFailure) { errors.Add(result1.Error); }
        if (result2.IsFailure) { errors.Add(result2.Error); }
        if (result3.IsFailure) { errors.Add(result3.Error); }

        return Result<(T1, T2, T3)>.Failure(AggregateErrors(errors, code, message));
    }

    /// <summary>
    /// Combines 4 results into a single result whose value is a tuple of all 4 values.
    /// Succeeds only if every result succeeds; otherwise collects the failures. A single failure
    /// is returned unchanged, while multiple failures are wrapped in an <see cref="ErrorType.Aggregate"/> error.
    /// </summary>
    /// <typeparam name="T1">The value type of the first result.</typeparam>
    /// <typeparam name="T2">The value type of the second result.</typeparam>
    /// <typeparam name="T3">The value type of the third result.</typeparam>
    /// <typeparam name="T4">The value type of the fourth result.</typeparam>
    /// <param name="result1">The first result to combine.</param>
    /// <param name="result2">The second result to combine.</param>
    /// <param name="result3">The third result to combine.</param>
    /// <param name="result4">The fourth result to combine.</param>
    /// <param name="code">Optional top-level code for the aggregate error when more than one result fails.</param>
    /// <param name="message">Optional top-level message for the aggregate error when more than one result fails.</param>
    public static Result<(T1, T2, T3, T4)> Combine<T1, T2, T3, T4>(
        Result<T1> result1,
        Result<T2> result2,
        Result<T3> result3,
        Result<T4> result4,
        string? code = null,
        string? message = null)
    {
        if (result1.IsSuccess && result2.IsSuccess && result3.IsSuccess && result4.IsSuccess)
        {
            return Result<(T1, T2, T3, T4)>.Success((result1.Value, result2.Value, result3.Value, result4.Value));
        }

        var errors = new List<Error>(4);
        if (result1.IsFailure) { errors.Add(result1.Error); }
        if (result2.IsFailure) { errors.Add(result2.Error); }
        if (result3.IsFailure) { errors.Add(result3.Error); }
        if (result4.IsFailure) { errors.Add(result4.Error); }

        return Result<(T1, T2, T3, T4)>.Failure(AggregateErrors(errors, code, message));
    }

    /// <summary>
    /// Combines 5 results into a single result whose value is a tuple of all 5 values.
    /// Succeeds only if every result succeeds; otherwise collects the failures. A single failure
    /// is returned unchanged, while multiple failures are wrapped in an <see cref="ErrorType.Aggregate"/> error.
    /// </summary>
    /// <typeparam name="T1">The value type of the first result.</typeparam>
    /// <typeparam name="T2">The value type of the second result.</typeparam>
    /// <typeparam name="T3">The value type of the third result.</typeparam>
    /// <typeparam name="T4">The value type of the fourth result.</typeparam>
    /// <typeparam name="T5">The value type of the fifth result.</typeparam>
    /// <param name="result1">The first result to combine.</param>
    /// <param name="result2">The second result to combine.</param>
    /// <param name="result3">The third result to combine.</param>
    /// <param name="result4">The fourth result to combine.</param>
    /// <param name="result5">The fifth result to combine.</param>
    /// <param name="code">Optional top-level code for the aggregate error when more than one result fails.</param>
    /// <param name="message">Optional top-level message for the aggregate error when more than one result fails.</param>
    public static Result<(T1, T2, T3, T4, T5)> Combine<T1, T2, T3, T4, T5>(
        Result<T1> result1,
        Result<T2> result2,
        Result<T3> result3,
        Result<T4> result4,
        Result<T5> result5,
        string? code = null,
        string? message = null)
    {
        if (result1.IsSuccess && result2.IsSuccess && result3.IsSuccess && result4.IsSuccess && result5.IsSuccess)
        {
            return Result<(T1, T2, T3, T4, T5)>.Success((result1.Value, result2.Value, result3.Value, result4.Value, result5.Value));
        }

        var errors = new List<Error>(5);
        if (result1.IsFailure) { errors.Add(result1.Error); }
        if (result2.IsFailure) { errors.Add(result2.Error); }
        if (result3.IsFailure) { errors.Add(result3.Error); }
        if (result4.IsFailure) { errors.Add(result4.Error); }
        if (result5.IsFailure) { errors.Add(result5.Error); }

        return Result<(T1, T2, T3, T4, T5)>.Failure(AggregateErrors(errors, code, message));
    }

    /// <summary>
    /// Combines 6 results into a single result whose value is a tuple of all 6 values.
    /// Succeeds only if every result succeeds; otherwise collects the failures. A single failure
    /// is returned unchanged, while multiple failures are wrapped in an <see cref="ErrorType.Aggregate"/> error.
    /// </summary>
    /// <typeparam name="T1">The value type of the first result.</typeparam>
    /// <typeparam name="T2">The value type of the second result.</typeparam>
    /// <typeparam name="T3">The value type of the third result.</typeparam>
    /// <typeparam name="T4">The value type of the fourth result.</typeparam>
    /// <typeparam name="T5">The value type of the fifth result.</typeparam>
    /// <typeparam name="T6">The value type of the sixth result.</typeparam>
    /// <param name="result1">The first result to combine.</param>
    /// <param name="result2">The second result to combine.</param>
    /// <param name="result3">The third result to combine.</param>
    /// <param name="result4">The fourth result to combine.</param>
    /// <param name="result5">The fifth result to combine.</param>
    /// <param name="result6">The sixth result to combine.</param>
    /// <param name="code">Optional top-level code for the aggregate error when more than one result fails.</param>
    /// <param name="message">Optional top-level message for the aggregate error when more than one result fails.</param>
    public static Result<(T1, T2, T3, T4, T5, T6)> Combine<T1, T2, T3, T4, T5, T6>(
        Result<T1> result1,
        Result<T2> result2,
        Result<T3> result3,
        Result<T4> result4,
        Result<T5> result5,
        Result<T6> result6,
        string? code = null,
        string? message = null)
    {
        if (result1.IsSuccess && result2.IsSuccess && result3.IsSuccess && result4.IsSuccess && result5.IsSuccess && result6.IsSuccess)
        {
            return Result<(T1, T2, T3, T4, T5, T6)>.Success((result1.Value, result2.Value, result3.Value, result4.Value, result5.Value, result6.Value));
        }

        var errors = new List<Error>(6);
        if (result1.IsFailure) { errors.Add(result1.Error); }
        if (result2.IsFailure) { errors.Add(result2.Error); }
        if (result3.IsFailure) { errors.Add(result3.Error); }
        if (result4.IsFailure) { errors.Add(result4.Error); }
        if (result5.IsFailure) { errors.Add(result5.Error); }
        if (result6.IsFailure) { errors.Add(result6.Error); }

        return Result<(T1, T2, T3, T4, T5, T6)>.Failure(AggregateErrors(errors, code, message));
    }

    /// <summary>
    /// Combines 7 results into a single result whose value is a tuple of all 7 values.
    /// Succeeds only if every result succeeds; otherwise collects the failures. A single failure
    /// is returned unchanged, while multiple failures are wrapped in an <see cref="ErrorType.Aggregate"/> error.
    /// </summary>
    /// <typeparam name="T1">The value type of the first result.</typeparam>
    /// <typeparam name="T2">The value type of the second result.</typeparam>
    /// <typeparam name="T3">The value type of the third result.</typeparam>
    /// <typeparam name="T4">The value type of the fourth result.</typeparam>
    /// <typeparam name="T5">The value type of the fifth result.</typeparam>
    /// <typeparam name="T6">The value type of the sixth result.</typeparam>
    /// <typeparam name="T7">The value type of the seventh result.</typeparam>
    /// <param name="result1">The first result to combine.</param>
    /// <param name="result2">The second result to combine.</param>
    /// <param name="result3">The third result to combine.</param>
    /// <param name="result4">The fourth result to combine.</param>
    /// <param name="result5">The fifth result to combine.</param>
    /// <param name="result6">The sixth result to combine.</param>
    /// <param name="result7">The seventh result to combine.</param>
    /// <param name="code">Optional top-level code for the aggregate error when more than one result fails.</param>
    /// <param name="message">Optional top-level message for the aggregate error when more than one result fails.</param>
    public static Result<(T1, T2, T3, T4, T5, T6, T7)> Combine<T1, T2, T3, T4, T5, T6, T7>(
        Result<T1> result1,
        Result<T2> result2,
        Result<T3> result3,
        Result<T4> result4,
        Result<T5> result5,
        Result<T6> result6,
        Result<T7> result7,
        string? code = null,
        string? message = null)
    {
        if (result1.IsSuccess && result2.IsSuccess && result3.IsSuccess && result4.IsSuccess && result5.IsSuccess && result6.IsSuccess && result7.IsSuccess)
        {
            return Result<(T1, T2, T3, T4, T5, T6, T7)>.Success((result1.Value, result2.Value, result3.Value, result4.Value, result5.Value, result6.Value, result7.Value));
        }

        var errors = new List<Error>(7);
        if (result1.IsFailure) { errors.Add(result1.Error); }
        if (result2.IsFailure) { errors.Add(result2.Error); }
        if (result3.IsFailure) { errors.Add(result3.Error); }
        if (result4.IsFailure) { errors.Add(result4.Error); }
        if (result5.IsFailure) { errors.Add(result5.Error); }
        if (result6.IsFailure) { errors.Add(result6.Error); }
        if (result7.IsFailure) { errors.Add(result7.Error); }

        return Result<(T1, T2, T3, T4, T5, T6, T7)>.Failure(AggregateErrors(errors, code, message));
    }

    /// <summary>
    /// Combines 8 results into a single result whose value is a tuple of all 8 values.
    /// Succeeds only if every result succeeds; otherwise collects the failures. A single failure
    /// is returned unchanged, while multiple failures are wrapped in an <see cref="ErrorType.Aggregate"/> error.
    /// </summary>
    /// <typeparam name="T1">The value type of the first result.</typeparam>
    /// <typeparam name="T2">The value type of the second result.</typeparam>
    /// <typeparam name="T3">The value type of the third result.</typeparam>
    /// <typeparam name="T4">The value type of the fourth result.</typeparam>
    /// <typeparam name="T5">The value type of the fifth result.</typeparam>
    /// <typeparam name="T6">The value type of the sixth result.</typeparam>
    /// <typeparam name="T7">The value type of the seventh result.</typeparam>
    /// <typeparam name="T8">The value type of the eighth result.</typeparam>
    /// <param name="result1">The first result to combine.</param>
    /// <param name="result2">The second result to combine.</param>
    /// <param name="result3">The third result to combine.</param>
    /// <param name="result4">The fourth result to combine.</param>
    /// <param name="result5">The fifth result to combine.</param>
    /// <param name="result6">The sixth result to combine.</param>
    /// <param name="result7">The seventh result to combine.</param>
    /// <param name="result8">The eighth result to combine.</param>
    /// <param name="code">Optional top-level code for the aggregate error when more than one result fails.</param>
    /// <param name="message">Optional top-level message for the aggregate error when more than one result fails.</param>
    public static Result<(T1, T2, T3, T4, T5, T6, T7, T8)> Combine<T1, T2, T3, T4, T5, T6, T7, T8>(
        Result<T1> result1,
        Result<T2> result2,
        Result<T3> result3,
        Result<T4> result4,
        Result<T5> result5,
        Result<T6> result6,
        Result<T7> result7,
        Result<T8> result8,
        string? code = null,
        string? message = null)
    {
        if (result1.IsSuccess && result2.IsSuccess && result3.IsSuccess && result4.IsSuccess && result5.IsSuccess && result6.IsSuccess && result7.IsSuccess && result8.IsSuccess)
        {
            return Result<(T1, T2, T3, T4, T5, T6, T7, T8)>.Success((result1.Value, result2.Value, result3.Value, result4.Value, result5.Value, result6.Value, result7.Value, result8.Value));
        }

        var errors = new List<Error>(8);
        if (result1.IsFailure) { errors.Add(result1.Error); }
        if (result2.IsFailure) { errors.Add(result2.Error); }
        if (result3.IsFailure) { errors.Add(result3.Error); }
        if (result4.IsFailure) { errors.Add(result4.Error); }
        if (result5.IsFailure) { errors.Add(result5.Error); }
        if (result6.IsFailure) { errors.Add(result6.Error); }
        if (result7.IsFailure) { errors.Add(result7.Error); }
        if (result8.IsFailure) { errors.Add(result8.Error); }

        return Result<(T1, T2, T3, T4, T5, T6, T7, T8)>.Failure(AggregateErrors(errors, code, message));
    }

    #endregion

    #region Combine (Result<T, TError>)

    /// <summary>
    /// Combines 2 results sharing an error type into a single result whose value is a tuple of all 2 values.
    /// Succeeds only if every result succeeds; otherwise the failed errors are passed to
    /// <paramref name="aggregator"/>, which produces the single error returned.
    /// </summary>
    /// <typeparam name="T1">The value type of the first result.</typeparam>
    /// <typeparam name="T2">The value type of the second result.</typeparam>
    /// <typeparam name="TError">The error type shared by all results.</typeparam>
    /// <param name="result1">The first result to combine.</param>
    /// <param name="result2">The second result to combine.</param>
    /// <param name="aggregator">Combines the collected failures into a single error.</param>
    public static Result<(T1, T2), TError> Combine<T1, T2, TError>(
        Result<T1, TError> result1,
        Result<T2, TError> result2,
        Func<IReadOnlyList<TError>, TError> aggregator)
    {
        if (result1.IsSuccess && result2.IsSuccess)
        {
            return Result<(T1, T2), TError>.Success((result1.Value, result2.Value));
        }

        var errors = new List<TError>(2);
        if (result1.IsFailure) { errors.Add(result1.Error); }
        if (result2.IsFailure) { errors.Add(result2.Error); }

        return Result<(T1, T2), TError>.Failure(aggregator(errors));
    }

    /// <summary>
    /// Combines 3 results sharing an error type into a single result whose value is a tuple of all 3 values.
    /// Succeeds only if every result succeeds; otherwise the failed errors are passed to
    /// <paramref name="aggregator"/>, which produces the single error returned.
    /// </summary>
    /// <typeparam name="T1">The value type of the first result.</typeparam>
    /// <typeparam name="T2">The value type of the second result.</typeparam>
    /// <typeparam name="T3">The value type of the third result.</typeparam>
    /// <typeparam name="TError">The error type shared by all results.</typeparam>
    /// <param name="result1">The first result to combine.</param>
    /// <param name="result2">The second result to combine.</param>
    /// <param name="result3">The third result to combine.</param>
    /// <param name="aggregator">Combines the collected failures into a single error.</param>
    public static Result<(T1, T2, T3), TError> Combine<T1, T2, T3, TError>(
        Result<T1, TError> result1,
        Result<T2, TError> result2,
        Result<T3, TError> result3,
        Func<IReadOnlyList<TError>, TError> aggregator)
    {
        if (result1.IsSuccess && result2.IsSuccess && result3.IsSuccess)
        {
            return Result<(T1, T2, T3), TError>.Success((result1.Value, result2.Value, result3.Value));
        }

        var errors = new List<TError>(3);
        if (result1.IsFailure) { errors.Add(result1.Error); }
        if (result2.IsFailure) { errors.Add(result2.Error); }
        if (result3.IsFailure) { errors.Add(result3.Error); }

        return Result<(T1, T2, T3), TError>.Failure(aggregator(errors));
    }

    /// <summary>
    /// Combines 4 results sharing an error type into a single result whose value is a tuple of all 4 values.
    /// Succeeds only if every result succeeds; otherwise the failed errors are passed to
    /// <paramref name="aggregator"/>, which produces the single error returned.
    /// </summary>
    /// <typeparam name="T1">The value type of the first result.</typeparam>
    /// <typeparam name="T2">The value type of the second result.</typeparam>
    /// <typeparam name="T3">The value type of the third result.</typeparam>
    /// <typeparam name="T4">The value type of the fourth result.</typeparam>
    /// <typeparam name="TError">The error type shared by all results.</typeparam>
    /// <param name="result1">The first result to combine.</param>
    /// <param name="result2">The second result to combine.</param>
    /// <param name="result3">The third result to combine.</param>
    /// <param name="result4">The fourth result to combine.</param>
    /// <param name="aggregator">Combines the collected failures into a single error.</param>
    public static Result<(T1, T2, T3, T4), TError> Combine<T1, T2, T3, T4, TError>(
        Result<T1, TError> result1,
        Result<T2, TError> result2,
        Result<T3, TError> result3,
        Result<T4, TError> result4,
        Func<IReadOnlyList<TError>, TError> aggregator)
    {
        if (result1.IsSuccess && result2.IsSuccess && result3.IsSuccess && result4.IsSuccess)
        {
            return Result<(T1, T2, T3, T4), TError>.Success((result1.Value, result2.Value, result3.Value, result4.Value));
        }

        var errors = new List<TError>(4);
        if (result1.IsFailure) { errors.Add(result1.Error); }
        if (result2.IsFailure) { errors.Add(result2.Error); }
        if (result3.IsFailure) { errors.Add(result3.Error); }
        if (result4.IsFailure) { errors.Add(result4.Error); }

        return Result<(T1, T2, T3, T4), TError>.Failure(aggregator(errors));
    }

    /// <summary>
    /// Combines 5 results sharing an error type into a single result whose value is a tuple of all 5 values.
    /// Succeeds only if every result succeeds; otherwise the failed errors are passed to
    /// <paramref name="aggregator"/>, which produces the single error returned.
    /// </summary>
    /// <typeparam name="T1">The value type of the first result.</typeparam>
    /// <typeparam name="T2">The value type of the second result.</typeparam>
    /// <typeparam name="T3">The value type of the third result.</typeparam>
    /// <typeparam name="T4">The value type of the fourth result.</typeparam>
    /// <typeparam name="T5">The value type of the fifth result.</typeparam>
    /// <typeparam name="TError">The error type shared by all results.</typeparam>
    /// <param name="result1">The first result to combine.</param>
    /// <param name="result2">The second result to combine.</param>
    /// <param name="result3">The third result to combine.</param>
    /// <param name="result4">The fourth result to combine.</param>
    /// <param name="result5">The fifth result to combine.</param>
    /// <param name="aggregator">Combines the collected failures into a single error.</param>
    public static Result<(T1, T2, T3, T4, T5), TError> Combine<T1, T2, T3, T4, T5, TError>(
        Result<T1, TError> result1,
        Result<T2, TError> result2,
        Result<T3, TError> result3,
        Result<T4, TError> result4,
        Result<T5, TError> result5,
        Func<IReadOnlyList<TError>, TError> aggregator)
    {
        if (result1.IsSuccess && result2.IsSuccess && result3.IsSuccess && result4.IsSuccess && result5.IsSuccess)
        {
            return Result<(T1, T2, T3, T4, T5), TError>.Success((result1.Value, result2.Value, result3.Value, result4.Value, result5.Value));
        }

        var errors = new List<TError>(5);
        if (result1.IsFailure) { errors.Add(result1.Error); }
        if (result2.IsFailure) { errors.Add(result2.Error); }
        if (result3.IsFailure) { errors.Add(result3.Error); }
        if (result4.IsFailure) { errors.Add(result4.Error); }
        if (result5.IsFailure) { errors.Add(result5.Error); }

        return Result<(T1, T2, T3, T4, T5), TError>.Failure(aggregator(errors));
    }

    /// <summary>
    /// Combines 6 results sharing an error type into a single result whose value is a tuple of all 6 values.
    /// Succeeds only if every result succeeds; otherwise the failed errors are passed to
    /// <paramref name="aggregator"/>, which produces the single error returned.
    /// </summary>
    /// <typeparam name="T1">The value type of the first result.</typeparam>
    /// <typeparam name="T2">The value type of the second result.</typeparam>
    /// <typeparam name="T3">The value type of the third result.</typeparam>
    /// <typeparam name="T4">The value type of the fourth result.</typeparam>
    /// <typeparam name="T5">The value type of the fifth result.</typeparam>
    /// <typeparam name="T6">The value type of the sixth result.</typeparam>
    /// <typeparam name="TError">The error type shared by all results.</typeparam>
    /// <param name="result1">The first result to combine.</param>
    /// <param name="result2">The second result to combine.</param>
    /// <param name="result3">The third result to combine.</param>
    /// <param name="result4">The fourth result to combine.</param>
    /// <param name="result5">The fifth result to combine.</param>
    /// <param name="result6">The sixth result to combine.</param>
    /// <param name="aggregator">Combines the collected failures into a single error.</param>
    public static Result<(T1, T2, T3, T4, T5, T6), TError> Combine<T1, T2, T3, T4, T5, T6, TError>(
        Result<T1, TError> result1,
        Result<T2, TError> result2,
        Result<T3, TError> result3,
        Result<T4, TError> result4,
        Result<T5, TError> result5,
        Result<T6, TError> result6,
        Func<IReadOnlyList<TError>, TError> aggregator)
    {
        if (result1.IsSuccess && result2.IsSuccess && result3.IsSuccess && result4.IsSuccess && result5.IsSuccess && result6.IsSuccess)
        {
            return Result<(T1, T2, T3, T4, T5, T6), TError>.Success((result1.Value, result2.Value, result3.Value, result4.Value, result5.Value, result6.Value));
        }

        var errors = new List<TError>(6);
        if (result1.IsFailure) { errors.Add(result1.Error); }
        if (result2.IsFailure) { errors.Add(result2.Error); }
        if (result3.IsFailure) { errors.Add(result3.Error); }
        if (result4.IsFailure) { errors.Add(result4.Error); }
        if (result5.IsFailure) { errors.Add(result5.Error); }
        if (result6.IsFailure) { errors.Add(result6.Error); }

        return Result<(T1, T2, T3, T4, T5, T6), TError>.Failure(aggregator(errors));
    }

    /// <summary>
    /// Combines 7 results sharing an error type into a single result whose value is a tuple of all 7 values.
    /// Succeeds only if every result succeeds; otherwise the failed errors are passed to
    /// <paramref name="aggregator"/>, which produces the single error returned.
    /// </summary>
    /// <typeparam name="T1">The value type of the first result.</typeparam>
    /// <typeparam name="T2">The value type of the second result.</typeparam>
    /// <typeparam name="T3">The value type of the third result.</typeparam>
    /// <typeparam name="T4">The value type of the fourth result.</typeparam>
    /// <typeparam name="T5">The value type of the fifth result.</typeparam>
    /// <typeparam name="T6">The value type of the sixth result.</typeparam>
    /// <typeparam name="T7">The value type of the seventh result.</typeparam>
    /// <typeparam name="TError">The error type shared by all results.</typeparam>
    /// <param name="result1">The first result to combine.</param>
    /// <param name="result2">The second result to combine.</param>
    /// <param name="result3">The third result to combine.</param>
    /// <param name="result4">The fourth result to combine.</param>
    /// <param name="result5">The fifth result to combine.</param>
    /// <param name="result6">The sixth result to combine.</param>
    /// <param name="result7">The seventh result to combine.</param>
    /// <param name="aggregator">Combines the collected failures into a single error.</param>
    public static Result<(T1, T2, T3, T4, T5, T6, T7), TError> Combine<T1, T2, T3, T4, T5, T6, T7, TError>(
        Result<T1, TError> result1,
        Result<T2, TError> result2,
        Result<T3, TError> result3,
        Result<T4, TError> result4,
        Result<T5, TError> result5,
        Result<T6, TError> result6,
        Result<T7, TError> result7,
        Func<IReadOnlyList<TError>, TError> aggregator)
    {
        if (result1.IsSuccess && result2.IsSuccess && result3.IsSuccess && result4.IsSuccess && result5.IsSuccess && result6.IsSuccess && result7.IsSuccess)
        {
            return Result<(T1, T2, T3, T4, T5, T6, T7), TError>.Success((result1.Value, result2.Value, result3.Value, result4.Value, result5.Value, result6.Value, result7.Value));
        }

        var errors = new List<TError>(7);
        if (result1.IsFailure) { errors.Add(result1.Error); }
        if (result2.IsFailure) { errors.Add(result2.Error); }
        if (result3.IsFailure) { errors.Add(result3.Error); }
        if (result4.IsFailure) { errors.Add(result4.Error); }
        if (result5.IsFailure) { errors.Add(result5.Error); }
        if (result6.IsFailure) { errors.Add(result6.Error); }
        if (result7.IsFailure) { errors.Add(result7.Error); }

        return Result<(T1, T2, T3, T4, T5, T6, T7), TError>.Failure(aggregator(errors));
    }

    /// <summary>
    /// Combines 8 results sharing an error type into a single result whose value is a tuple of all 8 values.
    /// Succeeds only if every result succeeds; otherwise the failed errors are passed to
    /// <paramref name="aggregator"/>, which produces the single error returned.
    /// </summary>
    /// <typeparam name="T1">The value type of the first result.</typeparam>
    /// <typeparam name="T2">The value type of the second result.</typeparam>
    /// <typeparam name="T3">The value type of the third result.</typeparam>
    /// <typeparam name="T4">The value type of the fourth result.</typeparam>
    /// <typeparam name="T5">The value type of the fifth result.</typeparam>
    /// <typeparam name="T6">The value type of the sixth result.</typeparam>
    /// <typeparam name="T7">The value type of the seventh result.</typeparam>
    /// <typeparam name="T8">The value type of the eighth result.</typeparam>
    /// <typeparam name="TError">The error type shared by all results.</typeparam>
    /// <param name="result1">The first result to combine.</param>
    /// <param name="result2">The second result to combine.</param>
    /// <param name="result3">The third result to combine.</param>
    /// <param name="result4">The fourth result to combine.</param>
    /// <param name="result5">The fifth result to combine.</param>
    /// <param name="result6">The sixth result to combine.</param>
    /// <param name="result7">The seventh result to combine.</param>
    /// <param name="result8">The eighth result to combine.</param>
    /// <param name="aggregator">Combines the collected failures into a single error.</param>
    public static Result<(T1, T2, T3, T4, T5, T6, T7, T8), TError> Combine<T1, T2, T3, T4, T5, T6, T7, T8, TError>(
        Result<T1, TError> result1,
        Result<T2, TError> result2,
        Result<T3, TError> result3,
        Result<T4, TError> result4,
        Result<T5, TError> result5,
        Result<T6, TError> result6,
        Result<T7, TError> result7,
        Result<T8, TError> result8,
        Func<IReadOnlyList<TError>, TError> aggregator)
    {
        if (result1.IsSuccess && result2.IsSuccess && result3.IsSuccess && result4.IsSuccess && result5.IsSuccess && result6.IsSuccess && result7.IsSuccess && result8.IsSuccess)
        {
            return Result<(T1, T2, T3, T4, T5, T6, T7, T8), TError>.Success((result1.Value, result2.Value, result3.Value, result4.Value, result5.Value, result6.Value, result7.Value, result8.Value));
        }

        var errors = new List<TError>(8);
        if (result1.IsFailure) { errors.Add(result1.Error); }
        if (result2.IsFailure) { errors.Add(result2.Error); }
        if (result3.IsFailure) { errors.Add(result3.Error); }
        if (result4.IsFailure) { errors.Add(result4.Error); }
        if (result5.IsFailure) { errors.Add(result5.Error); }
        if (result6.IsFailure) { errors.Add(result6.Error); }
        if (result7.IsFailure) { errors.Add(result7.Error); }
        if (result8.IsFailure) { errors.Add(result8.Error); }

        return Result<(T1, T2, T3, T4, T5, T6, T7, T8), TError>.Failure(aggregator(errors));
    }

    #endregion

    private static Error AggregateErrors(List<Error> errors, string? code, string? message)
        => errors.Count == 1
            ? errors[0]
            : Error.Aggregate(code ?? Error.DefaultAggregateCode, message ?? Error.DefaultAggregateMessage, errors.ToArray());
}
