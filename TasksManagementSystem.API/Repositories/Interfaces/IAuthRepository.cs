﻿using TaskManagementSystem.Models.DTOs.AuthDTOs;
using TaskManagementSystem.Models.DTOs.UserDTOs;
using TasksManagementSystem.API.Entities;

namespace TasksManagementSystem.API.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> LoginUser(UserLoginDTO userLoginDTO);
        Task<UserDTO> RegisterUser(UserRegisterDTO userRegisterDTO);
    }
}
