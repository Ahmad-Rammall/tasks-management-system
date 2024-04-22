using TaskManagementSystem.Models.DTOs.AuthDTOs;
using TaskManagementSystem.Models.DTOs.UserDTOs;
using TasksManagementSystem.API.Entities;

namespace TasksManagementSystem.API.Helpers
{
    public static class DtoConversions
    {
        public static UserDTO ConvertToDto(this User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                FullName = user.FullName,
                Username = user.Username,
                RoleName = user.Role.RoleName,
            };
        }
        public static LoginResponseDTO ConvertToDto(this User user, string token)
        {
            return new LoginResponseDTO
            {
                User = user.ConvertToDto(),
                Token= token
          
            };
        }
    }
}
