namespace Haitch.Results.UnitTests;

public class AsyncResultExtensionsTests
{
    private static Error TestError => Error.Failure("oops", "Something went wrong");

    #region extension(Task<Result> source)

    // 1. Async Success / Async Failure
    [Test]
    public async Task MatchAsync_TaskSource_AsyncSuccess_AsyncFailure_invokes_success_branch_when_successful()
    {
        var resultTask = Task.FromResult(Result.Success());

        var output = await resultTask.MatchAsync(
            onSuccess: () => Task.FromResult("ok"),
            onFailure: e => Task.FromResult($"error:{e.Code}"));

        await Assert.That(output).IsEqualTo("ok");
    }

    [Test]
    public async Task MatchAsync_TaskSource_AsyncSuccess_AsyncFailure_invokes_failure_branch_when_failed()
    {
        var resultTask = Task.FromResult(Result.Failure(TestError));

        var output = await resultTask.MatchAsync(
            onSuccess: () => Task.FromResult("ok"),
            onFailure: e => Task.FromResult($"error:{e.Code}"));

        await Assert.That(output).IsEqualTo("error:oops");
    }

    // 2. Async Success / Sync Failure
    [Test]
    public async Task MatchAsync_TaskSource_AsyncSuccess_SyncFailure_invokes_success_branch_when_successful()
    {
        var resultTask = Task.FromResult(Result.Success());

        var output = await resultTask.MatchAsync(
            onSuccess: () => Task.FromResult("ok"),
            onFailure: e => $"error:{e.Code}");

        await Assert.That(output).IsEqualTo("ok");
    }

    [Test]
    public async Task MatchAsync_TaskSource_AsyncSuccess_SyncFailure_invokes_failure_branch_when_failed()
    {
        var resultTask = Task.FromResult(Result.Failure(TestError));

        var output = await resultTask.MatchAsync(
            onSuccess: () => Task.FromResult("ok"),
            onFailure: e => $"error:{e.Code}");

        await Assert.That(output).IsEqualTo("error:oops");
    }

    // 3. Sync Success / Async Failure
    [Test]
    public async Task MatchAsync_TaskSource_SyncSuccess_AsyncFailure_invokes_success_branch_when_successful()
    {
        var resultTask = Task.FromResult(Result.Success());

        var output = await resultTask.MatchAsync(
            onSuccess: () => "ok",
            onFailure: e => Task.FromResult($"error:{e.Code}"));

        await Assert.That(output).IsEqualTo("ok");
    }

    [Test]
    public async Task MatchAsync_TaskSource_SyncSuccess_AsyncFailure_invokes_failure_branch_when_failed()
    {
        var resultTask = Task.FromResult(Result.Failure(TestError));

        var output = await resultTask.MatchAsync(
            onSuccess: () => "ok",
            onFailure: e => Task.FromResult($"error:{e.Code}"));

        await Assert.That(output).IsEqualTo("error:oops");
    }

    // 4. Sync Success / Sync Failure
    [Test]
    public async Task MatchAsync_TaskSource_SyncSuccess_SyncFailure_invokes_success_branch_when_successful()
    {
        var resultTask = Task.FromResult(Result.Success());

        var output = await resultTask.MatchAsync(
            onSuccess: () => "ok",
            onFailure: e => $"error:{e.Code}");

        await Assert.That(output).IsEqualTo("ok");
    }

    [Test]
    public async Task MatchAsync_TaskSource_SyncSuccess_SyncFailure_invokes_failure_branch_when_failed()
    {
        var resultTask = Task.FromResult(Result.Failure(TestError));

        var output = await resultTask.MatchAsync(
            onSuccess: () => "ok",
            onFailure: e => $"error:{e.Code}");

        await Assert.That(output).IsEqualTo("error:oops");
    }

    #endregion

    #region extension(Result source)

    // 5. Async Success / Async Failure
    [Test]
    public async Task MatchAsync_ResultSource_AsyncSuccess_AsyncFailure_invokes_success_branch_when_successful()
    {
        var result = Result.Success();

        var output = await result.MatchAsync(
            onSuccess: () => Task.FromResult("ok"),
            onFailure: e => Task.FromResult($"error:{e.Code}"));

        await Assert.That(output).IsEqualTo("ok");
    }

    [Test]
    public async Task MatchAsync_ResultSource_AsyncSuccess_AsyncFailure_invokes_failure_branch_when_failed()
    {
        var result = Result.Failure(TestError);

        var output = await result.MatchAsync(
            onSuccess: () => Task.FromResult("ok"),
            onFailure: e => Task.FromResult($"error:{e.Code}"));

        await Assert.That(output).IsEqualTo("error:oops");
    }

