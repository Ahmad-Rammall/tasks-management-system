using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Models.DTOs.ProjectDTOs;
using TasksManagementSystem.API.Helpers;
using TasksManagementSystem.API.Repositories.Interfaces;

namespace TasksManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetAllProjects()
        {
            try
            {
                var projects = await _projectRepository.GetAllProjects();
                if(projects == null)
                {
                    return NotFound("Projects Not Found. Try Again Later.");
                }

                return Ok(projects.ConvertToDto());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
