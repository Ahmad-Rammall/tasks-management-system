using Microsoft.AspNetCore.Components;
using TaskManagementSystem.Models.DTOs.TaskDTOs;
using TasksManagementSystem.Web.Services.Interfaces;

namespace TasksManagementSystem.Web.Pages.Tasks
{
    public class TasksBase : ComponentBase
    {
        [Parameter]
        public int ProjectId { get; set; }

        [Inject]
        public ITaskService _taskService {  get; set; }
        public IEnumerable<TaskDTO> TasksList { get; set; }
        protected override async Task OnInitializedAsync()
        {
            TasksList = await _taskService.GetProjectTasks(ProjectId);
        }
    }
}
