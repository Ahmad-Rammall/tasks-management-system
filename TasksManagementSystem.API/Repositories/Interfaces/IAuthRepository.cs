using TaskManagementSystem.Models.DTOs.AuthDTOs;
using TaskManagementSystem.Models.DTOs.UserDTOs;
using TasksManagementSystem.API.Entities;

namespace TasksManagementSystem.API.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<LoginResponseDTO> LoginUser(UserLoginDTO userLoginDTO);
        Task<User> RegisterUser(UserRegisterDTO userRegisterDTO);
        Task<bool> IsUserAdmin(string token);
    }
}
