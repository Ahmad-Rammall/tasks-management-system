using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagementSystem.Models.DTOs.AuthDTOs;
using TaskManagementSystem.Models.DTOs.UserDTOs;
using TasksManagementSystem.API.Data;
using TasksManagementSystem.API.Entities;
using TasksManagementSystem.API.Helpers;
using TasksManagementSystem.API.Repositories.Interfaces;

namespace TasksManagementSystem.API.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthRepository(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.RoleId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _config.GetSection("JwtConfig:secretKey").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public async Task<LoginResponseDTO> LoginUser(UserLoginDTO userLoginDTO)
        {
            var user = await _context.Users.Include(user => user.Role)
                .FirstOrDefaultAsync(user => user.Username == userLoginDTO.Username);

            if (user == null || user.isDeleted)
                return null;

            if (BCrypt.Net.BCrypt.Verify(userLoginDTO.Password, user.Password))
            {
                var token = CreateToken(user);
                return user.ConvertToDto(token);
            }
            else
                return null;

        }
        private async Task<UserRole> GetUserRole(string roleName)
        {
            var userRole = await _context.UserRoles.FirstOrDefaultAsync(role => role.RoleName == roleName);
            return userRole;
        }

        public async Task<User> RegisterUser(UserRegisterDTO userRegisterDTO)
        {
            if (string.IsNullOrWhiteSpace(userRegisterDTO.Username)
                || string.IsNullOrWhiteSpace(userRegisterDTO.FullName)
                || string.IsNullOrWhiteSpace(userRegisterDTO.Password))
                return null;

            // Check if Username exists
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == userRegisterDTO.Username);

            if (user != null)
            {
                return null;
            }

            // Get User Role
            var userRole = await GetUserRole("User");

            // Hash Password
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userRegisterDTO.Password);

            var newUser = new User
            {
                Username = userRegisterDTO.Username,
                FullName = userRegisterDTO.FullName,
                Password = hashedPassword,
                RoleId = userRole.Id,
            };

            // Add User to DB
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return newUser;
        }
    }
}
