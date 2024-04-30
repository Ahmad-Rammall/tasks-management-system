using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using TaskManagementSystem.Models.DTOs.TaskDTOs;
using TasksManagementSystem.Web.Components.DeleteConfirmationModal;
using TasksManagementSystem.Web.Helpers;
using TasksManagementSystem.Web.Services;
using TasksManagementSystem.Web.Services.Interfaces;

namespace TasksManagementSystem.Web.Pages.Admin.Requests
{
    public class RequestsBase : JwtVerificationComponent
    {
        [Inject] public ITaskService _taskService {  get; set; }
        public IEnumerable<TaskRequestDTO> RequestsList { get; set; }
        public int SelectedRequestId { get; set; }
        public bool ShowAcceptModal { get; set; } = false;
        public bool ShowRejectModal { get; set; } = false;
        public string ErrorMessage { get; set; }
        [Inject] public IAuthService _authService { get; set; }
        [Inject] public IJSRuntime jSRuntime { get; set; }
        public DeleteModalBase? RejectModal { get; set; }
        public DeleteModalBase? AcceptModal { get; set; }

        public void OpenRejectModal()
        {
            RejectModal.ShowModal();
            Console.WriteLine(SelectedRequestId);
        }
        public void OpenAcceptModal()
        {
            AcceptModal.ShowModal();
            Console.WriteLine(SelectedRequestId);
        }
        protected bool IsAdmin { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (IsNavigated) return;

            Methods methods = new Methods(_authService, jSRuntime);
            IsAdmin = await methods.IsUserAdmin();

            if (IsAdmin)
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
