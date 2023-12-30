using Client.Models;
using Microsoft.AspNetCore.Components;

namespace Client.Pages.EmployeePage.Components
{
    public partial class EmployeeEdit
    {
        [Parameter]
        public Employee? Employee { get; set; }
        [Parameter]
        public IEnumerable<Department>? Departments { get; set; }
        [Parameter]
        public EventCallback OnPersonalDataChanged { get; set; }
        [Parameter]
        public EventCallback OnDepartmentChanged { get; set; }
    }
}