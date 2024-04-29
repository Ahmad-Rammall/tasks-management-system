using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Reflection.Metadata;
using TaskManagementSystem.Models.DTOs.TaskDTOs;
using TasksManagementSystem.Web.Helpers;
using TasksManagementSystem.Web.Services;
using TasksManagementSystem.Web.Services.Interfaces;

namespace TasksManagementSystem.Web.Pages.Admin.TasksAdmin
{
    public class TasksAdminBase : JwtVerificationComponent
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
        [Inject] public IAuthService _authService { get; set; }

        protected bool IsAdmin { get; set; } = true;
        protected override async Task OnInitializedAsync()
        {
            try
            {
                await base.OnInitializedAsync();
                if (IsNavigated) return;

                Methods methods = new Methods(_authService, jSRuntime);
                IsAdmin = await methods.IsUserAdmin();

                if(IsAdmin)
                    TasksList = await _taskService.GetProjectTasks(ProjectId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void RefreshPage()
        {
            NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);
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
