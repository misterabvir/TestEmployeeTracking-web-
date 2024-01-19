using Infrastructure.Client.Extensions;
using Infrastructure.Client.Models;
using Microsoft.AspNetCore.Components;
using ProtoContracts;

namespace Infrastructure.Client.Pages.EmployeeComponent
{
    public partial class EmployeeComponent
    {

        [Parameter] public List<Department>? Departments { get; set; }
        [Parameter] public Department? SelectedDepartment { get; set; }   
        [Parameter] public List<Employee>? Employees { get; set; }

        
        private List<History>? _history;
        private Employee? _selectedEmployee;
        private ErrorModel? _error;


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
                Employees?.Add(employee);
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
                Employees?.Remove(employee);
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