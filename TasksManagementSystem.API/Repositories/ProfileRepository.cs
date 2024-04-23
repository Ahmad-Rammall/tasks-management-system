using TaskManagementSystem.Models.DTOs.AuthDTOs;
using TasksManagementSystem.API.Entities;
using TasksManagementSystem.API.Repositories.Interfaces;

namespace TasksManagementSystem.API.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        public Task<User> AddEmployee(UserRegisterDTO userRegisterDTO)
        {
            throw new NotImplementedException();
        }

        public Task<User> DeleteEmployee(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetAllEmployees()
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateEmployee(UserRegisterDTO userRegisterDTO)
        {
            throw new NotImplementedException();
        }
    }
}
