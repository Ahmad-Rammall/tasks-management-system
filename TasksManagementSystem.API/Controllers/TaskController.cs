using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Models.DTOs.TaskDTOs;
using TasksManagementSystem.API.Entities;
using TasksManagementSystem.API.Helpers;
using TasksManagementSystem.API.Repositories.Interfaces;

namespace TasksManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepo;
        public TaskController(ITaskRepository taskRepo)
        {
            _taskRepo = taskRepo;
        }
        [HttpGet]
        [Route("{projectId:int}")]
        public async Task<ActionResult<IEnumerable<TaskDTO>>> GetProjectTasks([FromRoute] int projectId)
        {
            try
            {
                var tasks = await _taskRepo.GetProjectTasks(projectId);
                if(tasks == null)
                {
                    return NotFound("Error Finding Tasks");
                }
                
                return Ok(tasks.ConvertToDto());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<TaskDTO>> AddTaskToEmployee(TaskToAddDTO taskToAddDTO)
        {
            try
            {
                var task = await _taskRepo.AddTaskToEmployee(taskToAddDTO);
                if(task == null)
                {
                    return NotFound("Project or Employee Not Found.");
                }

                return Ok(task.ConvertToDto());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("sendRequest")]
        public async Task<ActionResult<TaskRequestDTO>> SendRequest([FromBody] int taskid)
        {
            try
            {
                var request = await _taskRepo.SendApprovalRequest(taskid);
                if(request == null)
                {
                    return NotFound("Task Doesnt Exist!");
                }

                return Ok(request.ConvertToDto());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("acceptRequest/{requestId:int}")]
        public async Task<ActionResult<TaskRequestDTO>> AcceptRequest([FromRoute] int requestId)
        {
            try
            {
                var request = await _taskRepo.AcceptRequest(requestId);
                if(request == null)
                {
                    return NotFound("Request Doesnt Exist!");
                }

                return Ok(request.ConvertToDto());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("rejectRequest/{requestId:int}")]
        public async Task<ActionResult<TaskRequestDTO>> RejectRequest([FromRoute] int requestId)
        {
            try
            {
                var request = await _taskRepo.RejectRequest(requestId);
                if (request == null)
                {
                    return NotFound("Request Doesnt Exist!");
                }

                return Ok(request.ConvertToDto());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    
}
