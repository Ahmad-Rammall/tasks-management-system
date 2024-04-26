using Microsoft.AspNetCore.Components;

namespace TasksManagementSystem.Web.Pages.Tasks
{
    public class TasksBase : ComponentBase
    {
        [Parameter]
        public int ProjectId { get; set; }
        protected override Task OnInitializedAsync()
        {
            Console.WriteLine(ProjectId); return base.OnInitializedAsync();
        }
    }
}
