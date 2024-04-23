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

        public async Task<User> DeleteEmployee(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if(user == null || user.isDeleted == true)
            {
                return null;
            }

            user.isDeleted = true;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return user;
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
