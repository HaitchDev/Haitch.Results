using Haitch.Results.TestHelpers;

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

        await output.AssertSuccess(42);
    }

    [Test]
    public async Task MapAsync_TaskSource_AsyncMapper_propagates_error_when_failed()
    {
        var resultTask = Task.FromResult(Result.Failure(TestError));

        var output = await resultTask.MapAsync(() => Task.FromResult(42));

        await output.AssertFailure(TestError);
    }

    [Test]
    public async Task MapAsync_TaskSource_SyncMapper_returns_mapped_value_when_successful()
    {
        var resultTask = Task.FromResult(Result.Success());

        var output = await resultTask.MapAsync(() => 42);

        await output.AssertSuccess(42);
    }

    [Test]
    public async Task MapAsync_TaskSource_SyncMapper_propagates_error_when_failed()
    {
        var resultTask = Task.FromResult(Result.Failure(TestError));

        var output = await resultTask.MapAsync(() => 42);

        await output.AssertFailure(TestError);
    }

    #endregion

    #region MapAsync — extension(Result source)

    [Test]
    public async Task MapAsync_ResultSource_AsyncMapper_returns_mapped_value_when_successful()
    {
        var result = Result.Success();

        var output = await result.MapAsync(() => Task.FromResult(42));

        await output.AssertSuccess(42);
    }

    [Test]
    public async Task MapAsync_ResultSource_AsyncMapper_propagates_error_when_failed()
    {
        var result = Result.Failure(TestError);

        var output = await result.MapAsync(() => Task.FromResult(42));

        await output.AssertFailure(TestError);
    }

    [Test]
    public async Task MapAsync_ResultSource_SyncMapper_returns_mapped_value_when_successful()
    {
        var result = Result.Success();

        var output = await result.MapAsync(() => 42);

        await output.AssertSuccess(42);
    }

    [Test]
    public async Task MapAsync_ResultSource_SyncMapper_propagates_error_when_failed()
    {
        var result = Result.Failure(TestError);

        var output = await result.MapAsync(() => 42);

        await output.AssertFailure(TestError);
    }

    #endregion

    #region MapErrorAsync — extension(Task<Result> source)

    [Test]
    public async Task MapErrorAsync_TaskSource_AsyncMapper_propagates_success_when_successful()
    {
        var resultTask = Task.FromResult(Result.Success());

        var output = await resultTask.MapErrorAsync(e => Task.FromResult(Error.NotFound(e.Code, e.Message)));

        await output.AssertSuccess();
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

        await output.AssertSuccess();
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

        await output.AssertSuccess();
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

        await output.AssertSuccess();
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

        await output.AssertSuccess();
    }

    [Test]
    public async Task BindAsync_TaskSource_AsyncBinder_propagates_error_when_failed()
    {
        var resultTask = Task.FromResult(Result.Failure(TestError));

        var output = await resultTask.BindAsync(() => Task.FromResult(Result.Success()));

        await output.AssertFailure(TestError);
    }

    [Test]
    public async Task BindAsync_TaskSource_SyncBinder_returns_bound_result_when_successful()
    {
        var resultTask = Task.FromResult(Result.Success());

        var output = await resultTask.BindAsync(Result.Success);

        await output.AssertSuccess();
    }

    [Test]
    public async Task BindAsync_TaskSource_SyncBinder_propagates_error_when_failed()
    {
        var resultTask = Task.FromResult(Result.Failure(TestError));

        var output = await resultTask.BindAsync(Result.Success);

        await output.AssertFailure(TestError);
    }

    #endregion

    #region BindAsync (Result<TOut>) — extension(Task<Result> source)

    [Test]
    public async Task BindAsyncT_TaskSource_AsyncBinder_returns_bound_value_when_successful()
    {
        var resultTask = Task.FromResult(Result.Success());

        var output = await resultTask.BindAsync(() => Task.FromResult(Result<int>.Success(42)));

        await output.AssertSuccess(42);
    }

    [Test]
    public async Task BindAsyncT_TaskSource_AsyncBinder_propagates_error_when_failed()
    {
        var resultTask = Task.FromResult(Result.Failure(TestError));

        var output = await resultTask.BindAsync(() => Task.FromResult(Result<int>.Success(42)));

        await output.AssertFailure(TestError);
    }

    [Test]
    public async Task BindAsyncT_TaskSource_SyncBinder_returns_bound_value_when_successful()
    {
        var resultTask = Task.FromResult(Result.Success());

        var output = await resultTask.BindAsync(() => Result<int>.Success(42));

        await output.AssertSuccess(42);
    }

    [Test]
    public async Task BindAsyncT_TaskSource_SyncBinder_propagates_error_when_failed()
    {
        var resultTask = Task.FromResult(Result.Failure(TestError));

        var output = await resultTask.BindAsync(() => Result<int>.Success(42));

        await output.AssertFailure(TestError);
    }

    #endregion

    #region BindAsync (Result) — extension(Result source)

    [Test]
    public async Task BindAsync_ResultSource_AsyncBinder_returns_bound_result_when_successful()
    {
        var result = Result.Success();

        var output = await result.BindAsync(() => Task.FromResult(Result.Success()));

        await output.AssertSuccess();
    }

    [Test]
    public async Task BindAsync_ResultSource_AsyncBinder_propagates_error_when_failed()
    {
        var result = Result.Failure(TestError);

        var output = await result.BindAsync(() => Task.FromResult(Result.Success()));

        await output.AssertFailure(TestError);
    }

    [Test]
    public async Task BindAsync_ResultSource_SyncBinder_returns_bound_result_when_successful()
    {
        var result = Result.Success();

        var output = await result.BindAsync(Result.Success);

        await output.AssertSuccess();
    }

    [Test]
    public async Task BindAsync_ResultSource_SyncBinder_propagates_error_when_failed()
    {
        var result = Result.Failure(TestError);

        var output = await result.BindAsync(Result.Success);

        await output.AssertFailure(TestError);
    }

    #endregion

    #region BindAsync (Result<TOut>) — extension(Result source)

    [Test]
    public async Task BindAsyncT_ResultSource_AsyncBinder_returns_bound_value_when_successful()
    {
        var result = Result.Success();

        var output = await result.BindAsync(() => Task.FromResult(Result<int>.Success(42)));

        await output.AssertSuccess(42);
    }

    [Test]
    public async Task BindAsyncT_ResultSource_AsyncBinder_propagates_error_when_failed()
    {
        var result = Result.Failure(TestError);

        var output = await result.BindAsync(() => Task.FromResult(Result<int>.Success(42)));

        await output.AssertFailure(TestError);
    }

    [Test]
    public async Task BindAsyncT_ResultSource_SyncBinder_returns_bound_value_when_successful()
    {
        var result = Result.Success();

        var output = await result.BindAsync(() => Result<int>.Success(42));

        await output.AssertSuccess(42);
    }

    [Test]
    public async Task BindAsyncT_ResultSource_SyncBinder_propagates_error_when_failed()
    {
        var result = Result.Failure(TestError);

        var output = await result.BindAsync(() => Result<int>.Success(42));

        await output.AssertFailure(TestError);
    }

    #endregion

    #region TapAsync — extension(Task<Result> source)

    [Test]
    public async Task TapAsync_TaskSource_AsyncAction_invokes_action_when_successful()
    {
        var resultTask = Task.FromResult(Result.Success());
        var tapped = false;

        var output = await resultTask.TapAsync(() => { tapped = true; return Task.CompletedTask; });

        await output.AssertSuccess();
        await Assert.That(tapped).IsTrue();
    }

    [Test]
    public async Task TapAsync_TaskSource_AsyncAction_does_not_invoke_action_when_failed()
    {
        var resultTask = Task.FromResult(Result.Failure(TestError));
        var tapped = false;

        var output = await resultTask.TapAsync(() => { tapped = true; return Task.CompletedTask; });

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(tapped).IsFalse();
    }

    [Test]
    public async Task TapAsync_TaskSource_SyncAction_invokes_action_when_successful()
    {
        var resultTask = Task.FromResult(Result.Success());
        var tapped = false;

        var output = await resultTask.TapAsync(() => { tapped = true; });

        await output.AssertSuccess();
        await Assert.That(tapped).IsTrue();
    }

    [Test]
    public async Task TapAsync_TaskSource_SyncAction_does_not_invoke_action_when_failed()
    {
        var resultTask = Task.FromResult(Result.Failure(TestError));
        var tapped = false;

        var output = await resultTask.TapAsync(() => { tapped = true; });

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(tapped).IsFalse();
    }

    #endregion

    #region TapAsync — extension(Result source)

    [Test]
    public async Task TapAsync_ResultSource_AsyncAction_invokes_action_when_successful()
    {
        var result = Result.Success();
        var tapped = false;

        var output = await result.TapAsync(() => { tapped = true; return Task.CompletedTask; });

        await output.AssertSuccess();
        await Assert.That(tapped).IsTrue();
    }

    [Test]
    public async Task TapAsync_ResultSource_AsyncAction_does_not_invoke_action_when_failed()
    {
        var result = Result.Failure(TestError);
        var tapped = false;

        var output = await result.TapAsync(() => { tapped = true; return Task.CompletedTask; });

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(tapped).IsFalse();
    }

    [Test]
    public async Task TapAsync_ResultSource_SyncAction_invokes_action_when_successful()
    {
        var result = Result.Success();
        var tapped = false;

        var output = await result.TapAsync(() => { tapped = true; });

        await output.AssertSuccess();
        await Assert.That(tapped).IsTrue();
    }

    [Test]
    public async Task TapAsync_ResultSource_SyncAction_does_not_invoke_action_when_failed()
    {
        var result = Result.Failure(TestError);
        var tapped = false;

        var output = await result.TapAsync(() => { tapped = true; });

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(tapped).IsFalse();
    }

    #endregion

    #region TapErrorAsync — extension(Task<Result> source)

    [Test]
    public async Task TapErrorAsync_TaskSource_AsyncAction_does_not_invoke_action_when_successful()
    {
        var resultTask = Task.FromResult(Result.Success());
        var tapped = false;

        var output = await resultTask.TapErrorAsync(_ => { tapped = true; return Task.CompletedTask; });

        await output.AssertSuccess();
        await Assert.That(tapped).IsFalse();
    }

    [Test]
    public async Task TapErrorAsync_TaskSource_AsyncAction_invokes_action_when_failed()
    {
        var resultTask = Task.FromResult(Result.Failure(TestError));
        Error? tappedError = null;

        var output = await resultTask.TapErrorAsync(e => { tappedError = e; return Task.CompletedTask; });

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(tappedError).IsEqualTo(TestError);
    }

    [Test]
    public async Task TapErrorAsync_TaskSource_SyncAction_does_not_invoke_action_when_successful()
    {
        var resultTask = Task.FromResult(Result.Success());
        var tapped = false;

        var output = await resultTask.TapErrorAsync(_ => { tapped = true; });

        await output.AssertSuccess();
        await Assert.That(tapped).IsFalse();
    }

    [Test]
    public async Task TapErrorAsync_TaskSource_SyncAction_invokes_action_when_failed()
    {
        var resultTask = Task.FromResult(Result.Failure(TestError));
        Error? tappedError = null;

        var output = await resultTask.TapErrorAsync(e => { tappedError = e; });

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(tappedError).IsEqualTo(TestError);
    }

    #endregion

    #region TapErrorAsync — extension(Result source)

    [Test]
    public async Task TapErrorAsync_ResultSource_AsyncAction_does_not_invoke_action_when_successful()
    {
        var result = Result.Success();
        var tapped = false;

        var output = await result.TapErrorAsync(_ => { tapped = true; return Task.CompletedTask; });

        await output.AssertSuccess();
        await Assert.That(tapped).IsFalse();
    }

    [Test]
    public async Task TapErrorAsync_ResultSource_AsyncAction_invokes_action_when_failed()
    {
        var result = Result.Failure(TestError);
        Error? tappedError = null;

        var output = await result.TapErrorAsync(e => { tappedError = e; return Task.CompletedTask; });

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(tappedError).IsEqualTo(TestError);
    }

    [Test]
    public async Task TapErrorAsync_ResultSource_SyncAction_does_not_invoke_action_when_successful()
    {
        var result = Result.Success();
        var tapped = false;

        var output = await result.TapErrorAsync(_ => { tapped = true; });

        await output.AssertSuccess();
        await Assert.That(tapped).IsFalse();
    }

    [Test]
    public async Task TapErrorAsync_ResultSource_SyncAction_invokes_action_when_failed()
    {
        var result = Result.Failure(TestError);
        Error? tappedError = null;

        var output = await result.TapErrorAsync(e => { tappedError = e; });

        await Assert.That(output.IsFailure).IsTrue();
        await Assert.That(tappedError).IsEqualTo(TestError);
    }

    #endregion

    #region EnsureAsync — extension(Task<Result> source)

    [Test]
    public async Task EnsureAsync_TaskSource_AsyncPredicate_returns_success_when_predicate_passes()
    {
        var resultTask = Task.FromResult(Result.Success());

        var output = await resultTask.EnsureAsync(() => Task.FromResult(true), TestError);

        await output.AssertSuccess();
    }

    [Test]
    public async Task EnsureAsync_TaskSource_AsyncPredicate_returns_failure_when_predicate_fails()
    {
        var resultTask = Task.FromResult(Result.Success());

        var output = await resultTask.EnsureAsync(() => Task.FromResult(false), TestError);

        await output.AssertFailure(TestError);
    }

    [Test]
    public async Task EnsureAsync_TaskSource_AsyncPredicate_propagates_error_when_already_failed()
    {
        var resultTask = Task.FromResult(Result.Failure(TestError));

        var output = await resultTask.EnsureAsync(() => Task.FromResult(true), Error.NotFound("nf", "not found"));

        await output.AssertFailure(TestError);
    }

    [Test]
    public async Task EnsureAsync_TaskSource_SyncPredicate_returns_success_when_predicate_passes()
    {
        var resultTask = Task.FromResult(Result.Success());

        var output = await resultTask.EnsureAsync(() => true, TestError);

        await output.AssertSuccess();
    }

    [Test]
    public async Task EnsureAsync_TaskSource_SyncPredicate_returns_failure_when_predicate_fails()
    {
        var resultTask = Task.FromResult(Result.Success());

        var output = await resultTask.EnsureAsync(() => false, TestError);

        await output.AssertFailure(TestError);
    }

    [Test]
    public async Task EnsureAsync_TaskSource_SyncPredicate_propagates_error_when_already_failed()
    {
        var resultTask = Task.FromResult(Result.Failure(TestError));

        var output = await resultTask.EnsureAsync(() => true, Error.NotFound("nf", "not found"));

        await output.AssertFailure(TestError);
    }

    #endregion

    #region EnsureAsync — extension(Result source)

    [Test]
    public async Task EnsureAsync_ResultSource_AsyncPredicate_returns_success_when_predicate_passes()
    {
        var result = Result.Success();

        var output = await result.EnsureAsync(() => Task.FromResult(true), TestError);

        await output.AssertSuccess();
    }

    [Test]
    public async Task EnsureAsync_ResultSource_AsyncPredicate_returns_failure_when_predicate_fails()
    {
        var result = Result.Success();

        var output = await result.EnsureAsync(() => Task.FromResult(false), TestError);

        await output.AssertFailure(TestError);
    }

    [Test]
    public async Task EnsureAsync_ResultSource_AsyncPredicate_propagates_error_when_already_failed()
    {
        var result = Result.Failure(TestError);

        var output = await result.EnsureAsync(() => Task.FromResult(true), Error.NotFound("nf", "not found"));

        await output.AssertFailure(TestError);
    }

    [Test]
    public async Task EnsureAsync_ResultSource_SyncPredicate_returns_success_when_predicate_passes()
    {
        var result = Result.Success();

        var output = await result.EnsureAsync(() => true, TestError);

        await output.AssertSuccess();
    }

    [Test]
    public async Task EnsureAsync_ResultSource_SyncPredicate_returns_failure_when_predicate_fails()
    {
        var result = Result.Success();

        var output = await result.EnsureAsync(() => false, TestError);

        await output.AssertFailure(TestError);
    }

    [Test]
    public async Task EnsureAsync_ResultSource_SyncPredicate_propagates_error_when_already_failed()
    {
        var result = Result.Failure(TestError);

        var output = await result.EnsureAsync(() => true, Error.NotFound("nf", "not found"));

        await output.AssertFailure(TestError);
    }

    #endregion
}