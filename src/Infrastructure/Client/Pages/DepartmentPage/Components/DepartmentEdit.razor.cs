using Client.Models;
using Microsoft.AspNetCore.Components;

namespace Client.Pages.DepartmentPage.Components
{
    public partial class DepartmentEdit
    {
        [Parameter]
        public Department? Department { get; set; }
        [Parameter]
        public List<Department>? Departments { get; set; }
        [Parameter]
        public EventCallback OnTitleChanged { get; set; }
        [Parameter]
        public EventCallback OnParentChanged { get; set; }
    }
}