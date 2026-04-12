namespace Haitch.Results.UnitTests;

public class AsyncResultT2ExtensionsTests
{
    private static Error TestError => Error.Failure("oops", "Something went wrong");

    #region extension(Task<Result<TValue>> source)

    // 1. Async Success / Async Failure
    [Test]
    public async Task MatchAsync_TaskSource_AsyncSuccess_AsyncFailure_invokes_success_branch_when_successful()
    {
        var resultTask = Task.FromResult(Result<int, Error>.Success(42));

        var output = await resultTask.MatchAsync(
            onSuccess: v => Task.FromResult($"value:{v}"),
            onFailure: e => Task.FromResult($"error:{e.Code}"));

        await Assert.That(output).IsEqualTo("value:42");
    }

    [Test]
    public async Task MatchAsync_TaskSource_AsyncSuccess_AsyncFailure_invokes_failure_branch_when_failed()
    {
        var resultTask = Task.FromResult(Result<int, Error>.Failure(TestError));

        var output = await resultTask.MatchAsync(
            onSuccess: v => Task.FromResult($"value:{v}"),
            onFailure: e => Task.FromResult($"error:{e.Code}"));

        await Assert.That(output).IsEqualTo("error:oops");
    }

    // 2. Async Success / Sync Failure
    [Test]
    public async Task MatchAsync_TaskSource_AsyncSuccess_SyncFailure_invokes_success_branch_when_successful()
    {
        var resultTask = Task.FromResult(Result<int, Error>.Success(42));

        var output = await resultTask.MatchAsync(
            onSuccess: v => Task.FromResult($"value:{v}"),
            onFailure: e => $"error:{e.Code}");

        await Assert.That(output).IsEqualTo("value:42");
    }

    [Test]
    public async Task MatchAsync_TaskSource_AsyncSuccess_SyncFailure_invokes_failure_branch_when_failed()
    {
        var resultTask = Task.FromResult(Result<int, Error>.Failure(TestError));

        var output = await resultTask.MatchAsync(
            onSuccess: v => Task.FromResult($"value:{v}"),
            onFailure: e => $"error:{e.Code}");

        await Assert.That(output).IsEqualTo("error:oops");
    }

    // 3. Sync Success / Async Failure
    [Test]
    public async Task MatchAsync_TaskSource_SyncSuccess_AsyncFailure_invokes_success_branch_when_successful()
    {
        var resultTask = Task.FromResult(Result<int, Error>.Success(42));

        var output = await resultTask.MatchAsync(
            onSuccess: v => $"value:{v}",
            onFailure: e => Task.FromResult($"error:{e.Code}"));

        await Assert.That(output).IsEqualTo("value:42");
    }

    [Test]
    public async Task MatchAsync_TaskSource_SyncSuccess_AsyncFailure_invokes_failure_branch_when_failed()
    {
        var resultTask = Task.FromResult(Result<int, Error>.Failure(TestError));

        var output = await resultTask.MatchAsync(
            onSuccess: v => $"value:{v}",
            onFailure: e => Task.FromResult($"error:{e.Code}"));

        await Assert.That(output).IsEqualTo("error:oops");
    }

    // 4. Sync Success / Sync Failure
    [Test]
    public async Task MatchAsync_TaskSource_SyncSuccess_SyncFailure_invokes_success_branch_when_successful()
    {
        var resultTask = Task.FromResult(Result<int, Error>.Success(42));

        var output = await resultTask.MatchAsync(
            onSuccess: v => $"value:{v}",
            onFailure: e => $"error:{e.Code}");

        await Assert.That(output).IsEqualTo("value:42");
    }

    [Test]
    public async Task MatchAsync_TaskSource_SyncSuccess_SyncFailure_invokes_failure_branch_when_failed()
    {
        var resultTask = Task.FromResult(Result<int, Error>.Failure(TestError));

        var output = await resultTask.MatchAsync(
            onSuccess: v => $"value:{v}",
            onFailure: e => $"error:{e.Code}");

        await Assert.That(output).IsEqualTo("error:oops");
    }

    #endregion

    #region extension(Result<TValue, TError> source)

    // 5. Async Success / Async Failure
    [Test]
    public async Task MatchAsync_ResultSource_AsyncSuccess_AsyncFailure_invokes_success_branch_when_successful()
    {
        var result = Result<int, Error>.Success(42);

        var output = await result.MatchAsync(
            onSuccess: v => Task.FromResult($"value:{v}"),
            onFailure: e => Task.FromResult($"error:{e.Code}"));

        await Assert.That(output).IsEqualTo("value:42");
    }

    [Test]
    public async Task MatchAsync_ResultSource_AsyncSuccess_AsyncFailure_invokes_failure_branch_when_failed()
    {
        var result = Result<int, Error>.Failure(TestError);

        var output = await result.MatchAsync(
            onSuccess: v => Task.FromResult($"value:{v}"),
            onFailure: e => Task.FromResult($"error:{e.Code}"));

        await Assert.That(output).IsEqualTo("error:oops");
    }

    // 6. Async Success / Sync Failure
    [Test]
    public async Task MatchAsync_ResultSource_AsyncSuccess_SyncFailure_invokes_success_branch_when_successful()
    {
        var result = Result<int, Error>.Success(42);

        var output = await result.MatchAsync(
            onSuccess: v => Task.FromResult($"value:{v}"),
            onFailure: e => $"error:{e.Code}");

        await Assert.That(output).IsEqualTo("value:42");
    }

    [Test]
    public async Task MatchAsync_ResultSource_AsyncSuccess_SyncFailure_invokes_failure_branch_when_failed()
    {
        var result = Result<int, Error>.Failure(TestError);

        var output = await result.MatchAsync(
            onSuccess: v => Task.FromResult($"value:{v}"),
            onFailure: e => $"error:{e.Code}");

        await Assert.That(output).IsEqualTo("error:oops");
    }

    // 7. Sync Success / Async Failure
    [Test]
    public async Task MatchAsync_ResultSource_SyncSuccess_AsyncFailure_invokes_success_branch_when_successful()
    {
        var result = Result<int, Error>.Success(42);

        var output = await result.MatchAsync(
            onSuccess: v => $"value:{v}",
            onFailure: e => Task.FromResult($"error:{e.Code}"));

        await Assert.That(output).IsEqualTo("value:42");
    }

    [Test]
    public async Task MatchAsync_ResultSource_SyncSuccess_AsyncFailure_invokes_failure_branch_when_failed()
    {
        var result = Result<int, Error>.Failure(TestError);

        var output = await result.MatchAsync(
            onSuccess: v => $"value:{v}",
            onFailure: e => Task.FromResult($"error:{e.Code}"));

        await Assert.That(output).IsEqualTo("error:oops");
    }

    // 8. Sync Success / Sync Failure
    [Test]
    public async Task MatchAsync_ResultSource_SyncSuccess_SyncFailure_invokes_success_branch_when_successful()
    {
        var result = Result<int, Error>.Success(42);

        var output = await result.MatchAsync(
            onSuccess: v => $"value:{v}",
            onFailure: e => $"error:{e.Code}");

        await Assert.That(output).IsEqualTo("value:42");
    }

    [Test]
    public async Task MatchAsync_ResultSource_SyncSuccess_SyncFailure_invokes_failure_branch_when_failed()
    {
        var result = Result<int, Error>.Failure(TestError);

        var output = await result.MatchAsync(
            onSuccess: v => $"value:{v}",
            onFailure: e => $"error:{e.Code}");

        await Assert.That(output).IsEqualTo("error:oops");
    }

    #endregion
}