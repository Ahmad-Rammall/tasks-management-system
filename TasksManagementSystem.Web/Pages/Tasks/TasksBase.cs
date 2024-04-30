using Microsoft.AspNetCore.Components;
using TaskManagementSystem.Models.DTOs.TaskDTOs;
using TasksManagementSystem.Web.Helpers;
using TasksManagementSystem.Web.Services.Interfaces;

namespace TasksManagementSystem.Web.Pages.Tasks
{
    public class TasksBase : JwtVerificationComponent
    {
        [Parameter]
        public int ProjectId { get; set; }

        [Inject]
        public ITaskService _taskService {  get; set; }
        public IEnumerable<TaskDTO> TasksList { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (IsNavigated) return;

            string? userId = await LocalStorageManager.GetFromLocalStorage(JSRuntime, "userId");

            if(string.IsNullOrWhiteSpace(userId) )
                return;

            TasksList = await _taskService.GetEmployeeTasks(ProjectId, int.Parse(userId));
        }
    }
}
