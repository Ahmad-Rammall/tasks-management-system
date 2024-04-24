using TasksManagementSystem.API.Entities;
using TasksManagementSystem.API.Repositories.Interfaces;

namespace TasksManagementSystem.API.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        public Task<TaskApprovalRequest> AcceptRequest(int taskId)
        {
            throw new NotImplementedException();
        }

        public Task<TaskEntity> AddTaskToEmployee(int projectId, int employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TaskEntity>> GetProjectTasks(int projectId)
        {
            throw new NotImplementedException();
        }

        public Task<TaskApprovalRequest> RejectRequest(int taskId)
        {
            throw new NotImplementedException();
        }

        public Task<TaskApprovalRequest> SendApprovalRequest(int taskId)
        {
            throw new NotImplementedException();
        }
    }
}
