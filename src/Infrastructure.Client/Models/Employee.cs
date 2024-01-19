namespace Infrastructure.Client.Models;

public class Employee
{
    public string Id { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName{ get; set; } = null!;
    public string DepartmentId { get; set; } = null!;
    public string FullName => $"{FirstName} {LastName}";
    public static Employee GetEmpty() =>
        new()
        {
            Id = string.Empty,
            FirstName = string.Empty,
            LastName = string.Empty,
            DepartmentId = string.Empty
        };
}
