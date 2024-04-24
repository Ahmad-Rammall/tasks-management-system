using System.ComponentModel.DataAnnotations.Schema;

namespace TasksManagementSystem.API.Entities
{
    public class TaskApprovalRequest
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public bool IsApproved { get; set; }

        [ForeignKey(nameof(TaskId))]
        public TaskEntity TaskProp { get; set; }
    }
}
