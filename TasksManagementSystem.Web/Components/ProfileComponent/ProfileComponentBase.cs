using Microsoft.AspNetCore.Components;
using TaskManagementSystem.Models.DTOs.UserDTOs;
using TasksManagementSystem.Web.Services.Interfaces;
using TasksManagementSystem.Web.Components.DeleteConfirmationModal;

namespace TasksManagementSystem.Web.Components.ProfileComponent
{
    public class ProfileComponentBase : ComponentBase
    {
        [Parameter] public UserDTO Employee { get; set; }
        [Parameter] public bool IsDeleted { get; set; }
        [Parameter] public EventCallback<UserDTO> SetSelectedUser { get; set; }
        [Parameter] public EventCallback<int> SetSelectedProfileId { get; set; }
        [Parameter] public Action OpenDeleteModal { get; set; }
        [Parameter] public Action OpenUpdateModal { get; set; }
        public string ErrorMessage { get; set; }
        public string Password { get; set; }
        [Inject] public IProfileService _profileService { get; set; }
        [Inject] NavigationManager navigationManager { get; set; }

        public void ShowDeleteModal()
        {
            SetSelectedProfileId.InvokeAsync(Employee.Id);
            OpenDeleteModal?.Invoke();
        }
        public void ShowUpdateModal()
        {
            SetSelectedUser.InvokeAsync(Employee);
            OpenUpdateModal?.Invoke();
        }        
    }
}
