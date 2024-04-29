using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using TaskManagementSystem.Models.DTOs.CommentDTOs;
using TasksManagementSystem.Web.Helpers;
using TasksManagementSystem.Web.Services.Interfaces;
using TasksManagementSystem.Web.Store.User;

namespace TasksManagementSystem.Web.Pages.Comment
{
    public class CommentBase : ComponentBase
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
                foreach (var x in CommentsList)
                {
                    Console.WriteLine(x.Content + " : " + x.UserId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task SendComment()
        {
            string userId = await LocalStorageManager.GetFromLocalStorage(jSRuntime, "userId");
            Console.WriteLine(userId);
            CommentToAddDTO commentDto = new CommentToAddDTO
            {
                TaskId = TaskId,
                UserId = int.Parse(userId),
                Content = CommentContent
            };

            var response = await _commentService.AddComment(commentDto);
            //NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);

        }
    }
}
