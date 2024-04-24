using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Models.DTOs.ProjectDTOs;
using TasksManagementSystem.API.Data;
using TasksManagementSystem.API.Entities;
using TasksManagementSystem.API.Repositories.Interfaces;

namespace TasksManagementSystem.API.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;
        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Project> AddProject(ProjectToAddDTO projectToAddDTO)
        {
            var newProject = new Project
            {
                Title = projectToAddDTO.Title,
                Description = projectToAddDTO.Description,
            };

            _context.Projects.AddAsync(newProject);
            await _context.SaveChangesAsync();

            return newProject;
        }

        public Task<Project> DeleteProject(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            return await _context.Projects.ToListAsync();
        }

        public Task<Project> UpdateProject(int projectId, ProjectToAddDTO projectToAddDTO)
        {
            throw new NotImplementedException();
        }
    }
}
