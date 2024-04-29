using Fluxor;

namespace TasksManagementSystem.Web.Store.User
{
    public class Reducers
    {
        [ReducerMethod]
        public static UserState ReduceImplementUserAction(UserState state, ImplementUserAction action)
        {
            return action.User;
        }
    }
}
