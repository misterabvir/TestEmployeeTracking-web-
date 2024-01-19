using Infrastructure.Client.Models;
using Microsoft.AspNetCore.Components;

namespace Infrastructure.Client.Pages.DepartmentComponent.Components
{
    public partial class DepartmentCreate
    {
        private Department _department = Department.GetEmpty();
        [Parameter]
        public List<Department>? Departments { get; set; }
        [Parameter]
        public EventCallback<Department> OnCreateDepartment { get; set; }

        private bool IsDisabled => (_department.Title.Length < 2);

        private async Task CreateHandler()
        {
            await OnCreateDepartment.InvokeAsync(_department);

            _department = Department.GetEmpty();
        }
    }
}