    // 6. Async Success / Sync Failure
    [Test]
    public async Task MatchAsync_ResultSource_AsyncSuccess_SyncFailure_invokes_success_branch_when_successful()
    {
        var result = Result.Success();

        var output = await result.MatchAsync(
            onSuccess: () => Task.FromResult("ok"),
            onFailure: e => $"error:{e.Code}");

        await Assert.That(output).IsEqualTo("ok");
    }

    [Test]
    public async Task MatchAsync_ResultSource_AsyncSuccess_SyncFailure_invokes_failure_branch_when_failed()
    {
        var result = Result.Failure(TestError);

        var output = await result.MatchAsync(
            onSuccess: () => Task.FromResult("ok"),
            onFailure: e => $"error:{e.Code}");

        await Assert.That(output).IsEqualTo("error:oops");
    }

    // 7. Sync Success / Async Failure
    [Test]
    public async Task MatchAsync_ResultSource_SyncSuccess_AsyncFailure_invokes_success_branch_when_successful()
    {
        var result = Result.Success();

        var output = await result.MatchAsync(
            onSuccess: () => "ok",
            onFailure: e => Task.FromResult($"error:{e.Code}"));

        await Assert.That(output).IsEqualTo("ok");
    }

    [Test]
    public async Task MatchAsync_ResultSource_SyncSuccess_AsyncFailure_invokes_failure_branch_when_failed()
    {
        var result = Result.Failure(TestError);

        var output = await result.MatchAsync(
            onSuccess: () => "ok",
            onFailure: e => Task.FromResult($"error:{e.Code}"));

        await Assert.That(output).IsEqualTo("error:oops");
    }

    // 8. Sync Success / Sync Failure
    [Test]
    public async Task MatchAsync_ResultSource_SyncSuccess_SyncFailure_invokes_success_branch_when_successful()
    {
        var result = Result.Success();

        var output = await result.MatchAsync(
            onSuccess: () => "ok",
            onFailure: e => $"error:{e.Code}");

        await Assert.That(output).IsEqualTo("ok");
    }

    [Test]
    public async Task MatchAsync_ResultSource_SyncSuccess_SyncFailure_invokes_failure_branch_when_failed()
    {
        var result = Result.Failure(TestError);

        var output = await result.MatchAsync(
            onSuccess: () => "ok",
            onFailure: e => $"error:{e.Code}");

        await Assert.That(output).IsEqualTo("error:oops");
    }

    #endregion

    #region MapAsync — extension(Task<Result> source)

    [Test]
    public async Task MapAsync_TaskSource_AsyncMapper_returns_mapped_value_when_successful()
    {
        var resultTask = Task.FromResult(Result.Success());

        var output = await resultTask.MapAsync(() => Task.FromResult(42));

        await Assert.That(output.IsSuccess).IsTrue();
        await Assert.That(output.Value).IsEqualTo(42);
    }

    [Test]
    public async Task MapAsync_TaskSource_AsyncMapper_propagates_error_when_failed()
    {
        var resultTask = Task.FromResult(Result.Failure(TestError));

        var output = await resultTask.MapAsync(() => Task.FromResult(42));

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(output.Error).IsEqualTo(TestError);
    }

    [Test]
    public async Task MapAsync_TaskSource_SyncMapper_returns_mapped_value_when_successful()
    {
        var resultTask = Task.FromResult(Result.Success());

        var output = await resultTask.MapAsync(() => 42);

        await Assert.That(output.IsSuccess).IsTrue();
        await Assert.That(output.Value).IsEqualTo(42);
    }

    [Test]
    public async Task MapAsync_TaskSource_SyncMapper_propagates_error_when_failed()
    {
        var resultTask = Task.FromResult(Result.Failure(TestError));

        var output = await resultTask.MapAsync(() => 42);

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(output.Error).IsEqualTo(TestError);
    }

    #endregion

    #region MapAsync — extension(Result source)

    [Test]
    public async Task MapAsync_ResultSource_AsyncMapper_returns_mapped_value_when_successful()
    {
        var result = Result.Success();

        var output = await result.MapAsync(() => Task.FromResult(42));

        await Assert.That(output.IsSuccess).IsTrue();
        await Assert.That(output.Value).IsEqualTo(42);
    }

    [Test]
    public async Task MapAsync_ResultSource_AsyncMapper_propagates_error_when_failed()
    {
        var result = Result.Failure(TestError);

        var output = await result.MapAsync(() => Task.FromResult(42));

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(output.Error).IsEqualTo(TestError);
    }

