using Infrastructure.Client.Models;
using Microsoft.AspNetCore.Components;


namespace Infrastructure.Client.Pages.EmployeeComponent.Components
{
    public partial class EmployeeTable
    {
        [Parameter]
        public IEnumerable<Employee>? Employees { get; set; }
        [Parameter]
        public IEnumerable<Department>? Departments { get; set; }
        [Parameter]
        public EventCallback<Employee> OnSelectedChanged { get; set; }
        [Parameter]
        public EventCallback<Employee> OnDeleteSelected { get; set; }
        private Employee? _selected;
        private bool _isDialogShow = false;
        private void DeleteWithConfirm(Employee selected)
        {
            _selected = selected;
            _isDialogShow = true;
        }
        private async Task DeleteConfirmed(bool isConfirm)
        {
            _isDialogShow = false;
            if (isConfirm)
            {
                await OnDeleteSelected.InvokeAsync(_selected);
            }
        }
    }
}