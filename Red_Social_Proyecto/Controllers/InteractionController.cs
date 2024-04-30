using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Red_Social_Proyecto.Dtos;
using Red_Social_Proyecto.Dtos.Task;
using Red_Social_Proyecto.Dtos.ValidationsDto;
using Red_Social_Proyecto.Services.Interfaces;

namespace Red_Social_Proyecto.Controllers
{
    [Route("api/interaction")]
    [ApiController]
    public class InteractionController : ControllerBase
    {
private readonly IInteractionService _interactionService;

        public InteractionController(IInteractionService interactionService)
        {
            _interactionService = interactionService;
        }

        [HttpPost("like")]
        
        public async Task<IActionResult> CreateLike([FromBody] InteractionCreateDto interactionDto)
        {
            interactionDto.TypeInteraction = "like";
            var response = await _interactionService.CreateLikeAsync(interactionDto);

            if (!response.Status)
            {
                return StatusCode(response.StatusCode, response);
            }

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("like/{userId}/{publicationId}")]
        public async Task<IActionResult> RemoveLike(string userId, Guid publicationId)
        {
            var result = await _interactionService.RemoveLikeAsync(userId, publicationId);
            if (!result.Status)
            {
                return StatusCode(result.StatusCode, result.Message);
            }
            return Ok(result);
        }

        [HttpGet("likes/publication/{publicationId}")]
        public async Task<IActionResult> GetLikesByPublication(Guid publicationId)
        {
            var likes = await _interactionService.GetLikesByPublicationAsync(publicationId);
            if (likes == null || !likes.Any())
            {
                return NotFound("No hay likes para esta publicación.");
            }
            return Ok(likes);
        }


    }
}
