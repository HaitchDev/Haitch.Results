namespace Haitch.Results.UnitTests;

public class AsyncResultT1ExtensionsTests
{
    private static Error TestError => Error.Failure("oops", "Something went wrong");

    #region extension(Task<Result<TValue>> source)

    // 1. Async Success / Async Failure
    [Test]
    public async Task MatchAsync_TaskSource_AsyncSuccess_AsyncFailure_invokes_success_branch_when_successful()
    {
        var resultTask = Task.FromResult(Result<int>.Success(42));

        var output = await resultTask.MatchAsync(
            onSuccess: v => Task.FromResult($"value:{v}"),
            onFailure: e => Task.FromResult($"error:{e.Code}"));

        await Assert.That(output).IsEqualTo("value:42");
    }

    [Test]
    public async Task MatchAsync_TaskSource_AsyncSuccess_AsyncFailure_invokes_failure_branch_when_failed()
    {
        var resultTask = Task.FromResult(Result<int>.Failure(TestError));

        var output = await resultTask.MatchAsync(
            onSuccess: v => Task.FromResult($"value:{v}"),
            onFailure: e => Task.FromResult($"error:{e.Code}"));

        await Assert.That(output).IsEqualTo("error:oops");
    }

    // 2. Async Success / Sync Failure
    [Test]
    public async Task MatchAsync_TaskSource_AsyncSuccess_SyncFailure_invokes_success_branch_when_successful()
    {
        var resultTask = Task.FromResult(Result<int>.Success(42));

        var output = await resultTask.MatchAsync(
            onSuccess: v => Task.FromResult($"value:{v}"),
            onFailure: e => $"error:{e.Code}");

        await Assert.That(output).IsEqualTo("value:42");
    }

    [Test]
    public async Task MatchAsync_TaskSource_AsyncSuccess_SyncFailure_invokes_failure_branch_when_failed()
    {
        var resultTask = Task.FromResult(Result<int>.Failure(TestError));

        var output = await resultTask.MatchAsync(
            onSuccess: v => Task.FromResult($"value:{v}"),
            onFailure: e => $"error:{e.Code}");

        await Assert.That(output).IsEqualTo("error:oops");
    }

    // 3. Sync Success / Async Failure
    [Test]
    public async Task MatchAsync_TaskSource_SyncSuccess_AsyncFailure_invokes_success_branch_when_successful()
    {
        var resultTask = Task.FromResult(Result<int>.Success(42));

        var output = await resultTask.MatchAsync(
            onSuccess: v => $"value:{v}",
            onFailure: e => Task.FromResult($"error:{e.Code}"));

        await Assert.That(output).IsEqualTo("value:42");
    }

    [Test]
    public async Task MatchAsync_TaskSource_SyncSuccess_AsyncFailure_invokes_failure_branch_when_failed()
    {
        var resultTask = Task.FromResult(Result<int>.Failure(TestError));

        var output = await resultTask.MatchAsync(
            onSuccess: v => $"value:{v}",
            onFailure: e => Task.FromResult($"error:{e.Code}"));

        await Assert.That(output).IsEqualTo("error:oops");
    }

    // 4. Sync Success / Sync Failure
    [Test]
    public async Task MatchAsync_TaskSource_SyncSuccess_SyncFailure_invokes_success_branch_when_successful()
    {
        var resultTask = Task.FromResult(Result<int>.Success(42));

        var output = await resultTask.MatchAsync(
            onSuccess: v => $"value:{v}",
            onFailure: e => $"error:{e.Code}");

        await Assert.That(output).IsEqualTo("value:42");
    }

    [Test]
    public async Task MatchAsync_TaskSource_SyncSuccess_SyncFailure_invokes_failure_branch_when_failed()
    {
        var resultTask = Task.FromResult(Result<int>.Failure(TestError));

        var output = await resultTask.MatchAsync(
            onSuccess: v => $"value:{v}",
            onFailure: e => $"error:{e.Code}");

        await Assert.That(output).IsEqualTo("error:oops");
    }

