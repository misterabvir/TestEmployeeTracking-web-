namespace Domain.Common;

public record Error
{
    public string Title { get; } = string.Empty;
    public string Description { get; } = string.Empty;
    public Error(string title, string description)
    {
        Title = title;
        Description = description;
    }
    public static implicit operator Result(Error error) => Result.Failure(error);
    public static readonly Error None = new(string.Empty, string.Empty);
}
public record Error<T> : Error
{
    public Error(string title, string description) : base(title, description)
    { }
    public static implicit operator Result<T>(Error<T> error) => Result<T>.Failure(error);
}
