using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Red_Social_Proyecto.Database;
using Red_Social_Proyecto.Dtos.Task;
using Red_Social_Proyecto.Dtos.ValidationsDto;
using Red_Social_Proyecto.Dtos;
using Red_Social_Proyecto.Entities;
using Red_Social_Proyecto.Services.Interfaces;

namespace Red_Social_Proyecto.Services
{
    public class InteractionService : IInteractionService
    {
        private readonly TodoListDBContext _context;
        private readonly IMapper _mapper;

        public InteractionService(TodoListDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseDto<InteractionDto>> CreateLikeAsync(InteractionCreateDto interactionDto)
        {
            var existingInteraction = await _context.Interactions
                .FirstOrDefaultAsync(i => i.UserId == interactionDto.UserId
                    && i.PublicationId == interactionDto.PublicationId
                    && i.TypeInteraction == interactionDto.TypeInteraction);

            if (existingInteraction != null)
            {
                return new ResponseDto<InteractionDto>
                {
                    Status = false,
                    StatusCode = 409,
                    Message = "Ya has dado 'like' a esta publicación.",
                    Data = null
                };
            }

            var interaction = new InteractionEntity
            {
                Id = Guid.NewGuid(),
                UserId = interactionDto.UserId,
                PublicationId = interactionDto.PublicationId,
                TypeInteraction = interactionDto.TypeInteraction,
                InteractionDate = DateTime.UtcNow
            };

            _context.Interactions.Add(interaction);
            await _context.SaveChangesAsync();

            return new ResponseDto<InteractionDto>
            {
                Status = true,
                StatusCode = 201,
                Message = "Like añadido correctamente a la publicación.",
                Data = _mapper.Map<InteractionDto>(interaction)
            };
        }

        public async Task<ResponseDto<bool>> RemoveLikeAsync(string userId, Guid publicationId)
        {
            var interaction = await _context.Interactions
                .FirstOrDefaultAsync(i => i.UserId == userId && i.PublicationId == publicationId && i.TypeInteraction == "like");

            if (interaction == null)
            {
                return new ResponseDto<bool>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Like no encontrado o ya fue removido.",
                    Data = false
                };
            }

            _context.Interactions.Remove(interaction);
            await _context.SaveChangesAsync();

            return new ResponseDto<bool>
            {
                Status = true,
                StatusCode = 200,
                Message = "Like removido exitosamente.",
                Data = true
            };
        }

        public async Task<List<InteractionDto>> GetLikesByPublicationAsync(Guid publicationId)
        {
            var likes = await _context.Interactions
                .Where(i => i.PublicationId == publicationId && i.TypeInteraction == "like" && i.CommentId == Guid.Empty)
                .ToListAsync();

            return _mapper.Map<List<InteractionDto>>(likes);
        }
    }
}
