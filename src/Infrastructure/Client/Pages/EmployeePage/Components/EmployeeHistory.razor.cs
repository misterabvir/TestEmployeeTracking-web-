using Client.Models;
using Microsoft.AspNetCore.Components;
using System.Globalization;
namespace Client.Pages.EmployeePage.Components
{
    public partial class EmployeeHistory
    {
        [Parameter] public Employee? Employee { get; set; }
        [Parameter] public IEnumerable<Department>? Departments { get; set; }
        [Parameter] public IEnumerable<History>? Histories { get; set; }
    }
}