    #endregion

    #region extension(Result<TValue> source)

    // 5. Async Success / Async Failure
    [Test]
    public async Task MatchAsync_ResultSource_AsyncSuccess_AsyncFailure_invokes_success_branch_when_successful()
    {
        var result = Result<int>.Success(42);

        var output = await result.MatchAsync(
            onSuccess: v => Task.FromResult($"value:{v}"),
            onFailure: e => Task.FromResult($"error:{e.Code}"));

        await Assert.That(output).IsEqualTo("value:42");
    }

    [Test]
    public async Task MatchAsync_ResultSource_AsyncSuccess_AsyncFailure_invokes_failure_branch_when_failed()
    {
        var result = Result<int>.Failure(TestError);

        var output = await result.MatchAsync(
            onSuccess: v => Task.FromResult($"value:{v}"),
            onFailure: e => Task.FromResult($"error:{e.Code}"));

        await Assert.That(output).IsEqualTo("error:oops");
    }

    // 6. Async Success / Sync Failure
    [Test]
    public async Task MatchAsync_ResultSource_AsyncSuccess_SyncFailure_invokes_success_branch_when_successful()
    {
        var result = Result<int>.Success(42);

        var output = await result.MatchAsync(
            onSuccess: v => Task.FromResult($"value:{v}"),
            onFailure: e => $"error:{e.Code}");

        await Assert.That(output).IsEqualTo("value:42");
    }

    [Test]
    public async Task MatchAsync_ResultSource_AsyncSuccess_SyncFailure_invokes_failure_branch_when_failed()
    {
        var result = Result<int>.Failure(TestError);

        var output = await result.MatchAsync(
            onSuccess: v => Task.FromResult($"value:{v}"),
            onFailure: e => $"error:{e.Code}");

        await Assert.That(output).IsEqualTo("error:oops");
    }

    // 7. Sync Success / Async Failure
    [Test]
    public async Task MatchAsync_ResultSource_SyncSuccess_AsyncFailure_invokes_success_branch_when_successful()
    {
        var result = Result<int>.Success(42);

        var output = await result.MatchAsync(
            onSuccess: v => $"value:{v}",
            onFailure: e => Task.FromResult($"error:{e.Code}"));

        await Assert.That(output).IsEqualTo("value:42");
    }

    [Test]
    public async Task MatchAsync_ResultSource_SyncSuccess_AsyncFailure_invokes_failure_branch_when_failed()
    {
        var result = Result<int>.Failure(TestError);

        var output = await result.MatchAsync(
            onSuccess: v => $"value:{v}",
            onFailure: e => Task.FromResult($"error:{e.Code}"));

        await Assert.That(output).IsEqualTo("error:oops");
    }

    // 8. Sync Success / Sync Failure
    [Test]
    public async Task MatchAsync_ResultSource_SyncSuccess_SyncFailure_invokes_success_branch_when_successful()
    {
        var result = Result<int>.Success(42);

        var output = await result.MatchAsync(
            onSuccess: v => $"value:{v}",
            onFailure: e => $"error:{e.Code}");

        await Assert.That(output).IsEqualTo("value:42");
    }

    [Test]
    public async Task MatchAsync_ResultSource_SyncSuccess_SyncFailure_invokes_failure_branch_when_failed()
    {
        var result = Result<int>.Failure(TestError);

        var output = await result.MatchAsync(
            onSuccess: v => $"value:{v}",
            onFailure: e => $"error:{e.Code}");

        await Assert.That(output).IsEqualTo("error:oops");
    }

    #endregion

    #region MapAsync — extension(Task<Result<TValue>> source)

    [Test]
    public async Task MapAsync_TaskSource_AsyncMapper_returns_mapped_value_when_successful()
    {
        var resultTask = Task.FromResult(Result<int>.Success(42));

        var output = await resultTask.MapAsync(v => Task.FromResult($"value:{v}"));

        await Assert.That(output.IsSuccess).IsTrue();
        await Assert.That(output.Value).IsEqualTo("value:42");
    }

