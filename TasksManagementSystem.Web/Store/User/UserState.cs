using Fluxor;
using TaskManagementSystem.Models.DTOs.UserDTOs;

namespace TasksManagementSystem.Web.Store.User
{
    [FeatureState]

    public class UserState
    {
        public int UserId { get; }
        public string UserName { get; }
        public string FullName { get; }
        public bool IsAdmin { get; }
        private UserState() { }

        public UserState(UserDTO user)
        {
            UserId = user.Id;
            UserName = user.Username;
            FullName = user.FullName;
            if (user.RoleId == 1)
                IsAdmin = true;
            else
                IsAdmin = false;
        }
    }
}
