using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Models.DTOs.AuthDTOs;
using TasksManagementSystem.API.Data;
using TasksManagementSystem.API.Entities;
using TasksManagementSystem.API.Repositories.Interfaces;

namespace TasksManagementSystem.API.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly AppDbContext _context;
        public ProfileRepository(AppDbContext context)
        {
            _context = context;
        }
        private async Task<int> GetEmployeeRoleId()
        {
            var role = await _context.UserRoles.FirstOrDefaultAsync(r => r.RoleName == "User");
            return role.Id;
        }
        public Task<User> AddEmployee(UserRegisterDTO userRegisterDTO)
        {
            throw new NotImplementedException();
        }

        public Task<User> DeleteEmployee(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAllEmployees()
        {
            int roleId = await GetEmployeeRoleId();
            return await _context.Users.Where(u => u.RoleId == roleId).ToListAsync();
        }

        public Task<User> UpdateEmployee(UserRegisterDTO userRegisterDTO)
        {
            throw new NotImplementedException();
        }
    }
}