    [Test]
    public async Task MapAsync_ResultSource_SyncMapper_returns_mapped_value_when_successful()
    {
        var result = Result.Success();

        var output = await result.MapAsync(() => 42);

        await Assert.That(output.IsSuccess).IsTrue();
        await Assert.That(output.Value).IsEqualTo(42);
    }

    [Test]
    public async Task MapAsync_ResultSource_SyncMapper_propagates_error_when_failed()
    {
        var result = Result.Failure(TestError);

        var output = await result.MapAsync(() => 42);

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(output.Error).IsEqualTo(TestError);
    }

    #endregion

    #region MapErrorAsync — extension(Task<Result> source)

    [Test]
    public async Task MapErrorAsync_TaskSource_AsyncMapper_propagates_success_when_successful()
    {
        var resultTask = Task.FromResult(Result.Success());

        var output = await resultTask.MapErrorAsync(e => Task.FromResult(Error.NotFound(e.Code, e.Message)));

        await Assert.That(output.IsSuccess).IsTrue();
    }

    [Test]
    public async Task MapErrorAsync_TaskSource_AsyncMapper_transforms_error_when_failed()
    {
        var resultTask = Task.FromResult(Result.Failure(TestError));

        var output = await resultTask.MapErrorAsync(e => Task.FromResult(Error.NotFound(e.Code, e.Message)));

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(output.Error.Type).IsEqualTo(ErrorType.NotFound);
        await Assert.That(output.Error.Code).IsEqualTo("oops");
    }

    [Test]
    public async Task MapErrorAsync_TaskSource_SyncMapper_propagates_success_when_successful()
    {
        var resultTask = Task.FromResult(Result.Success());

        var output = await resultTask.MapErrorAsync(e => Error.NotFound(e.Code, e.Message));

        await Assert.That(output.IsSuccess).IsTrue();
    }

    [Test]
    public async Task MapErrorAsync_TaskSource_SyncMapper_transforms_error_when_failed()
    {
        var resultTask = Task.FromResult(Result.Failure(TestError));

        var output = await resultTask.MapErrorAsync(e => Error.NotFound(e.Code, e.Message));

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(output.Error.Type).IsEqualTo(ErrorType.NotFound);
        await Assert.That(output.Error.Code).IsEqualTo("oops");
    }

    #endregion

    #region MapErrorAsync — extension(Result source)

    [Test]
    public async Task MapErrorAsync_ResultSource_AsyncMapper_propagates_success_when_successful()
    {
        var result = Result.Success();

        var output = await result.MapErrorAsync(e => Task.FromResult(Error.NotFound(e.Code, e.Message)));

        await Assert.That(output.IsSuccess).IsTrue();
    }

    [Test]
    public async Task MapErrorAsync_ResultSource_AsyncMapper_transforms_error_when_failed()
    {
        var result = Result.Failure(TestError);

        var output = await result.MapErrorAsync(e => Task.FromResult(Error.NotFound(e.Code, e.Message)));

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(output.Error.Type).IsEqualTo(ErrorType.NotFound);
        await Assert.That(output.Error.Code).IsEqualTo("oops");
    }

    [Test]
    public async Task MapErrorAsync_ResultSource_SyncMapper_propagates_success_when_successful()
    {
        var result = Result.Success();

        var output = await result.MapErrorAsync(e => Error.NotFound(e.Code, e.Message));

        await Assert.That(output.IsSuccess).IsTrue();
    }

    [Test]
    public async Task MapErrorAsync_ResultSource_SyncMapper_transforms_error_when_failed()
    {
        var result = Result.Failure(TestError);

        var output = await result.MapErrorAsync(e => Error.NotFound(e.Code, e.Message));

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(output.Error.Type).IsEqualTo(ErrorType.NotFound);
        await Assert.That(output.Error.Code).IsEqualTo("oops");
    }

    #endregion

    #region BindAsync (Result) — extension(Task<Result> source)

    [Test]
    public async Task BindAsync_TaskSource_AsyncBinder_returns_bound_result_when_successful()
    {
        var resultTask = Task.FromResult(Result.Success());

        var output = await resultTask.BindAsync(() => Task.FromResult(Result.Success()));

        await Assert.That(output.IsSuccess).IsTrue();
    }

    [Test]
    public async Task BindAsync_TaskSource_AsyncBinder_propagates_error_when_failed()
    {
        var resultTask = Task.FromResult(Result.Failure(TestError));

        var output = await resultTask.BindAsync(() => Task.FromResult(Result.Success()));

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(output.Error).IsEqualTo(TestError);
    }

