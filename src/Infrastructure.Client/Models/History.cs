namespace Infrastructure.Client.Models;

public class History
{
    public string Id { get; set; } = null!;
    public string DepartmentId { get; set; } = null!;
    public string EmployeeId { get; set; } = null!;
    public string StartDate { get; set; } = null!;
    public string EndDate { get; set; } = null!;
}