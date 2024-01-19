using Infrastructure.Client.Models;
using Microsoft.AspNetCore.Components;

namespace Infrastructure.Client.Pages.DepartmentComponent.Components
{
    public partial class DepartmentNode
    {

        [Parameter]
        public Department? Node { get; set; }
        [Parameter]
        public EventCallback<Department> OnSelectedChanged { get; set; }
        [Parameter]
        public EventCallback<Department> OnDeleteSelected { get; set; }
        bool IsNotRoot => !string.IsNullOrEmpty(Node?.ParentId);
        bool _isExpand { get; set; }
        void Toggle() => _isExpand = !_isExpand;
        private bool _isConfirmDialogShow = false;
        private void DeleteWithConfirm()
        {
            _isConfirmDialogShow = true;
        }
        private async Task DeleteConfirmed(bool isConfirm)
        {
            _isConfirmDialogShow = false;
            if (isConfirm)
            {
                await OnDeleteSelected.InvokeAsync(Node);
            }
        }
    }
}