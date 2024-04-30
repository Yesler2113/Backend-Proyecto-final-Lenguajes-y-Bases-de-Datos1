using Red_Social_Proyecto.Dtos;
using Red_Social_Proyecto.Dtos.Task;
using Red_Social_Proyecto.Dtos.ValidationsDto;

namespace Red_Social_Proyecto.Services.Interfaces
{
    public interface IInteractionService
    {
        Task<ResponseDto<InteractionDto>> CreateLikeAsync(InteractionCreateDto interactionDto);
        Task<List<InteractionDto>> GetLikesByPublicationAsync(Guid publicationId);
        Task<ResponseDto<bool>> RemoveLikeAsync(string userId, Guid publicationId);
    }
}