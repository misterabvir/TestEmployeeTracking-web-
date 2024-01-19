using Infrastructure.Client.Models;
using Microsoft.AspNetCore.Components;

namespace Infrastructure.Client.Pages.EmployeeComponent.Components
{
    public partial class EmployeeCreate
    {
        
        [Parameter]
        public IEnumerable<Department>? Departments { get; set; }
        [Parameter]
        public EventCallback<Employee> OnCreate { get; set; }
        private Employee _employee = Employee.GetEmpty();
        private bool IsDisabled => (
            _employee.FirstName.Length < 2 ||
            _employee.LastName.Length < 2 ||
            string.IsNullOrWhiteSpace(_employee.DepartmentId));
        private async Task CreateHandler()
        {
            await OnCreate.InvokeAsync(_employee);
            _employee = Employee.GetEmpty();
        }
    }
}