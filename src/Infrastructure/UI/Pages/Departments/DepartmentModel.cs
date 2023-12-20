namespace UI.Pages.Departments;

public class DepartmentModel
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? ParentId { get; set; }

    public IEnumerable<DepartmentModel> SubDepartments { get; set; } = new List<DepartmentModel>();

}

