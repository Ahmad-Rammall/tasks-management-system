using Microsoft.AspNetCore.Components;
using TaskManagementSystem.Models.DTOs.AuthDTOs;
using TasksManagementSystem.Web.Services.Interfaces;

namespace TasksManagementSystem.Web.Pages.Register
{
    public class RegisterBase : ComponentBase
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string VerifyPassword { get; set; }
        public string ErrorMessage { get; set; }
        public string FullName { get; set; }
        public bool IsValid { get; set; }

        [Inject]
        public IAuthService _authService { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }
        public async Task HandleRegister()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(FullName) || string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password)
                    || string.IsNullOrWhiteSpace(VerifyPassword))
                {
                    ErrorMessage = "All Fields Required";
                    return;
                }
                else if (!Password.Equals(VerifyPassword))
                {
                    ErrorMessage = "Passwords Don't Match.";
                    return;
                }
                ErrorMessage = string.Empty;

                if (!IsValid) return;

                UserRegisterDTO user = new UserRegisterDTO
                {
                    FullName = FullName,
                    Username = Username,
                    Password = Password,
                };

                var response = await _authService.RegisterUser(user);
                Console.WriteLine(response);
                if(response != null)
                {
                    navigationManager.NavigateTo("/");
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
