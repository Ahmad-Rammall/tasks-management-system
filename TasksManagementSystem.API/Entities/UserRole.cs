namespace TasksManagementSystem.API.Entities
{
    public class UserRole
    {
        public int Id { get;set; }
        public string RoleName { get;set; }
        public List<User> Users { get; set; }
    }
}
