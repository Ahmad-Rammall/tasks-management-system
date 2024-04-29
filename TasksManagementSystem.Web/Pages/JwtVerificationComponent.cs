using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using TasksManagementSystem.Web.Helpers;

namespace TasksManagementSystem.Web.Pages
{
    public class JwtVerificationComponent : ComponentBase
    {
        [Inject] protected NavigationManager NavigationManager { get; set; }
        [Inject] protected IJSRuntime JSRuntime { get; set; }
        protected bool IsNavigated { get; set; }

        protected override async Task OnInitializedAsync()
        {
            string? jwtToken = await LocalStorageManager.GetFromLocalStorage(JSRuntime,"jwtToken");
            if (string.IsNullOrEmpty(jwtToken))
            {
                NavigationManager.NavigateTo("/");
                IsNavigated = true;
            }
            else
                IsNavigated = false;
        }
    }
}
