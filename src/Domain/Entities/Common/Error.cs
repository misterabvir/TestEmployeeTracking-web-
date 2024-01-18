namespace Domain.Common;

/// <summary>
/// Error
/// </summary>
public record Error
{
    /// <summary>
    /// Error title
    /// </summary>
    public string Title { get; } = string.Empty;
    /// <summary>
    /// Error status
    /// </summary>
    public ResultErrorStatus Status { get; init; } = ResultErrorStatus.None;
    /// <summary>
    /// Error message
    /// </summary>
    public string Message { get; } = string.Empty;
    
    /// <summary>
    /// Initialize new instance of <see cref="Error"/>
    /// </summary>
    /// <param name="title"> Error title </param>
    /// <param name="message"> Error message </param>
    /// <param name="status"> Error status </param>
    public Error(string title, string message, ResultErrorStatus status)
    {
        Title = title;
        Message = message;
        Status = status;
    }
    
    /// <summary>
    /// Implicit conversion from <see cref="Error"/> to <see cref="Result"/>
    /// </summary>
    /// <param name="error"> Error to convert </param>
    public static implicit operator Result(Error error) => Result.Failure(error);
    
    /// <summary>
    /// Error when there is no error
    /// </summary>
    public static readonly Error None = new(string.Empty, string.Empty, ResultErrorStatus.None);
}
/// <summary>
/// Error
/// </summary>
/// <typeparam name="T">Any</typeparam>
public record Error<T> : Error
{
    /// <summary>
    /// Initialize new instance of <see cref="Error"/>
    /// </summary>
    /// <param name="title"> Error title </param>
    /// <param name="message"> Error message </param>
    /// <param name="status"> Error status </param>
    /// <returns> New instance of <see cref="Error"/> </returns>
    public Error(string title, string description, ResultErrorStatus status) : base(title, description, status)
    { }
    /// <summary>
    /// Implicit conversion from generic <see cref="Error"/> to generic <see cref="Result"/>
    /// </summary>
    /// <param name="error"> Error to convert </param>
    /// <typeparam name="T"> Any </typeparam>
    public static implicit operator Result<T>(Error<T> error) => Result<T>.Failure(error);
}