    [Test]
    public async Task MapAsync_TaskSource_AsyncMapper_propagates_error_when_failed()
    {
        var resultTask = Task.FromResult(Result<int>.Failure(TestError));

        var output = await resultTask.MapAsync(v => Task.FromResult($"value:{v}"));

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(output.Error).IsEqualTo(TestError);
    }

    [Test]
    public async Task MapAsync_TaskSource_SyncMapper_returns_mapped_value_when_successful()
    {
        var resultTask = Task.FromResult(Result<int>.Success(42));

        var output = await resultTask.MapAsync(v => $"value:{v}");

        await Assert.That(output.IsSuccess).IsTrue();
        await Assert.That(output.Value).IsEqualTo("value:42");
    }

    [Test]
    public async Task MapAsync_TaskSource_SyncMapper_propagates_error_when_failed()
    {
        var resultTask = Task.FromResult(Result<int>.Failure(TestError));

        var output = await resultTask.MapAsync(v => $"value:{v}");

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(output.Error).IsEqualTo(TestError);
    }

    #endregion

    #region MapAsync — extension(Result<TValue> source)

    [Test]
    public async Task MapAsync_ResultSource_AsyncMapper_returns_mapped_value_when_successful()
    {
        var result = Result<int>.Success(42);

        var output = await result.MapAsync(v => Task.FromResult($"value:{v}"));

        await Assert.That(output.IsSuccess).IsTrue();
        await Assert.That(output.Value).IsEqualTo("value:42");
    }

    [Test]
    public async Task MapAsync_ResultSource_AsyncMapper_propagates_error_when_failed()
    {
        var result = Result<int>.Failure(TestError);

        var output = await result.MapAsync(v => Task.FromResult($"value:{v}"));

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(output.Error).IsEqualTo(TestError);
    }

    [Test]
    public async Task MapAsync_ResultSource_SyncMapper_returns_mapped_value_when_successful()
    {
        var result = Result<int>.Success(42);

        var output = await result.MapAsync(v => $"value:{v}");

        await Assert.That(output.IsSuccess).IsTrue();
        await Assert.That(output.Value).IsEqualTo("value:42");
    }

    [Test]
    public async Task MapAsync_ResultSource_SyncMapper_propagates_error_when_failed()
    {
        var result = Result<int>.Failure(TestError);

        var output = await result.MapAsync(v => $"value:{v}");

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(output.Error).IsEqualTo(TestError);
    }

    #endregion

    #region MapErrorAsync — extension(Task<Result<TValue>> source)

    [Test]
    public async Task MapErrorAsync_TaskSource_AsyncMapper_propagates_success_when_successful()
    {
        var resultTask = Task.FromResult(Result<int>.Success(42));

        var output = await resultTask.MapErrorAsync(e => Task.FromResult(Error.NotFound(e.Code, e.Message)));

        await Assert.That(output.IsSuccess).IsTrue();
        await Assert.That(output.Value).IsEqualTo(42);
    }

    [Test]
    public async Task MapErrorAsync_TaskSource_AsyncMapper_transforms_error_when_failed()
    {
        var resultTask = Task.FromResult(Result<int>.Failure(TestError));

        var output = await resultTask.MapErrorAsync(e => Task.FromResult(Error.NotFound(e.Code, e.Message)));

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(output.Error.Type).IsEqualTo(ErrorType.NotFound);
    }

    [Test]
    public async Task MapErrorAsync_TaskSource_SyncMapper_propagates_success_when_successful()
    {
        var resultTask = Task.FromResult(Result<int>.Success(42));

        var output = await resultTask.MapErrorAsync(e => Error.NotFound(e.Code, e.Message));

        await Assert.That(output.IsSuccess).IsTrue();
        await Assert.That(output.Value).IsEqualTo(42);
    }

