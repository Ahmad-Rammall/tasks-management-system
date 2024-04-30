using Microsoft.AspNetCore.Components;
using TaskManagementSystem.Models.DTOs.AuthDTOs;
using TaskManagementSystem.Models.DTOs.UserDTOs;

namespace TasksManagementSystem.Web.Components.ProfileUpdateModal
{
    public class ProfileUpdateModalBase : ComponentBase
    {
        [Parameter] public UserDTO Employee { get; set; }
        [Parameter] public string Password { get; set; }
        [Parameter] public EventCallback Callback { get; set; }
        [Parameter] public EventCallback<UserRegisterDTO> OnModalChange { get; set; }
        [Parameter] public EventCallback SelectProfileId { get; set; }

        public string ErrorMessage { get; set; }
        public bool IsVisible { get; set; } = false;
        public bool IsValid { get; set; } = false;
        public void SaveChanges()
        {
            UserRegisterDTO userRegisterDTO = new UserRegisterDTO
            {
                FullName = Employee?.FullName,
                Username = Employee?.Username,
                Password = Password,
            };
            OnModalChange.InvokeAsync(userRegisterDTO);
        }
        public void ExecuteCallback()
        {
            if (Employee.Username?.Length < 5 || Password?.Length < 8)
                return;
            SaveChanges();
            Callback.InvokeAsync();
        }
        public void CloseModal()
        {
            StateHasChanged();
            IsVisible = false;
        }
        public void ShowModal()
        {
            StateHasChanged();
            IsVisible = true;
        }
    }
}
