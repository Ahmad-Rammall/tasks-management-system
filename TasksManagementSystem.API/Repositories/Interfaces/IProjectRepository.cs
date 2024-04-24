using TaskManagementSystem.Models.DTOs.ProjectDTOs;
using TasksManagementSystem.API.Entities;

namespace TasksManagementSystem.API.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllProjects();
        Task<Project> AddProject(ProjectToAddDTO projectToAddDTO);
        Task<Project> UpdateProject(int projectId, ProjectToAddDTO projectToAddDTO);
        Task<Project> DeleteProject(int projectId);
    }
}
