using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Models.DTOs.ProjectDTOs;
using TasksManagementSystem.API.Entities;
using TasksManagementSystem.API.Helpers;
using TasksManagementSystem.API.Repositories.Interfaces;

namespace TasksManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
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
                if (projects == null)
                {
                    return NotFound("Projects Not Found. Try Again Later.");
                }

                return Ok(projects.ConvertToDto());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{employeeId:int}")]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetUserProjects([FromRoute] int employeeId)
        {
            try
            {
                var projects = await _projectRepository.GetUserProjects(employeeId);
                if (projects == null)
                {
                    return NotFound("Error Finding Tasks");
                }

                return Ok(projects.ConvertToDto());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProjectDTO>> AddProject([FromBody] ProjectToAddDTO projectDTO)
        {
            try
            {
                var project = await _projectRepository.AddProject(projectDTO);
                if (project == null)
                {
                    return BadRequest();
                }

                return Ok(project.ConvertToDto());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{projectId:int}")]
        public async Task<ActionResult<ProjectDTO>> UpdateProject([FromRoute] int projectId, [FromBody] ProjectToAddDTO projectDTO)
        {
            try
            {
                var project = await _projectRepository.UpdateProject(projectId, projectDTO);
                if (project == null)
                    return NotFound("Project Not Found");

                return Ok(project.ConvertToDto());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{projectId:int}")]
        public async Task<ActionResult<ProjectDTO>> DeleteProject([FromRoute] int projectId)
        {
            try
            {
                var project = await _projectRepository.DeleteProject(projectId);
                if (project == null)
                    return NotFound("Project Not Found");

                return Ok(project.ConvertToDto());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
