using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections;
using TaskManagementSystem.Models.DTOs.ProjectDTOs;
using TaskManagementSystem.Models.DTOs.TaskDTOs;
using TasksManagementSystem.Web.Helpers;
using TasksManagementSystem.Web.Services.Interfaces;

namespace TasksManagementSystem.Web.Pages.Employee
{
    public class EmployeeBase : ComponentBase
    {
        [Inject]
        public ITaskService _taskService { get; set; }

        [Inject]
        public IProjectService _projectService { get; set; }
        public string ErrorMessage { get; set; }
        public IEnumerable<ProjectDTO> ProjectsList { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        protected override async Task OnInitializedAsync()
        {
            string userId = await LocalStorageManager.GetFromLocalStorage(JSRuntime, "userId");
            ProjectsList = await _projectService.GetEmployeeProjects(int.Parse(userId));
        }
    }
}
