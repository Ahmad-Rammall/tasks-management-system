using TaskManagementSystem.Models.DTOs.TaskDTOs;

namespace TasksManagementSystem.Web.Services.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDTO>> GetProjectTasks(int projectId);
        Task<IEnumerable<TaskDTO>> GetEmployeeTasks(int projectId, int employeeId);
        Task<IEnumerable<TaskRequestDTO>> GetAllRequests();
        Task<TaskDTO> AddTaskToEmployee(TaskToAddDTO taskToAddDTO);
        Task<TaskRequestDTO> SendRequest(int taskid);
        Task<TaskRequestDTO> AcceptRequest(int requestId);
        Task<TaskRequestDTO> RejectRequest(int requestId);

    }
}
