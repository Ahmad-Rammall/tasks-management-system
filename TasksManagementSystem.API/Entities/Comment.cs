using System.ComponentModel.DataAnnotations.Schema;

namespace TasksManagementSystem.API.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public TaskEntity TaskProp { get; set; }
        public User UserProp { get; set; }
    }
}
