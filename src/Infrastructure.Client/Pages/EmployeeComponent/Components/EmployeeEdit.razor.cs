using Infrastructure.Client.Models;
using Microsoft.AspNetCore.Components;

namespace Infrastructure.Client.Pages.EmployeeComponent.Components
{
    public partial class EmployeeEdit
    {
        private bool isDisable => Employee is null;
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