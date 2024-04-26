using Microsoft.AspNetCore.Components;
using TasksManagementSystem.Web.Services.Interfaces;

namespace TasksManagementSystem.Web.Components.CommentComponent
{
    public class CommentBase : ComponentBase
    {
        

        [Parameter]
        public string Fullname { get; set; }
        [Parameter]
        public string Content { get; set; }

        
    }
}
