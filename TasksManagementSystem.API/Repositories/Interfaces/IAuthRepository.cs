using TaskManagementSystem.Models.DTOs.AuthDTOs;
using TaskManagementSystem.Models.DTOs.UserDTOs;

namespace TasksManagementSystem.API.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<UserLoginDTO> LoginUser(UserLoginDTO userLoginDTO);
        Task<UserDTO> RegisterUser(UserRegisterDTO userRegisterDTO);
    }
}