    [Test]
    public async Task MapErrorAsync_TaskSource_SyncMapper_transforms_error_when_failed()
    {
        var resultTask = Task.FromResult(Result<int>.Failure(TestError));

        var output = await resultTask.MapErrorAsync(e => Error.NotFound(e.Code, e.Message));

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(output.Error.Type).IsEqualTo(ErrorType.NotFound);
    }

    #endregion

    #region MapErrorAsync — extension(Result<TValue> source)

    [Test]
    public async Task MapErrorAsync_ResultSource_AsyncMapper_propagates_success_when_successful()
    {
        var result = Result<int>.Success(42);

        var output = await result.MapErrorAsync(e => Task.FromResult(Error.NotFound(e.Code, e.Message)));

        await Assert.That(output.IsSuccess).IsTrue();
        await Assert.That(output.Value).IsEqualTo(42);
    }

    [Test]
    public async Task MapErrorAsync_ResultSource_AsyncMapper_transforms_error_when_failed()
    {
        var result = Result<int>.Failure(TestError);

        var output = await result.MapErrorAsync(e => Task.FromResult(Error.NotFound(e.Code, e.Message)));

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(output.Error.Type).IsEqualTo(ErrorType.NotFound);
    }

    [Test]
    public async Task MapErrorAsync_ResultSource_SyncMapper_propagates_success_when_successful()
    {
        var result = Result<int>.Success(42);

        var output = await result.MapErrorAsync(e => Error.NotFound(e.Code, e.Message));

        await Assert.That(output.IsSuccess).IsTrue();
        await Assert.That(output.Value).IsEqualTo(42);
    }

    [Test]
    public async Task MapErrorAsync_ResultSource_SyncMapper_transforms_error_when_failed()
    {
        var result = Result<int>.Failure(TestError);

        var output = await result.MapErrorAsync(e => Error.NotFound(e.Code, e.Message));

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(output.Error.Type).IsEqualTo(ErrorType.NotFound);
    }

    #endregion

    #region BindAsync — extension(Task<Result<TValue>> source)

    [Test]
    public async Task BindAsync_TaskSource_AsyncBinder_returns_bound_value_when_successful()
    {
        var resultTask = Task.FromResult(Result<int>.Success(42));

        var output = await resultTask.BindAsync(v => Task.FromResult(Result<string>.Success($"value:{v}")));

        await Assert.That(output.IsSuccess).IsTrue();
        await Assert.That(output.Value).IsEqualTo("value:42");
    }

    [Test]
    public async Task BindAsync_TaskSource_AsyncBinder_propagates_error_when_failed()
    {
        var resultTask = Task.FromResult(Result<int>.Failure(TestError));

        var output = await resultTask.BindAsync(v => Task.FromResult(Result<string>.Success($"value:{v}")));

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(output.Error).IsEqualTo(TestError);
    }

    [Test]
    public async Task BindAsync_TaskSource_SyncBinder_returns_bound_value_when_successful()
    {
        var resultTask = Task.FromResult(Result<int>.Success(42));

        var output = await resultTask.BindAsync(v => Result<string>.Success($"value:{v}"));

        await Assert.That(output.IsSuccess).IsTrue();
        await Assert.That(output.Value).IsEqualTo("value:42");
    }

    [Test]
    public async Task BindAsync_TaskSource_SyncBinder_propagates_error_when_failed()
    {
        var resultTask = Task.FromResult(Result<int>.Failure(TestError));

        var output = await resultTask.BindAsync(v => Result<string>.Success($"value:{v}"));

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(output.Error).IsEqualTo(TestError);
    }

    #endregion

    #region BindAsync — extension(Result<TValue> source)

    [Test]
    public async Task BindAsync_ResultSource_AsyncBinder_returns_bound_value_when_successful()
    {
        var result = Result<int>.Success(42);

        var output = await result.BindAsync(v => Task.FromResult(Result<string>.Success($"value:{v}")));

        await Assert.That(output.IsSuccess).IsTrue();
        await Assert.That(output.Value).IsEqualTo("value:42");
    }

