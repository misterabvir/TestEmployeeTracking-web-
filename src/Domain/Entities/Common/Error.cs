namespace Domain.Common;

public record Error
{
    public string Title { get; } = string.Empty;
    public ResultErrorStatus Status { get; init; } = ResultErrorStatus.None;
    public string Message { get; } = string.Empty;
    public Error(string title, string message, ResultErrorStatus status)
    {
        Title = title;
        Message = message;
        Status = status;
    }
    public static implicit operator Result(Error error) => Result.Failure(error);
    public static readonly Error None = new(string.Empty, string.Empty, ResultErrorStatus.None);
}
public record Error<T> : Error
{
    public Error(string title, string description, ResultErrorStatus status) : base(title, description, status)
    { }
    public static implicit operator Result<T>(Error<T> error) => Result<T>.Failure(error);
}

public enum ResultErrorStatus
{
    None,
    NotFound,
    InvalidArgument,
    BadRequest
}