using Infrastructure.Client.Models;
using Microsoft.AspNetCore.Components;
namespace Infrastructure.Client.Pages.DepartmentComponent.Components;


/// <summary>
/// 
/// </summary>
public partial class DepartmentEdit
{
    private bool IsDisabled => Department is null;
    
    [Parameter]
    public Department? Department { get; set; }
    [Parameter]
    public List<Department>? Departments { get; set; }
    [Parameter]
    public EventCallback OnTitleChanged { get; set; }
    [Parameter]
    public EventCallback OnParentChanged { get; set; }
}