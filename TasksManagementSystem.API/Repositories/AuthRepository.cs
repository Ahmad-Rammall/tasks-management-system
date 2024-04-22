using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
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
        public AuthRepository(AppDbContext context)
        {
            _context = context;
        }
        public Task<UserLoginDTO> LoginUser(UserLoginDTO userLoginDTO)
        {
            throw new NotImplementedException();
        }
        private async Task<UserRole> GetUserRole(string roleName)
        {
            var userRole = await _context.UserRoles.FirstOrDefaultAsync(role => role.RoleName == roleName);
            return userRole;
        }

        public async Task<UserDTO> RegisterUser(UserRegisterDTO userRegisterDTO)
        {
            if (string.IsNullOrWhiteSpace(userRegisterDTO.Username)
                || string.IsNullOrWhiteSpace(userRegisterDTO.FullName)
                || string.IsNullOrWhiteSpace(userRegisterDTO.Password))
                return null;

            // Check if Username exists
            var user = _context.Users.FirstOrDefault(u => u.Username == userRegisterDTO.Username);

            if (user != null) {
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

            return newUser.ConvertToDto(userRole.RoleName);
        }
    }
}
