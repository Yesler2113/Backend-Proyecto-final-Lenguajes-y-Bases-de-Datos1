using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Red_Social_Proyecto.Database;
using Red_Social_Proyecto.Dtos;
using Red_Social_Proyecto.Dtos.Task;
using Red_Social_Proyecto.Dtos.ValidationsDto;
using Red_Social_Proyecto.Entities;
using Red_Social_Proyecto.Services.Interfaces;

namespace Red_Social_Proyecto.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly TodoListDBContext _context;
        private readonly IMapper _mapper;

        public CommentsService(TodoListDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseDto<CommentsDto>> CreateCommentAsync(CommentsCreateDto model)
        {
            var commentEntity = _mapper.Map<CommentsEntity>(model);

            // Verificar si el CommentParentId es válido si está presente
            if (commentEntity.CommentParentId.HasValue)
            {
                var parentComment = await _context.Comments.FindAsync(commentEntity.CommentParentId.Value);
                if (parentComment == null)
                {
                    return new ResponseDto<CommentsDto>
                    {
                        Status = false,
                        StatusCode = 400,
                        Message = "El comentario padre especificado no existe.",
                        Data = null
                    };
                }
            }

            commentEntity.CommentDate = DateTime.UtcNow;
            _context.Comments.Add(commentEntity);
            await _context.SaveChangesAsync();

            var commentDto = _mapper.Map<CommentsDto>(commentEntity);

            return new ResponseDto<CommentsDto>
            {
                Status = true,
                StatusCode = 201,
                Message = "Comentario creado correctamente",
                Data = commentDto
            };
        }


        public async Task<List<CommentsDto>> GetCommentsByPublicationAsync(Guid publicationId)
        {
            var comments = await _context.Comments
                .Where(p => p.PublicationId == publicationId)
                .ToListAsync();

            return _mapper.Map<List<CommentsDto>>(comments);
        }

        public async Task<ResponseDto<bool>> DeleteCommentAsync(Guid commentId)
        {
            var comment = await _context.Comments.FindAsync(commentId);
            if (comment == null)
            {
                return new ResponseDto<bool>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Comentario no encontrado",
                    Data = false
                };
            }

            // Opcional: Verificar si el comentario tiene respuestas antes de eliminar
            var replies = await _context.Comments
                .AnyAsync(c => c.CommentParentId == commentId);
            if (replies)
            {
                return new ResponseDto<bool>
                {
                    Status = false,
                    StatusCode = 400,
                    Message = "No se puede eliminar un comentario que tiene respuestas",
                    Data = false
                };
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return new ResponseDto<bool>
            {
                Status = true,
                StatusCode = 200,
                Message = "Comentario eliminado correctamente",
                Data = true
            };
        }

    }
}
