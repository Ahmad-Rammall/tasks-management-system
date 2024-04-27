using Microsoft.AspNetCore.Components;
using TaskManagementSystem.Models.DTOs.TaskDTOs;
using TasksManagementSystem.Web.Services.Interfaces;

namespace TasksManagementSystem.Web.Pages.Admin.Requests
{
    public class RequestsBase : ComponentBase
    {
        [Inject]
        public ITaskService _taskService {  get; set; }
        public IEnumerable<TaskRequestDTO> RequestsList { get; set; }
        protected override async Task OnInitializedAsync()
        {
            RequestsList = await _taskService.GetAllRequests();
        }
    }
}
