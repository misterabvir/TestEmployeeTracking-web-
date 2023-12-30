using Client.Extensions;
using Client.Models;
using Client.Protos;

namespace Client.Pages.EmployeePage
{
    public partial class EmployeePage
    {

        private List<Department>? _departments;
        private List<Employee>? _employees;
        private List<History>? _history;
        private Employee? _selectedEmployee;
        private ErrorModel? _error;
        protected override async Task OnInitializedAsync()
        {
            await Load();
        }

        internal async Task Load()
        {
            var departmentReply = await _departmentService.GetAll();
            if (departmentReply.IsSucces)
            {
                _departments = departmentReply.Departments.FromResponse().ToLineList();
            }
            else
            {
                _error = departmentReply.Error;
            }
            var employeeReply = await _employeeService.GetAll();
            if (employeeReply.IsSucces)
            {
                _employees = employeeReply.Employees.FromResponse();
            }
            else
            {
                _error = employeeReply.Error;
            }
        }

        internal async Task SelectedEmployeeChanged(Employee employee)
        {
            _selectedEmployee = employee;
            var historyReply = await _historyService.GetEmployeeHistory(employee.Id);
            if (historyReply.IsSucces)
                _history = historyReply.Histories.Where(h => h.EmployeeId == employee.Id).FromResponse();
            else
            {
                _error = historyReply.Error;
            }
        }

        internal async Task CreateEmployee(Employee employee)
        {
            var result = await _employeeService.Create(
                employee.LastName,
                employee.FirstName,
                employee.DepartmentId);
            if (result.IsSucces)
            {
                await Load();
            }
            else
            {
                _error = result.Error;
            }
        }

        internal async Task Remove(Employee employee)
        {
            var result = await _employeeService.Delete(employee.Id);
            if (result.IsSucces)
            {
                await Load();
            }
            else
            {
                _error = result.Error;
            }
        }

        internal async Task EmployeePersonalDataChanged()
        {
            if (_selectedEmployee is null) return;
            var result = await _employeeService.ChangePersonalData(
                _selectedEmployee.Id,
                _selectedEmployee.LastName,
                _selectedEmployee.FirstName);
            if (result.IsSucces)
            {
                _selectedEmployee.FirstName = result.Employee.Firstname;
                _selectedEmployee.LastName = result.Employee.Lastname;
            }
            else
            {
                _error = result.Error;
            }
        }

        internal async Task EmployeeDepartmentChanged()
        {
            if (_selectedEmployee is null) return;
            var result = await _employeeService.ChangeDepartment(
                _selectedEmployee.Id,
                _selectedEmployee.DepartmentId);
            if (result.IsSucces)
            {
                _selectedEmployee.DepartmentId = result.Employee.DepartmentId;
                var historyReply = await _historyService.GetEmployeeHistory(_selectedEmployee.Id);
                if (historyReply.IsSucces)
                {
                    _history = historyReply.Histories.Where(h => h.EmployeeId == _selectedEmployee.Id).FromResponse();
                }
                else
                {
                    _error = historyReply.Error;
                }
            }
            else
            {
                _error = result.Error;
            }
        }
    }
}