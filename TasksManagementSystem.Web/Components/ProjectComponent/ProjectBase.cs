using Microsoft.AspNetCore.Components;

namespace TasksManagementSystem.Web.Components.ProjectComponent
{
    public class ProjectBase : ComponentBase
    {
        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public string Description { get; set; }
    }
}
