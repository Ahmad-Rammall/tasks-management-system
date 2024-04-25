using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using TaskManagementSystem.Models.DTOs.AuthDTOs;
using TaskManagementSystem.Models.DTOs.CommentDTOs;
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
                RoleName = user.Role.RoleName,
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
        public static TaskDTO ConvertToDto(this TaskEntity task)
        {
            return new TaskDTO
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
            };
        }
        public static TaskRequestDTO ConvertToDto(this TaskApprovalRequest request)
        {
            return new TaskRequestDTO
            {
                Id = request.Id,
                TaskId = request.TaskId,
                IsApproved = request.IsApproved
            };
        }
        public static IEnumerable<CommentDTO> ConvertToDto(this IEnumerable<Comment> comments)
        {

            List<CommentDTO> commentsList = comments.Select(comment =>
                new CommentDTO
                {
                    Id = comment.Id,
                    Content = comment.Content,
                    UserId = comment.UserId,
                    TaskId = comment.TaskId
                }).ToList();

            return commentsList;
        }
        public static CommentDTO ConvertToDto(this Comment comment)
        {
            return new CommentDTO
            {
                Id = comment.Id,
                Content = comment.Content,
                UserId = comment.UserId,
                TaskId = comment.TaskId
            };
        }
    }
}
