using TaskManagementSystem.Models.DTOs.AuthDTOs;
using TaskManagementSystem.Models.DTOs.TaskDTOs;
using TaskManagementSystem.Models.DTOs.UserDTOs;

namespace TasksManagementSystem.Web.Services.Interfaces
{
    public interface IProfileService
    {
        Task<IEnumerable<UserDTO>> GetAllEmployees();
        Task<UserDTO> DeleteUser(int userId);
        Task<UserDTO> AddEmployee(UserRegisterDTO userRegisterDTO);
        Task<UserDTO> UpdateEmployee(int employeeId, UserUpdateDTO userUpdateDTO);
        Task<UserDTO> UpdateEmployeeWP(int employeeId, UserUpdateWithoutPassDTO userUpdateDTO);

    }
}
