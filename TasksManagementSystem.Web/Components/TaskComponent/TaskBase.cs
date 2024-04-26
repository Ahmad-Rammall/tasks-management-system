﻿using Microsoft.AspNetCore.Components;
using TasksManagementSystem.Web.Services.Interfaces;

namespace TasksManagementSystem.Web.Components.TaskComponent
{
    public class TaskBase : ComponentBase
    {
        [Parameter]
        public int TaskId { get; set; }

        [Parameter]
        public string Title { get; set; }
        [Parameter]
        public string Description { get; set; }
        [Parameter]
        public bool IsCompleted { get; set; }
        public string ErrorMessage { get; set; }

        [Inject]
        public ITaskService _taskService {  get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }
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
        public void NavigateToComments()
        {
            navigationManager.NavigateTo($"/comments/{TaskId}");
        }
    }
}
