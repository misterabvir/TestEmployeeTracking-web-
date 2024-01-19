using Infrastructure.Client.Extensions;
using Infrastructure.Client.Models;
using ProtoContracts;

namespace Infrastructure.Client.Pages.DepartmentComponent
{
    public partial class DepartmentComponent
    {

        private List<Department>? _departmentsTree;
        private List<Department>? _departmentsOptionList;
        private List<Department>? _departmentsOptionFilteredList;
        private List<Employee> _employees = new();
        private Department? _selectedDepartment;
        private ErrorModel? _error;

        protected override async Task OnInitializedAsync()
        {
            await Load();
        }

        internal async Task Load()
        {
            var departments = await _departmentService.GetAll();
            _departmentsTree = departments.Departments.FromResponse();
            _departmentsOptionList = _departmentsTree?.ToLineList();
            
        }


        internal async Task SelectedDepartmentChanged(Department department)
        {
            _selectedDepartment = department;
            _departmentsOptionFilteredList = _departmentsTree?.ToLineList(_selectedDepartment);

            var result = await _employeeService.GetEmployeeByDepartmentId(_selectedDepartment.Id);           
            
            if (result.IsSucces)
            {
                _employees = result.Employees.FromResponse();
            }
            else
            {
                _error = result.Error;
            }
        }

        internal async Task SelectedDepartmentTitleChanged()
        {

            if (_selectedDepartment is null) return;

            var current = _selectedDepartment;
            var result = await _departmentService.ChangeTitle(
                current.Id,
                current.Title);
            if (result.IsSucces)
            {
                current.Title = result.Department.Title;
            }
            else
            {
                _error = result.Error;
            }
        }

        internal async Task SelectedDepartmentParentChanged()
        {
            if (_selectedDepartment is null) return;

            var current = _selectedDepartment;
            var result = await _departmentService.SetParent(
                current.Id,
                current.ParentId);
            if (result.IsSucces)
            {
                current.ParentId = result.Department.ParentId;
                await Load();
            }
            else
            {
                _error = result.Error;
            }
        }

        internal async Task CreateDepartment(Department department)
        {
            var result = await _departmentService.Create(department.Title, department.ParentId);
            if (result.IsSucces)
            {
                await Load();
            }
            else
            {
                _error = result.Error;
            }
        }

        internal async Task DeleteDepartment(Department department)
        {
            var result = await _departmentService.Delete(department.Id);
            if (result.IsSucces)
            {
                await Load();
            }
            else
            {
                _error = result.Error;
            }
        }

    }
}