    [Test]
    public async Task BindAsync_TaskSource_SyncBinder_returns_bound_result_when_successful()
    {
        var resultTask = Task.FromResult(Result.Success());

        var output = await resultTask.BindAsync(() => Result.Success());

        await Assert.That(output.IsSuccess).IsTrue();
    }

    [Test]
    public async Task BindAsync_TaskSource_SyncBinder_propagates_error_when_failed()
    {
        var resultTask = Task.FromResult(Result.Failure(TestError));

        var output = await resultTask.BindAsync(() => Result.Success());

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(output.Error).IsEqualTo(TestError);
    }

    #endregion

    #region BindAsync (Result<TOut>) — extension(Task<Result> source)

    [Test]
    public async Task BindAsyncT_TaskSource_AsyncBinder_returns_bound_value_when_successful()
    {
        var resultTask = Task.FromResult(Result.Success());

        var output = await resultTask.BindAsync(() => Task.FromResult(Result<int>.Success(42)));

        await Assert.That(output.IsSuccess).IsTrue();
        await Assert.That(output.Value).IsEqualTo(42);
    }

    [Test]
    public async Task BindAsyncT_TaskSource_AsyncBinder_propagates_error_when_failed()
    {
        var resultTask = Task.FromResult(Result.Failure(TestError));

        var output = await resultTask.BindAsync(() => Task.FromResult(Result<int>.Success(42)));

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(output.Error).IsEqualTo(TestError);
    }

    [Test]
    public async Task BindAsyncT_TaskSource_SyncBinder_returns_bound_value_when_successful()
    {
        var resultTask = Task.FromResult(Result.Success());

        var output = await resultTask.BindAsync(() => Result<int>.Success(42));

        await Assert.That(output.IsSuccess).IsTrue();
        await Assert.That(output.Value).IsEqualTo(42);
    }

    [Test]
    public async Task BindAsyncT_TaskSource_SyncBinder_propagates_error_when_failed()
    {
        var resultTask = Task.FromResult(Result.Failure(TestError));

        var output = await resultTask.BindAsync(() => Result<int>.Success(42));

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(output.Error).IsEqualTo(TestError);
    }

    #endregion

    #region BindAsync (Result) — extension(Result source)

    [Test]
    public async Task BindAsync_ResultSource_AsyncBinder_returns_bound_result_when_successful()
    {
        var result = Result.Success();

        var output = await result.BindAsync(() => Task.FromResult(Result.Success()));

        await Assert.That(output.IsSuccess).IsTrue();
    }

    [Test]
    public async Task BindAsync_ResultSource_AsyncBinder_propagates_error_when_failed()
    {
        var result = Result.Failure(TestError);

        var output = await result.BindAsync(() => Task.FromResult(Result.Success()));

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(output.Error).IsEqualTo(TestError);
    }

    [Test]
    public async Task BindAsync_ResultSource_SyncBinder_returns_bound_result_when_successful()
    {
        var result = Result.Success();

        var output = await result.BindAsync(() => Result.Success());

        await Assert.That(output.IsSuccess).IsTrue();
    }

    [Test]
    public async Task BindAsync_ResultSource_SyncBinder_propagates_error_when_failed()
    {
        var result = Result.Failure(TestError);

        var output = await result.BindAsync(() => Result.Success());

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(output.Error).IsEqualTo(TestError);
    }

    #endregion

    #region BindAsync (Result<TOut>) — extension(Result source)

    [Test]
    public async Task BindAsyncT_ResultSource_AsyncBinder_returns_bound_value_when_successful()
    {
        var result = Result.Success();

        var output = await result.BindAsync(() => Task.FromResult(Result<int>.Success(42)));

        await Assert.That(output.IsSuccess).IsTrue();
        await Assert.That(output.Value).IsEqualTo(42);
    }

    [Test]
    public async Task BindAsyncT_ResultSource_AsyncBinder_propagates_error_when_failed()
    {
        var result = Result.Failure(TestError);

        var output = await result.BindAsync(() => Task.FromResult(Result<int>.Success(42)));

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(output.Error).IsEqualTo(TestError);
    }

    [Test]
    public async Task BindAsyncT_ResultSource_SyncBinder_returns_bound_value_when_successful()
    {
        var result = Result.Success();

        var output = await result.BindAsync(() => Result<int>.Success(42));

        await Assert.That(output.IsSuccess).IsTrue();
        await Assert.That(output.Value).IsEqualTo(42);
    }

    [Test]
    public async Task BindAsyncT_ResultSource_SyncBinder_propagates_error_when_failed()
    {
        var result = Result.Failure(TestError);

        var output = await result.BindAsync(() => Result<int>.Success(42));

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(output.Error).IsEqualTo(TestError);
    }

    #endregion
}