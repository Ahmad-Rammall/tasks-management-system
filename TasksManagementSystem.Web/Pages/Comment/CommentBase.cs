using Microsoft.AspNetCore.Components;
using TaskManagementSystem.Models.DTOs.CommentDTOs;
using TasksManagementSystem.Web.Services.Interfaces;

namespace TasksManagementSystem.Web.Pages.Comment
{
    public class CommentBase : ComponentBase
    {
        [Parameter]
        public int TaskId { get; set; }
        [Inject]
        public ICommentService _commentService { get; set; }
        public IEnumerable<CommentDTO> CommentsList { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                CommentsList = await _commentService.GetAllTaskComments(TaskId);
                foreach(var x in CommentsList)
                {
                    Console.WriteLine(x.Content + " : " + x.UserId);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
}
    }
}
