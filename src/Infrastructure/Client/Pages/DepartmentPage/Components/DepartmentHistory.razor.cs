using Client.Models;
using Microsoft.AspNetCore.Components;

namespace Client.Pages.DepartmentPage.Components
{
    public partial class DepartmentHistory
    {
        [Parameter]
        public Department? Department { get; set; }
        [Parameter]
        public IEnumerable<Employee>? Employees { get; set; }
        [Parameter]
        public IEnumerable<History>? Histories { get; set; }
        private IEnumerable<History> _histories => Histories?.Where(h => h.DepartmentId == Department?.Id) ?? Enumerable.Empty<History>();
    }
}