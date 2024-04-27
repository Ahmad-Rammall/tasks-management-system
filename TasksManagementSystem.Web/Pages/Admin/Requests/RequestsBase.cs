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
        public int SelectedRequestId { get; set; }
        public bool ShowAcceptModal { get; set; } = false;
        public bool ShowRejectModal { get; set; } = false;
        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            RequestsList = await _taskService.GetAllRequests();
        }
        public async Task AcceptRequest()
        {
            try
            {
                var response = await _taskService.AcceptRequest(SelectedRequestId);
                ShowAcceptModal = false;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
        public async Task RejectRequest()
        {
            try
            {
                var response = await _taskService.RejectRequest(SelectedRequestId);
                ShowRejectModal = false;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
