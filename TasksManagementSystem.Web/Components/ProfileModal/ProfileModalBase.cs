using Microsoft.AspNetCore.Components;
using TaskManagementSystem.Models.DTOs.AuthDTOs;

namespace TasksManagementSystem.Web.Components.ProfileModal
{
    public class ProfileModalBase : ComponentBase
    {
        [Parameter] public string FullName { get; set; }
        [Parameter] public string Username { get; set; }
        [Parameter] public string Password { get; set; }
        [Parameter] public EventCallback Callback { get; set; }
        [Parameter] public EventCallback<UserRegisterDTO> OnModalChange { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsVisible { get; set; } = false;
        public bool IsValid { get; set; } = false;

        public void SaveChanges()
        {
            UserRegisterDTO userRegisterDTO = new UserRegisterDTO
            {
                FullName = FullName,
                Username = Username,
                Password = Password,
            };
            OnModalChange.InvokeAsync(userRegisterDTO);
        }
        public void ExecuteCallback()
        {
            if (Username?.Length < 5 || Password.Length < 8)
                return;
            SaveChanges();
            Callback.InvokeAsync();
        }
        public void CloseModal()
        {
            IsVisible = false;
            Console.WriteLine(IsVisible);
        }
        public void ShowModal()
        {
            StateHasChanged();
            IsVisible = true;
        }

    }
}
