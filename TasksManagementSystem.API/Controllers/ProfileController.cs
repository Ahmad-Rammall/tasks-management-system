using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Models.DTOs.AuthDTOs;
using TaskManagementSystem.Models.DTOs.UserDTOs;
using TasksManagementSystem.API.Entities;
using TasksManagementSystem.API.Helpers;
using TasksManagementSystem.API.Repositories.Interfaces;

namespace TasksManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileRepository _profileRepository;
        public ProfileController(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository; 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllEmployees()
        {
            try
            {
                var employees = await _profileRepository.GetAllEmployees();
                if (employees == null)
                {
                    return NotFound();
                }

                return Ok(employees.ConvertToDto());
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Route("{userId:int}")]
        public async Task<ActionResult<UserDTO>> DeleteUser([FromRoute] int userId)
        {
            try
            {
                var user = await _profileRepository.DeleteEmployee(userId);
                if(user == null)
                {
                    return NotFound("User Not Found or Deleted!");
                }

                return Ok(user.ConvertToDto());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> AddEmployee([FromBody] UserRegisterDTO userRegisterDTO)
        {
            try
            {
                var user = await _profileRepository.AddEmployee(userRegisterDTO);
                if (user == null)
                    return BadRequest("User Already Exists");

                return Ok(user.ConvertToDto());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("{employeeId:int}")]
        public async Task<ActionResult<UserDTO>> UpdateEmployee([FromRoute] int employeeId, [FromBody] UserUpdateDTO userUpdateDTO)
        {
            try
            {
                var user = await _profileRepository.UpdateEmployee(employeeId, userUpdateDTO);
                if (user == null)
                    return NotFound("User Doesnt Exist");

                return Ok(user.ConvertToDto());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("updateWP/{employeeId:int}")]
        public async Task<ActionResult<UserDTO>> UpdateEmployeeWP([FromRoute] int employeeId, [FromBody] UserUpdateWithoutPassDTO userUpdateDTO)
        {
            try
            {
                var user = await _profileRepository.UpdateEmployeeWithoutPass(employeeId, userUpdateDTO);
                if (user == null)
                    return NotFound("User Doesnt Exist");

                return Ok(user.ConvertToDto());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}