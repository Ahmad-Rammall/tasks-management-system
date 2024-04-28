using TaskManagementSystem.Models.DTOs.AuthDTOs;
using TaskManagementSystem.Models.DTOs.UserDTOs;
using TasksManagementSystem.API.Entities;

namespace TasksManagementSystem.API.Repositories.Interfaces
{
    public interface IProfileRepository
    {
        Task<IEnumerable<User>> GetAllEmployees();
        Task<User> AddEmployee(UserRegisterDTO userRegisterDTO);
        Task<User> UpdateEmployee(int employeeId, UserUpdateDTO userUpdateDTO);
        Task<User> UpdateEmployeeWithoutPass(int employeeId, UserUpdateWithoutPassDTO userUpdateDTO);
        Task<User> DeleteEmployee(int id);
    }
}
