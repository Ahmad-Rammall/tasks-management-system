using Microsoft.AspNetCore.Components;

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
        public NavigationManager navigationManager { get; set; }
        public void HandleClick()
        {
            navigationManager.NavigateTo($"/TasksPage/{ProjectID}");
        }
    }
}
