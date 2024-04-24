using TaskManagementSystem.Models.DTOs.TaskDTOs;
using TasksManagementSystem.API.Entities;

namespace TasksManagementSystem.API.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskEntity>> GetProjectTasks(int projectId);
        Task<TaskEntity> AddTaskToEmployee(TaskToAddDTO taskToAddDTO);
        Task<TaskApprovalRequest> SendApprovalRequest(int taskId);
        Task<TaskApprovalRequest> AcceptRequest(int taskId);
        Task<TaskApprovalRequest> RejectRequest(int taskId);
    }
}
