namespace TasksManagementSystem.Web.Store.User
{
    public class ImplementUserAction
    {
        public UserState User { get; }

        public ImplementUserAction(UserState user)
        {
            User = user;
        }
    }
}
