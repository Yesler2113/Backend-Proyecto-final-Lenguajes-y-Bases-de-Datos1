using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Red_Social_Proyecto.Dtos.ValidationsDto;
using Red_Social_Proyecto.Services;
using Red_Social_Proyecto.Services.Interfaces;

namespace Red_Social_Proyecto.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService _commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            _commentsService = commentsService;
        }

        [HttpPost]

        public async Task<IActionResult> CreateComment([FromBody] CommentsCreateDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _commentsService.CreateCommentAsync(commentDto);
            if (!response.Status)
            {
                return BadRequest(response.Message);
            }

            return CreatedAtAction(nameof(GetCommentsByPublication), new { publicationId = response.Data.PublicationId }, response.Data);
        }

        [HttpGet("publication/{publicationId}")]
        public async Task<IActionResult> GetCommentsByPublication(Guid publicationId)
        {
            var comments = await _commentsService.GetCommentsByPublicationAsync(publicationId);
            if (comments == null || !comments.Any())
            {
                return NotFound("No se encontraron comentarios para la publicación especificada.");
            }
            return Ok(comments);
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteComment(Guid commentId)
        {
            var result = await _commentsService.DeleteCommentAsync(commentId);
            if (!result.Status)
            {
                return StatusCode(result.StatusCode, result.Message);
            }
            return Ok(result.Message);
        }
    }
}
