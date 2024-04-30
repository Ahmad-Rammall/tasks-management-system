using Microsoft.AspNetCore.Components;

namespace TasksManagementSystem.Web.Components.DeleteConfirmationModal
{
    public class DeleteModalBase : ComponentBase
    {
        [Parameter] public EventCallback OnConfirm { get; set; }
        [Parameter] public string ModalTitle { get; set; }
        [Parameter] public string ModalBody { get; set; }
        [Parameter] public string ActionButton { get; set; }
        [Parameter] public bool IsSuccess { get; set; } = false;

        public string DisplayModalTitle => ModalTitle ?? "Delete";
        public string DisplayModalBody => ModalBody ?? "Are you sure you want to delete this?";
        public string DisplayActionButton => ActionButton ?? "Delete";

        public bool IsVisible { get; set; } = false;

        public void ConfirmDelete()
        {
            OnConfirm.InvokeAsync();
            CloseModal();
        }

        public void CloseModal()
        {
            IsVisible = false;
        }
        public void ShowModal()
        {
            IsVisible = true;
        }
    }
}
