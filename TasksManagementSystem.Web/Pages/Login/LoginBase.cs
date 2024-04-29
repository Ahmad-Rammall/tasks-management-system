using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using TaskManagementSystem.Models.DTOs.AuthDTOs;
using TaskManagementSystem.Models.DTOs.UserDTOs;
using TasksManagementSystem.Web.Helpers;
using TasksManagementSystem.Web.Services.Interfaces;
using TasksManagementSystem.Web.Store.User;

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

        [Inject]
        protected IState<UserState> UserState { get; set; }

        [Inject]
        public IDispatcher Dispatcher { get; set; }
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
                    await LocalStorageManager.SaveToLocalStorage(JSRuntime, "jwtToken", userResponse.Token);
                    await LocalStorageManager.SaveToLocalStorage(JSRuntime, "userId", userResponse.User.Id.ToString());

                    if (userResponse.User.RoleId == 2)
                        navigationManager.NavigateTo("/employeePage");
                    else if (userResponse.User.RoleId == 1)
                        navigationManager.NavigateTo("/profiles");

                    var userState = new UserState(userResponse.User);
                    Dispatcher.Dispatch(new ImplementUserAction(userState));
                }

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
