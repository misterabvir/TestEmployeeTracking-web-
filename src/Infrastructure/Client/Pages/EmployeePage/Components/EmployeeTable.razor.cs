using Client.Models;
using Microsoft.AspNetCore.Components;


namespace Client.Pages.EmployeePage.Components
{
    public partial class EmployeeTable
    {
        [Parameter]
        public List<Employee>? Employees { get; set; }
        [Parameter]
        public List<Department>? Departments { get; set; }
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