    [Test]
    public async Task BindAsync_ResultSource_AsyncBinder_propagates_error_when_failed()
    {
        var result = Result<int>.Failure(TestError);

        var output = await result.BindAsync(v => Task.FromResult(Result<string>.Success($"value:{v}")));

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(output.Error).IsEqualTo(TestError);
    }

    [Test]
    public async Task BindAsync_ResultSource_SyncBinder_returns_bound_value_when_successful()
    {
        var result = Result<int>.Success(42);

        var output = await result.BindAsync(v => Result<string>.Success($"value:{v}"));

        await Assert.That(output.IsSuccess).IsTrue();
        await Assert.That(output.Value).IsEqualTo("value:42");
    }

    [Test]
    public async Task BindAsync_ResultSource_SyncBinder_propagates_error_when_failed()
    {
        var result = Result<int>.Failure(TestError);

        var output = await result.BindAsync(v => Result<string>.Success($"value:{v}"));

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(output.Error).IsEqualTo(TestError);
    }

    #endregion

    #region TapAsync — extension(Task<Result<TValue>> source)

    [Test]
    public async Task TapAsync_TaskSource_AsyncAction_invokes_action_when_successful()
    {
        var resultTask = Task.FromResult(Result<int>.Success(42));
        var tappedValue = 0;

        var output = await resultTask.TapAsync(v => { tappedValue = v; return Task.CompletedTask; });

        await Assert.That(output.IsSuccess).IsTrue();
        await Assert.That(tappedValue).IsEqualTo(42);
    }

    [Test]
    public async Task TapAsync_TaskSource_AsyncAction_does_not_invoke_action_when_failed()
    {
        var resultTask = Task.FromResult(Result<int>.Failure(TestError));
        var tapped = false;

        var output = await resultTask.TapAsync(v => { tapped = true; return Task.CompletedTask; });

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(tapped).IsFalse();
    }

    [Test]
    public async Task TapAsync_TaskSource_SyncAction_invokes_action_when_successful()
    {
        var resultTask = Task.FromResult(Result<int>.Success(42));
        var tappedValue = 0;

        var output = await resultTask.TapAsync(v => { tappedValue = v; });

        await Assert.That(output.IsSuccess).IsTrue();
        await Assert.That(tappedValue).IsEqualTo(42);
    }

    [Test]
    public async Task TapAsync_TaskSource_SyncAction_does_not_invoke_action_when_failed()
    {
        var resultTask = Task.FromResult(Result<int>.Failure(TestError));
        var tapped = false;

        var output = await resultTask.TapAsync(v => { tapped = true; });

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(tapped).IsFalse();
    }

    #endregion

    #region TapAsync — extension(Result<TValue> source)

    [Test]
    public async Task TapAsync_ResultSource_AsyncAction_invokes_action_when_successful()
    {
        var result = Result<int>.Success(42);
        var tappedValue = 0;

        var output = await result.TapAsync(v => { tappedValue = v; return Task.CompletedTask; });

        await Assert.That(output.IsSuccess).IsTrue();
        await Assert.That(tappedValue).IsEqualTo(42);
    }

    [Test]
    public async Task TapAsync_ResultSource_AsyncAction_does_not_invoke_action_when_failed()
    {
        var result = Result<int>.Failure(TestError);
        var tapped = false;

        var output = await result.TapAsync(v => { tapped = true; return Task.CompletedTask; });

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(tapped).IsFalse();
    }

    [Test]
    public async Task TapAsync_ResultSource_SyncAction_invokes_action_when_successful()
    {
        var result = Result<int>.Success(42);
        var tappedValue = 0;

        var output = await result.TapAsync(v => { tappedValue = v; });

        await Assert.That(output.IsSuccess).IsTrue();
        await Assert.That(tappedValue).IsEqualTo(42);
    }

