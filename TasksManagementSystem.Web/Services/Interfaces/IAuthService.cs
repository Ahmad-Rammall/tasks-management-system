using TaskManagementSystem.Models.DTOs.AuthDTOs;

namespace TasksManagementSystem.Web.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> LoginUser(UserLoginDTO userLoginDTO);
        Task<UserRegisterDTO> RegisterUser(UserRegisterDTO userRegisterDTO);
    }
}
