using TaskManagementSystem.Models.DTOs.CommentDTOs;

namespace TasksManagementSystem.Web.Services.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDTO>> GetAllTaskComments(int taskId);
        Task<CommentDTO> AddComment(CommentToAddDTO commentToAddDTO);
    }
}
