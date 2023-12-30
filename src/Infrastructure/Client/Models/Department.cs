namespace Client.Models;

public class Department
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public List<Department>? Children { get; set; }
    public string ParentId { get; set;} = null!;

    public static Department GetEmpty() => new Department 
    { 
        Title = string.Empty, 
        Children = null,
        ParentId = string.Empty,
        Id = string.Empty
    };   
}
