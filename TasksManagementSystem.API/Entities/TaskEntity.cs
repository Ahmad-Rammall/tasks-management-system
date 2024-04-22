using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasksManagementSystem.API.Entities
{
    public class TaskEntity
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [ForeignKey("UserProp")]
        public int UserId {  get; set; }

        [ForeignKey("ProjectProp")]
        public int ProjectId { get; set; }
        public bool IsCompleted {  get; set; }
        public User UserProp {  get; set; }
        public Project ProjectProp { get; set; }
    }
}
