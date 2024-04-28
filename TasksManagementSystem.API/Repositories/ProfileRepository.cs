﻿using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Models.DTOs.AuthDTOs;
using TaskManagementSystem.Models.DTOs.UserDTOs;
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
        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(user => user.Username.Equals(username));
        }
        public async Task<User> AddEmployee(UserRegisterDTO userRegisterDTO)
        {
            if (await UserExists(userRegisterDTO.Username))
            {
                return null;
            }

            // Hash Password
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userRegisterDTO.Password);

            var user = new User
            {
                Username = userRegisterDTO.Username,
                FullName = userRegisterDTO.FullName,
                RoleId = await GetEmployeeRoleId(),
                Password =hashedPassword,
                isDeleted = false
            };

            _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> DeleteEmployee(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null || user.isDeleted == true)
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

        public async Task<User> UpdateEmployee(int employeeId, UserUpdateDTO userUpdateDTO)
        {
            var user = await _context.Users.FindAsync(employeeId);

            if(user == null)
            {
                return null;
            }

            // Hash Password
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userUpdateDTO.Password);

            user.Username = userUpdateDTO.Username;
            user.Password = hashedPassword;
            user.FullName = userUpdateDTO.FullName;
            user.isDeleted = userUpdateDTO.IsDeleted;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return user;

        }

        public async Task<User> UpdateEmployeeWithoutPass(int employeeId, UserUpdateWithoutPassDTO userUpdateDTO)
        {
            var user = await _context.Users.FindAsync(employeeId);

            if (user == null)
            {
                return null;
            }

            user.Username = userUpdateDTO.Username;
            user.FullName = userUpdateDTO.FullName;
            user.isDeleted = userUpdateDTO.IsDeleted;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
