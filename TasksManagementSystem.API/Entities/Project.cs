using System.ComponentModel.DataAnnotations;

namespace TasksManagementSystem.API.Entities
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<TaskEntity> Tasks { get; set; }
    }
}
