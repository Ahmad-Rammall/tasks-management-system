using TaskManagementSystem.Models.DTOs.TaskDTOs;

namespace TasksManagementSystem.Web.Services.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDTO>> GetProjectTasks(int projectId);
        Task<IEnumerable<TaskRequestDTO>> GetAllRequests();
        Task<TaskRequestDTO> SendRequest(int taskid);
        Task<TaskRequestDTO> AcceptRequest(int requestId);
        Task<TaskRequestDTO> RejectRequest(int requestId);

    }
}
