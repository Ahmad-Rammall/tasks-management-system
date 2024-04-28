using TaskManagementSystem.Models.DTOs.ProjectDTOs;

namespace TasksManagementSystem.Web.Services.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDTO>> GetEmployeeProjects(int employeeId);
        Task<IEnumerable<ProjectDTO>> GetAllProjects();
        Task<ProjectDTO> DeleteProject(int projectId);
        Task<ProjectDTO> UpdateProject(int projectId, ProjectToAddDTO projectDTO);
        Task<ProjectDTO> AddProject(ProjectToAddDTO projectDTO);

    }
}
