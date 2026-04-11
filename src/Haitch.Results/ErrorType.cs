namespace Haitch.Results;

/// <summary>
/// Represents the category of errors that can occur during the execution of an operation.
/// </summary>
public enum ErrorType
{
    /// <summary>
    /// Default error category. Used to represent a generic failure
    /// </summary>
    Failure,

    /// <summary>
    /// Represents that the error occurred is from a validation operation
    /// </summary>
    Validation,

    /// <summary>
    /// Represents that the error occurred is from a resource not being found
    /// </summary>
    NotFound,

    /// <summary>
    /// Represents an error category indicating a conflict, such as a resource state
    /// preventing an operation from completing successfully.
    /// </summary>
    Conflict,

    /// <summary>
    /// Represents an error category indicating that the operation failed due to lack of authorization.
    /// </summary>
    Unauthorized,

    /// <summary>
    /// Indicates that the requested action is forbidden due to insufficient permissions or access constraints.
    /// </summary>
    Forbidden,

    /// <summary>
    /// Represents an error category for unexpected or unhandled scenarios that do not fall into predefined error types.
    /// </summary>
    Unexpected
}