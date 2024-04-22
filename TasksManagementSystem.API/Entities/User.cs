using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasksManagementSystem.API.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public bool isDeleted { get; set; }

        [ForeignKey(nameof(RoleId))]
        public UserRole Role { get; set; }
        public List<TaskEntity> Tasks { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
