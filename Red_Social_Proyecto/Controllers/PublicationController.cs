using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Red_Social_Proyecto.Dtos;
using Red_Social_Proyecto.Dtos.Task;
using Red_Social_Proyecto.Dtos.ValidationsDto;
using Red_Social_Proyecto.Entities;
using Red_Social_Proyecto.Services;
using Red_Social_Proyecto.Services.Interfaces;

namespace Red_Social_Proyecto.Controllers
{
    [Route("api/publication")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class PublicationController : ControllerBase
    {
        private readonly IPublicationService _publicationService;

        public PublicationController(IPublicationService publicationService)
        {
            this._publicationService = publicationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePublication([FromBody] PublicationCreateDto publicationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _publicationService.CreatePublicationAsync(publicationDto);
            if (!response.Status)
            {
                return BadRequest(response.Message);
            }

            return CreatedAtAction(nameof(GetPublicationsByUser), new { userId = response.Data.UserId }, response.Data);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetPublicationsByUser(Guid userId)
        {
            var publications = await _publicationService.GetPublicationsByUserAsync(userId);
            if (publications == null || !publications.Any())
            {
                return NotFound("No se encontraron publicaciones para el usuario especificado.");
            }
            return Ok(publications);
        }

        [HttpDelete("{publicationId}")]
        public async Task<IActionResult> DeletePublication(Guid publicationId)
        {
            var result = await _publicationService.DeletePublicationAsync(publicationId);
            if (!result.Status)
            {
                return StatusCode(result.StatusCode, result.Message);
            }
            return Ok(result.Message);
        }


    }
}
