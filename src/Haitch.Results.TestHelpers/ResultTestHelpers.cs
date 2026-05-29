namespace Haitch.Results.TestHelpers;

/// <summary>
/// Provides utility methods for testing and validating results within the Haitch.Results framework.
/// This static class is designed to offer commonly used functionality to simplify unit testing
/// scenarios involving result-related objects.
/// </summary>
public static class ResultTestHelpers
{
    extension<TValue, TError>(Result<TValue, TError> source)
    {
        /// <summary>
        /// Asserts that the result is successful.
        /// </summary>
        /// <returns>A task representing the asynchronous assertion operation.</returns>
        public async Task AssertSuccess()
        {
            using (Assert.Multiple())
            {
                await Assert.That(source.IsSuccess).IsTrue();
                await Assert.That(source.IsFailure).IsFalse();
            }
        }
        
        /// <summary>
        /// Asserts that the result is successful and its value matches the expected value.
        /// </summary>
        /// <param name="value">The expected value to compare against the result's value.</param>
        /// <returns>A task representing the asynchronous assertion operation.</returns>
        public async Task AssertSuccess(TValue value)
        {
            using (Assert.Multiple())
            {
                await Assert.That(source.IsSuccess).IsTrue();
                await Assert.That(source.IsFailure).IsFalse();
                await Assert.That(source.Value).IsEquatableOrEqualTo(value);
            }
        }

        /// <summary>
        /// Asserts that the result is a failure.
        /// </summary>
        /// <returns>A task representing the asynchronous assertion operation.</returns>
        public async Task AssertFailure()
        {
            using (Assert.Multiple())
            {
                await Assert.That(source.IsSuccess).IsFalse();
                await Assert.That(source.IsFailure).IsTrue();
            }
        }
        
        /// <summary>
        /// Asserts that the result is a failure and its error matches the expected error.
        /// </summary>
        /// <param name="error">The expected error to compare against the result's error.</param>
        /// <returns>A task representing the asynchronous assertion operation.</returns>
        public async Task AssertFailure(TError error)
        {
            using (Assert.Multiple())
            {
                await Assert.That(source.IsSuccess).IsFalse();
                await Assert.That(source.IsFailure).IsTrue();
                await Assert.That(source.Error).IsEqualTo(error);
            }
        }
    }
    
    extension<TValue>(Result<TValue> source)
    {
        /// <summary>
        /// Asserts that the result is successful.
        /// </summary>
        /// <returns>A task representing the asynchronous assertion operation.</returns>
        public async Task AssertSuccess()
        {
            using (Assert.Multiple())
            {
                await Assert.That(source.IsSuccess).IsTrue();
                await Assert.That(source.IsFailure).IsFalse();
            }
        }
        
        /// <summary>
        /// Asserts that the result is successful and its value matches the expected value.
        /// </summary>
        /// <param name="value">The expected value to compare against the result's value.</param>
        /// <returns>A task representing the asynchronous assertion operation.</returns>
        public async Task AssertSuccess(TValue value)
        {
            using (Assert.Multiple())
            {
                await Assert.That(source.IsSuccess).IsTrue();
                await Assert.That(source.IsFailure).IsFalse();
                await Assert.That(source.Value).IsEquatableOrEqualTo(value);
            }
        }

        /// <summary>
        /// Asserts that the result is a failure.
        /// </summary>
        /// <returns>A task representing the asynchronous assertion operation.</returns>
        public async Task AssertFailure()
        {
            using (Assert.Multiple())
            {
                await Assert.That(source.IsSuccess).IsFalse();
                await Assert.That(source.IsFailure).IsTrue();
            }
        }

        /// <summary>
        /// Asserts that the result represents a failure and its error matches the expected error.
        /// </summary>
        /// <param name="error">The expected error to compare against the result's error.</param>
        /// <returns>A task representing the asynchronous assertion operation.</returns>
        public async Task AssertFailure(Error error)
        {
            using (Assert.Multiple())
            {
                await Assert.That(source.IsSuccess).IsFalse();
                await Assert.That(source.IsFailure).IsTrue();
                await Assert.That(source.Error).IsEqualTo(error);
            }
        }
    }

    extension(Result source)
    {
        /// <summary>
        /// Asserts that the result is successful by verifying that the IsSuccess property
        /// is true and the IsFailure property is false.
        /// </summary>
        /// <returns>A task representing the asynchronous assertion operation.</returns>
        public async Task AssertSuccess()
        {
            using (Assert.Multiple())
            {
                await Assert.That(source.IsSuccess).IsTrue();
                await Assert.That(source.IsFailure).IsFalse();
            }
        }

        /// <summary>
        /// Asserts that the result is a failure.
        /// </summary>
        /// <returns>A task representing the asynchronous assertion operation.</returns>
        public async Task AssertFailure()
        {
            using (Assert.Multiple())
            {
                await Assert.That(source.IsSuccess).IsFalse();
                await Assert.That(source.IsFailure).IsTrue();
            }
        }

        /// <summary>
        /// Asserts that the result represents a failure and its error matches the expected error.
        /// </summary>
        /// <param name="error">The expected error to compare against the result's error.</param>
        /// <returns>A task representing the asynchronous assertion operation.</returns>
        public async Task AssertFailure(Error error)
        {
            using (Assert.Multiple())
            {
                await Assert.That(source.IsSuccess).IsFalse();
                await Assert.That(source.IsFailure).IsTrue();
                await Assert.That(source.Error).IsEqualTo(error);
            }
        }
    }
}