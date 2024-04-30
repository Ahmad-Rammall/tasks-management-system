using TaskManagementSystem.Models.DTOs.TaskDTOs;
using TasksManagementSystem.API.Entities;

namespace TasksManagementSystem.API.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskEntity>> GetProjectTasks(int projectId);
        Task<IEnumerable<TaskEntity>> GetEmployeeTasks(int projectId, int employeeId);
        Task<IEnumerable<TaskApprovalRequest>> GetAllRequests();
        Task<TaskEntity> AddTaskToEmployee(TaskToAddDTO taskToAddDTO);
        Task<TaskApprovalRequest> SendApprovalRequest(int taskId);
        Task<TaskApprovalRequest> AcceptRequest(int requestId);
        Task<TaskApprovalRequest> RejectRequest(int requestId);
    }
}
