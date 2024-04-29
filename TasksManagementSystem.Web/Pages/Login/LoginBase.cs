using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using TaskManagementSystem.Models.DTOs.AuthDTOs;
using TasksManagementSystem.Web.Helpers;
using TasksManagementSystem.Web.Services.Interfaces;

namespace TasksManagementSystem.Web.Pages.Login
{
    public class LoginBase : ComponentBase
    {
        [Inject]
        public IAuthService _authService { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ErrorMessage { get; set; }
        public async Task HandleLogin()
        {
            try
            {
                UserLoginDTO userLoginDTO = new UserLoginDTO
                {
                    Username = Username,
                    Password = Password
                };
                var userResponse = await _authService.LoginUser(userLoginDTO);

                if (userResponse != null)
                {
                    if (userResponse.User.RoleId == 2)
                        navigationManager.NavigateTo("/employeePage");
                    else if(userResponse.User.RoleId == 1)
                        navigationManager.NavigateTo("/adminPage");

                    await LocalStorageManager.SaveToLocalStorage(JSRuntime, "jwtToken", userResponse.Token);
                    await LocalStorageManager.SaveToLocalStorage(JSRuntime, "userId", userResponse.User.Id.ToString());
                    await LocalStorageManager.SaveToLocalStorage(JSRuntime, "userRole", userResponse.User.RoleId.ToString());
                }

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
