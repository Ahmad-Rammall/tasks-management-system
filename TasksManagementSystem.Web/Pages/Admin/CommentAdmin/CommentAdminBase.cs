﻿using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using TaskManagementSystem.Models.DTOs.CommentDTOs;
using TasksManagementSystem.Web.Helpers;
using TasksManagementSystem.Web.Services.Interfaces;
using TasksManagementSystem.Web.Store.User;

namespace TasksManagementSystem.Web.Pages.Admin.CommentAdmin
{
    public class CommentAdminBase : ComponentBase
    {
        [Parameter]
        public int TaskId { get; set; }

        [Inject]
        public ICommentService _commentService { get; set; }

        [Inject]
        public IJSRuntime jSRuntime { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }
        public IEnumerable<CommentDTO> CommentsList { get; set; }
        public string CommentContent { get; set; }

        [Inject]
        public IState<UserState> UserState { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                CommentsList = await _commentService.GetAllTaskComments(TaskId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task SendComment()
        {
            int userId = UserState.Value.UserId;

            CommentToAddDTO commentDto = new CommentToAddDTO
            {
                TaskId = TaskId,
                UserId = userId,
                Content = CommentContent
            };

            await _commentService.AddComment(commentDto);            
        }
    }
}
