using System.ComponentModel.DataAnnotations;

namespace TasksManagementSystem.API.Entities
{
    public class TaskEntity
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId {  get; set; }
        public int ProjectId { get; set; }
        public bool IsCompleted {  get; set; }
        public User UserProp {  get; set; }
        public Project ProjectProp { get; set; }
        public List<Comment> Comments { get; set; }
        public TaskApprovalRequest TaskApprovalRequest {  get; set; }
    }
}
