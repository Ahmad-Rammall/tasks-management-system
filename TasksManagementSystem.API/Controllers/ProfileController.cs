using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
                    return NotFound("User Not Found!");
                }

                return Ok(user.ConvertToDto());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
