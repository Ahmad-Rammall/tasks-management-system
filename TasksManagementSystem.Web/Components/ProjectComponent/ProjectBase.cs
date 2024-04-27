using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using TasksManagementSystem.Web.Helpers;

namespace TasksManagementSystem.Web.Components.ProjectComponent
{
    public class ProjectBase : ComponentBase
    {
        [Parameter]
        public int ProjectID { get; set; }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public string Description { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }
        public async Task HandleClick()
        {
            var roleId = await LocalStorageManager.GetFromLocalStorage(JSRuntime, "userRole");
            if(int.Parse(roleId) == 2)
                navigationManager.NavigateTo($"/TasksPage/{ProjectID}");
            else
                navigationManager.NavigateTo($"/TasksPageAdmin/{ProjectID}");
        }
    }
}
