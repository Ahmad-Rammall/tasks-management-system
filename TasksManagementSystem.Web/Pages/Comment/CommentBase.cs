using Microsoft.AspNetCore.Components;

namespace TasksManagementSystem.Web.Pages.Comment
{
    public class CommentBase : ComponentBase
    {
        [Parameter]
        public int TaskId { get; set; }
    }
}
