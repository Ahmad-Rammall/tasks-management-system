using System.ComponentModel.DataAnnotations.Schema;

namespace TasksManagementSystem.API.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }

        [ForeignKey("UserProp")]
        public int UserId { get; set; }

        [ForeignKey("TaskProp")]
        public int TaskId { get; set; }
        public TaskEntity TaskProp { get; set; }
        public User UserProp { get; set; }
    }
}
