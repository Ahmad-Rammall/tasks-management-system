using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Models.DTOs.CommentDTOs;
using TasksManagementSystem.API.Data;
using TasksManagementSystem.API.Entities;
using TasksManagementSystem.API.Repositories.Interfaces;

namespace TasksManagementSystem.API.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;
        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }
        private async Task<bool> TaskExists(int taskId)
        {
            return await _context.Tasks.AnyAsync(t => t.Id == taskId);
        }
        private async Task<bool> UserExists(int userId)
        {
            return await _context.Users.AnyAsync(u => u.Id == userId);
        }
        public async Task<Comment> AddComment(CommentToAddDTO commentToAddDTO)
        {
            if (!await UserExists(commentToAddDTO.UserId) || !await TaskExists(commentToAddDTO.TaskId))
                return null;

            var comment = new Comment
            {
                Content = commentToAddDTO.Content,
                UserId = commentToAddDTO.UserId,
                TaskId = commentToAddDTO.TaskId
            };

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return comment;
        }

        public async Task<IEnumerable<Comment>> GetAllTaskComments(int taskId)
        {
            return await _context.Comments.Include(comment => comment.UserProp).Where(comment => comment.TaskId == taskId).ToListAsync();
        }
    }
}
