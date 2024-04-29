using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using TasksManagementSystem.Web.Helpers;
using TasksManagementSystem.Web.Services.Interfaces;
using TasksManagementSystem.Web.Store.User;

namespace TasksManagementSystem.Web.Components.TaskComponent
{
    public class TaskBase : ComponentBase
    {
        [Parameter]
        public int TaskId { get; set; }

        [Parameter]
        public int UserId { get; set; }

        [Parameter]
        public string Title { get; set; }
        [Parameter]
        public string Description { get; set; }
        [Parameter]
        public bool IsCompleted { get; set; }

        [Parameter]
        public bool IsAdmin { get; set; }
        public string ErrorMessage { get; set; }

        [Inject]
        public ITaskService _taskService {  get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public IState<UserState> UserState { get; set; }
        public async Task SendApprovalRequest()
        {
            try
            {
                var response = await _taskService.SendRequest(TaskId);
               
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
        public async Task NavigateToComments()
        {
            if (UserState.Value.IsAdmin)
                navigationManager.NavigateTo($"/commentsAdmin/{TaskId}");
            else
                navigationManager.NavigateTo($"/comments/{TaskId}");
        }
    }
}
