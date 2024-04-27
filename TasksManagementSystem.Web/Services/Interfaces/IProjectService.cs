using TaskManagementSystem.Models.DTOs.ProjectDTOs;

namespace TasksManagementSystem.Web.Services.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDTO>> GetEmployeeProjects(int employeeId);
        Task<IEnumerable<ProjectDTO>> GetAllProjects();
    }
}
