using Microsoft.AspNetCore.Components;

namespace TasksManagementSystem.Web.Components.TaskComponent
{
    public class TaskBase : ComponentBase
    {
        [Parameter]
        public string Title { get; set; }
        [Parameter]
        public string Description { get; set; }
        [Parameter]
        public bool IsCompleted { get; set; }
    }
}