    [Test]
    public async Task TapAsync_ResultSource_SyncAction_does_not_invoke_action_when_failed()
    {
        var result = Result<int>.Failure(TestError);
        var tapped = false;

        var output = await result.TapAsync(v => { tapped = true; });

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(tapped).IsFalse();
    }

    #endregion

    #region TapErrorAsync — extension(Task<Result<TValue>> source)

    [Test]
    public async Task TapErrorAsync_TaskSource_AsyncAction_does_not_invoke_action_when_successful()
    {
        var resultTask = Task.FromResult(Result<int>.Success(42));
        var tapped = false;

        var output = await resultTask.TapErrorAsync(e => { tapped = true; return Task.CompletedTask; });

        await Assert.That(output.IsSuccess).IsTrue();
        await Assert.That(tapped).IsFalse();
    }

    [Test]
    public async Task TapErrorAsync_TaskSource_AsyncAction_invokes_action_when_failed()
    {
        var resultTask = Task.FromResult(Result<int>.Failure(TestError));
        Error? tappedError = null;

        var output = await resultTask.TapErrorAsync(e => { tappedError = e; return Task.CompletedTask; });

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(tappedError).IsEqualTo(TestError);
    }

    [Test]
    public async Task TapErrorAsync_TaskSource_SyncAction_does_not_invoke_action_when_successful()
    {
        var resultTask = Task.FromResult(Result<int>.Success(42));
        var tapped = false;

        var output = await resultTask.TapErrorAsync(e => { tapped = true; });

        await Assert.That(output.IsSuccess).IsTrue();
        await Assert.That(tapped).IsFalse();
    }

    [Test]
    public async Task TapErrorAsync_TaskSource_SyncAction_invokes_action_when_failed()
    {
        var resultTask = Task.FromResult(Result<int>.Failure(TestError));
        Error? tappedError = null;

        var output = await resultTask.TapErrorAsync(e => { tappedError = e; });

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(tappedError).IsEqualTo(TestError);
    }

    #endregion

    #region TapErrorAsync — extension(Result<TValue> source)

    [Test]
    public async Task TapErrorAsync_ResultSource_AsyncAction_does_not_invoke_action_when_successful()
    {
        var result = Result<int>.Success(42);
        var tapped = false;

        var output = await result.TapErrorAsync(e => { tapped = true; return Task.CompletedTask; });

        await Assert.That(output.IsSuccess).IsTrue();
        await Assert.That(tapped).IsFalse();
    }

    [Test]
    public async Task TapErrorAsync_ResultSource_AsyncAction_invokes_action_when_failed()
    {
        var result = Result<int>.Failure(TestError);
        Error? tappedError = null;

        var output = await result.TapErrorAsync(e => { tappedError = e; return Task.CompletedTask; });

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(tappedError).IsEqualTo(TestError);
    }

    [Test]
    public async Task TapErrorAsync_ResultSource_SyncAction_does_not_invoke_action_when_successful()
    {
        var result = Result<int>.Success(42);
        var tapped = false;

        var output = await result.TapErrorAsync(e => { tapped = true; });

        await Assert.That(output.IsSuccess).IsTrue();
        await Assert.That(tapped).IsFalse();
    }

    [Test]
    public async Task TapErrorAsync_ResultSource_SyncAction_invokes_action_when_failed()
    {
        var result = Result<int>.Failure(TestError);
        Error? tappedError = null;

        var output = await result.TapErrorAsync(e => { tappedError = e; });

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(tappedError).IsEqualTo(TestError);
    }

    #endregion

    #region EnsureAsync — extension(Task<Result<TValue>> source)

    [Test]
    public async Task EnsureAsync_TaskSource_AsyncPredicate_returns_success_when_predicate_passes()
    {
        var resultTask = Task.FromResult(Result<int>.Success(42));

        var output = await resultTask.EnsureAsync(v => Task.FromResult(v > 0), TestError);

        await Assert.That(output.IsSuccess).IsTrue();
        await Assert.That(output.Value).IsEqualTo(42);
    }

