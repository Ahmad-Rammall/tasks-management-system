using TaskManagementSystem.Models.DTOs.TaskDTOs;

namespace TasksManagementSystem.Web.Services.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDTO>> GetProjectTasks(int projectId);
        Task<TaskRequestDTO> SendRequest(int taskid);
    }
}
