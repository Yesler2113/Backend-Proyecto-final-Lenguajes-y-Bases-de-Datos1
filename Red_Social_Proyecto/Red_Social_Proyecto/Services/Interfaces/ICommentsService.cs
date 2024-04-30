using Red_Social_Proyecto.Dtos;
using Red_Social_Proyecto.Dtos.Task;
using Red_Social_Proyecto.Dtos.ValidationsDto;

namespace Red_Social_Proyecto.Services.Interfaces
{
    public interface ICommentsService
    {
        Task<ResponseDto<CommentsDto>> CreateCommentAsync(CommentsCreateDto model);
        Task<ResponseDto<bool>> DeleteCommentAsync(Guid commentId);
        Task<List<CommentsDto>> GetCommentsByPublicationAsync(Guid publicationId);
    }
}