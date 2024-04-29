using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using TaskManagementSystem.Models.DTOs.CommentDTOs;
using TasksManagementSystem.Web.Helpers;
using TasksManagementSystem.Web.Services;
using TasksManagementSystem.Web.Services.Interfaces;
using TasksManagementSystem.Web.Store.User;

namespace TasksManagementSystem.Web.Pages.Admin.CommentAdmin
{
    public class CommentAdminBase : JwtVerificationComponent
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
        [Inject] IAuthService _authService { get; set; }
        protected bool IsAdmin { get; set; } = true;
        protected override async Task OnInitializedAsync()
        {
            try
            {
                await base.OnInitializedAsync();
                if (IsNavigated) return;

                Methods methods = new Methods(_authService, jSRuntime);
                IsAdmin = await methods.IsUserAdmin();

                if (IsAdmin)
                    CommentsList = await _commentService.GetAllTaskComments(TaskId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task SendComment()
        {
            string userId = await LocalStorageManager.GetFromLocalStorage(jSRuntime, "userId");

            CommentToAddDTO commentDto = new CommentToAddDTO
            {
                TaskId = TaskId,
                UserId = int.Parse(userId),
                Content = CommentContent
            };

            await _commentService.AddComment(commentDto);            
        }
    }
}
