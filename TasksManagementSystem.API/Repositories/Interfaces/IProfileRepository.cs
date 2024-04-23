using TaskManagementSystem.Models.DTOs.AuthDTOs;
using TasksManagementSystem.API.Entities;

namespace TasksManagementSystem.API.Repositories.Interfaces
{
    public interface IProfileRepository
    {
        Task<User> GetAllEmployees();
        Task<User> AddEmployee(UserRegisterDTO userRegisterDTO);
        Task<User> UpdateEmployee(UserRegisterDTO userRegisterDTO);
        Task<User> DeleteEmployee(int Id);
    }
}
