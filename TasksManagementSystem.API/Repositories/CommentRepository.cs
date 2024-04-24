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
        public Task<Comment> AddComment(CommentToAddDTO commentToAddDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Comment>> GetAllTaskComments(int taskId)
        {
            return await _context.Comments.Where(comment => comment.TaskId == taskId).ToListAsync();
        }
    }
}
