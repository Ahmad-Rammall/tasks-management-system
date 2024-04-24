﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Models.DTOs.TaskDTOs;
using TasksManagementSystem.API.Helpers;
using TasksManagementSystem.API.Repositories.Interfaces;

namespace TasksManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
    }

    
}
