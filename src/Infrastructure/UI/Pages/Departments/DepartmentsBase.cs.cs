using Grpc;
using Microsoft.AspNetCore.Components;
using UI.Services.Abstractions;

namespace UI.Pages.Departments;

public partial class DepartmentsBase : ComponentBase
{
    [Inject] private IDepartmentService departmentService { get; set; }

    [Parameter] public IEnumerable<DepartmentModel> Departments { get; set; } = null!;


    private IEnumerable<DepartmentResponse> _departments = new List<DepartmentResponse>();

    protected override async Task OnInitializedAsync()
    {
        var response = await departmentService.Get();
        Departments = FromResponce(response);
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
    }

    private static IEnumerable<DepartmentModel> FromResponce(IEnumerable<DepartmentResponse?> departments, string? parentId = null)
    {

        var parents = departments.Where(d => d is not null && d.ParentId == parentId);
        List<DepartmentModel> models = new();

        foreach (var department in parents)
        {
            if (department is not null)
                models.Add(
                    new()
                    {
                        Id = department.Id,
                        Title = department.Title,
                        ParentId = department.ParentId,
                        SubDepartments = FromResponce(departments, department.Id)
                    });
        }
        return models;
    }
}
