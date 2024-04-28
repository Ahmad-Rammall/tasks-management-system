using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Reflection.Metadata;
using TaskManagementSystem.Models.DTOs.TaskDTOs;
using TasksManagementSystem.Web.Services.Interfaces;

namespace TasksManagementSystem.Web.Pages.Admin.TasksAdmin
{
    public class TasksAdminBase : ComponentBase
    {
        [Parameter]
        public int ProjectId { get; set; }

        [Inject]
        public ITaskService _taskService { get; set; }

        [Inject]
        public IJSRuntime jSRuntime { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public IEnumerable<TaskDTO> TasksList { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        private void RefreshPage()
        {
            NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);
        }
        protected override async Task OnInitializedAsync()
        {
            TasksList = await _taskService.GetProjectTasks(ProjectId);
        }
        public async Task AddTaskToEmployee()
        {
            try
            {
                ErrorMessage = "";

                TaskToAddDTO taskToAddDTO = new TaskToAddDTO
                {
                    Title = Title,
                    Description = Description,
                    ProjectId = ProjectId,
                    UserId = UserId
                };
                await _taskService.AddTaskToEmployee(taskToAddDTO);
                SuccessMessage = "Task Added";
                RefreshPage();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                SuccessMessage = "";
            }
        }
    }
}
