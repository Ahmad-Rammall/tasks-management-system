using TaskManagementSystem.Models.DTOs.CommentDTOs;
using TasksManagementSystem.API.Entities;

namespace TasksManagementSystem.API.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllTaskComments(int taskId);
        Task<Comment> AddComment(CommentToAddDTO commentToAddDTO);
    }
}
