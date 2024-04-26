using Microsoft.AspNetCore.Components;
using System.Collections;
using TaskManagementSystem.Models.DTOs.TaskDTOs;
using TasksManagementSystem.Web.Services.Interfaces;

namespace TasksManagementSystem.Web.Pages.Employee
{
    public class EmployeeBase : ComponentBase
    {
        [Inject]
        public ITaskService _taskService { get; set; }
        public string ErrorMessage { get; set; }
        public IEnumerable<TaskDTO> Tasks { get; set; }

    }
}
