using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Models.DTOs.CommentDTOs;
using TasksManagementSystem.API.Helpers;
using TasksManagementSystem.API.Repositories.Interfaces;

namespace TasksManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepo = commentRepository;
        }
        [HttpGet]
        [Route("{taskId:int}")]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetAllTaskComments([FromRoute] int taskId)
        {
            try
            {
                var comments = await _commentRepo.GetAllTaskComments(taskId);
                if(comments == null)
                {
                    return NotFound();
                }

                return Ok(comments.ConvertToDto());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CommentDTO>> AddComment([FromBody] CommentToAddDTO commentToAddDTO)
        {
            try
            {
                var comment = await _commentRepo.AddComment(commentToAddDTO);
                if (comment == null)
                {
                    return NotFound();
                }

                return Ok(comment.ConvertToDto());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
