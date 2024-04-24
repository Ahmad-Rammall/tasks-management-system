using Microsoft.EntityFrameworkCore;
using TasksManagementSystem.API.Data;
using TasksManagementSystem.API.Entities;
using TasksManagementSystem.API.Repositories.Interfaces;

namespace TasksManagementSystem.API.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;
        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }
        private async Task<bool> ProjectExists(int projectId)
        {
            return await _context.Projects.AnyAsync(p => p.Id == projectId);
        }
        public Task<TaskApprovalRequest> AcceptRequest(int taskId)
        {
            throw new NotImplementedException();
        }

        public Task<TaskEntity> AddTaskToEmployee(int projectId, int employeeId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TaskEntity>> GetProjectTasks(int projectId)
        {
            if(! await ProjectExists(projectId))
            {
                return null;
            }

            return await _context.Tasks.Where(task => task.ProjectId == projectId).ToListAsync();
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
