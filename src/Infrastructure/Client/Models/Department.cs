namespace Client.Models;

public class Department
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public List<Department>? Children { get; set; }
}