    [Test]
    public async Task EnsureAsync_TaskSource_AsyncPredicate_returns_failure_when_predicate_fails()
    {
        var resultTask = Task.FromResult(Result<int>.Success(42));

        var output = await resultTask.EnsureAsync(v => Task.FromResult(v > 100), TestError);

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(output.Error).IsEqualTo(TestError);
    }

    [Test]
    public async Task EnsureAsync_TaskSource_AsyncPredicate_propagates_error_when_already_failed()
    {
        var resultTask = Task.FromResult(Result<int>.Failure(TestError));

        var output = await resultTask.EnsureAsync(v => Task.FromResult(true), Error.NotFound("nf", "not found"));

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(output.Error).IsEqualTo(TestError);
    }

    [Test]
    public async Task EnsureAsync_TaskSource_SyncPredicate_returns_success_when_predicate_passes()
    {
        var resultTask = Task.FromResult(Result<int>.Success(42));

        var output = await resultTask.EnsureAsync(v => v > 0, TestError);

        await Assert.That(output.IsSuccess).IsTrue();
        await Assert.That(output.Value).IsEqualTo(42);
    }

    [Test]
    public async Task EnsureAsync_TaskSource_SyncPredicate_returns_failure_when_predicate_fails()
    {
        var resultTask = Task.FromResult(Result<int>.Success(42));

        var output = await resultTask.EnsureAsync(v => v > 100, TestError);

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(output.Error).IsEqualTo(TestError);
    }

    [Test]
    public async Task EnsureAsync_TaskSource_SyncPredicate_propagates_error_when_already_failed()
    {
        var resultTask = Task.FromResult(Result<int>.Failure(TestError));

        var output = await resultTask.EnsureAsync(v => true, Error.NotFound("nf", "not found"));

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(output.Error).IsEqualTo(TestError);
    }

    #endregion

    #region EnsureAsync — extension(Result<TValue> source)

    [Test]
    public async Task EnsureAsync_ResultSource_AsyncPredicate_returns_success_when_predicate_passes()
    {
        var result = Result<int>.Success(42);

        var output = await result.EnsureAsync(v => Task.FromResult(v > 0), TestError);

        await Assert.That(output.IsSuccess).IsTrue();
        await Assert.That(output.Value).IsEqualTo(42);
    }

    [Test]
    public async Task EnsureAsync_ResultSource_AsyncPredicate_returns_failure_when_predicate_fails()
    {
        var result = Result<int>.Success(42);

        var output = await result.EnsureAsync(v => Task.FromResult(v > 100), TestError);

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(output.Error).IsEqualTo(TestError);
    }

    [Test]
    public async Task EnsureAsync_ResultSource_AsyncPredicate_propagates_error_when_already_failed()
    {
        var result = Result<int>.Failure(TestError);

        var output = await result.EnsureAsync(v => Task.FromResult(true), Error.NotFound("nf", "not found"));

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(output.Error).IsEqualTo(TestError);
    }

    [Test]
    public async Task EnsureAsync_ResultSource_SyncPredicate_returns_success_when_predicate_passes()
    {
        var result = Result<int>.Success(42);

        var output = await result.EnsureAsync(v => v > 0, TestError);

        await Assert.That(output.IsSuccess).IsTrue();
        await Assert.That(output.Value).IsEqualTo(42);
    }

    [Test]
    public async Task EnsureAsync_ResultSource_SyncPredicate_returns_failure_when_predicate_fails()
    {
        var result = Result<int>.Success(42);

        var output = await result.EnsureAsync(v => v > 100, TestError);

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(output.Error).IsEqualTo(TestError);
    }

    [Test]
    public async Task EnsureAsync_ResultSource_SyncPredicate_propagates_error_when_already_failed()
    {
        var result = Result<int>.Failure(TestError);

        var output = await result.EnsureAsync(v => true, Error.NotFound("nf", "not found"));

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(output.Error).IsEqualTo(TestError);
    }

    #endregion
}