using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using TasksManagementSystem.Web.Services.Interfaces;

namespace TasksManagementSystem.Web.Helpers
{
    public class Methods
    {
        private readonly IAuthService _authService;
        private readonly IJSRuntime _jsRuntime;

        public Methods(IAuthService authService, IJSRuntime jsRuntime)
        {
            _authService = authService;
            _jsRuntime = jsRuntime;
        }
        public async Task<bool> IsUserAdmin()
        {
            try
            {
                string jwtToken = await LocalStorageManager.GetFromLocalStorage(_jsRuntime, "jwtToken");
                if(string.IsNullOrEmpty(jwtToken))
                {
                    return false;
                }
                return await _authService.IsUserAdmin(jwtToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
