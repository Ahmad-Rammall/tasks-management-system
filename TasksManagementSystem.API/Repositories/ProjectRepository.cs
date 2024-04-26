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
        private async Task<bool> ProjectExists(int id)
        {
            return await _context.Projects.AnyAsync(p => p.Id == id);
        }
        private async Task<bool> EmployeeExists(int employeeId)
        {
            return await _context.Users.AnyAsync(e => e.Id == employeeId && !e.isDeleted);
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

        public async Task<Project> DeleteProject(int projectId)
        {
            if(! await ProjectExists(projectId))
            {
                return null;
            }

            var project = await _context.Projects.FindAsync(projectId);

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return project;
        }

        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<Project> UpdateProject(int projectId, ProjectToAddDTO projectToAddDTO)
        {
            if (!await ProjectExists(projectId))
            {
                return null;
            }

            var newProject = new Project
            {
                Title = projectToAddDTO.Title,
                Description = projectToAddDTO.Description,
            };

            _context.Projects.AddAsync(newProject);
            await _context.SaveChangesAsync();

            return newProject;
        }
        public async Task<IEnumerable<Project>> GetUserProjects(int employeeId)
        {
            if (!await EmployeeExists(employeeId))
                return null;

            return await _context.Projects
                .Include(p => p.Tasks)
                .Where(p => p.Tasks.Any(t => t.UserId == employeeId))
                .ToListAsync();
        }
    }
}
