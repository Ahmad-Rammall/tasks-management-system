using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagementSystem.Models.DTOs.AuthDTOs;
using TaskManagementSystem.Models.DTOs.UserDTOs;
using TasksManagementSystem.API.Entities;
using TasksManagementSystem.API.Helpers;
using TasksManagementSystem.API.Repositories.Interfaces;

namespace TasksManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;

        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }
        

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterDTO userRegisterDTO)
        {
            try
            {
                var user = await _authRepo.RegisterUser(userRegisterDTO);

                if(user == null)
                {
                    return BadRequest("User Not Added");
                }
                return Ok(user);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginDTO userLoginDTO)
        {
            try
            {
                var user = await _authRepo.LoginUser(userLoginDTO);
                if(user == null)
                {
                    return NotFound("User Not Found");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
