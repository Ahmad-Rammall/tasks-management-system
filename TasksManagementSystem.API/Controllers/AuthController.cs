using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using TaskManagementSystem.Models.DTOs.AuthDTOs;
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
        public async Task<ActionResult<UserRegisterDTO>> RegisterUser([FromBody] UserRegisterDTO userRegisterDTO)
        {
            try
            {
                var user = await _authRepo.RegisterUser(userRegisterDTO);

                if(user == null)
                {
                    return BadRequest("User Not Added");
                }
                return Ok(user.ConvertToDto());
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

        //[HttpGet]
        [HttpPost]
        //[Route("{token:string}")]
        public async Task<IActionResult> IsUserAdmin([FromBody] string token)
        {
            try
            {
                var response = await _authRepo.IsUserAdmin(token);

                return Ok(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
