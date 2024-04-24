﻿using TaskManagementSystem.Models.DTOs.AuthDTOs;
using TaskManagementSystem.Models.DTOs.ProjectDTOs;
using TaskManagementSystem.Models.DTOs.TaskDTOs;
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
                RoleName = "User",
            };
        }
        public static LoginResponseDTO ConvertToDto(this User user, string token)
        {
            return new LoginResponseDTO
            {
                User = user.ConvertToDto(),
                Token = token

            };
        }
        public static IEnumerable<UserDTO> ConvertToDto(this IEnumerable<User> users)
        {
            List<UserDTO> userDTOs = users.Select(user =>
                new UserDTO
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Username = user.Username,
                    RoleName = "User",
                }).ToList();

            return userDTOs;
        }
        public static IEnumerable<ProjectDTO> ConvertToDto(this IEnumerable<Project> projects)
        {
            List<ProjectDTO> projectsList = projects.Select(project =>
                new ProjectDTO
                {
                    Id = project.Id,
                    Title = project.Title,
                    Description = project.Description,
                }).ToList();

            return projectsList;
        }
        public static ProjectDTO ConvertToDto(this Project project)
        {
            return new ProjectDTO
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
            };
        }
        public static IEnumerable<TaskDTO> ConvertToDto(this IEnumerable<TaskEntity> tasks)
        {
            List<TaskDTO> tasksList = tasks.Select(task =>
                new TaskDTO
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    IsCompleted = task.IsCompleted,
                }).ToList();
            return tasksList;
        }
    }
}
