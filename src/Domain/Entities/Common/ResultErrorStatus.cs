namespace Domain.Common;

/// <summary>
/// Status of error
/// </summary>
public enum ResultErrorStatus
{
    /// <summary>No error</summary>
    None,
    /// <summary>Not found error </summary>
    NotFound,
    /// <summary>Invalid argument </summary>
    InvalidArgument,
    /// <summary>Bad request </summary>
    BadRequest
}