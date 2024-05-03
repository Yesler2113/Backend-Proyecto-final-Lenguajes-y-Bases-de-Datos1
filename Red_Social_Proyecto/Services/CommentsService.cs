using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Red_Social_Proyecto.Database;
using Red_Social_Proyecto.Dtos;
using Red_Social_Proyecto.Dtos.Task;
using Red_Social_Proyecto.Dtos.ValidationsDto;
using Red_Social_Proyecto.Entities;
using Red_Social_Proyecto.Services.Interfaces;
using Red_Social_Proyecto.SignalRConnect;

namespace Red_Social_Proyecto.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly TodoListDBContext _context;
        private readonly IMapper _mapper;
        private readonly IHubContext<CommentsHub> _hubContext;
        private readonly HttpContext _httpContext;
        private readonly string _USER_ID;

        public CommentsService(TodoListDBContext context, IMapper mapper, IHubContext<CommentsHub> hubContext,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _hubContext = hubContext;
            _httpContext = httpContextAccessor.HttpContext!;
            var idClaim = _httpContext.User.Claims.
                Where(x => x.Type == "UserId"). //Revertir el token y obtener el id del usuario
                FirstOrDefault();
            _USER_ID = idClaim?.Value!;
        }

        public async Task<ResponseDto<CommentsDto>> CreateCommentAsync(CommentsCreateDto model)
        {
            var commentEntity = _mapper.Map<CommentsEntity>(model);

           
            commentEntity.CommentDate = DateTime.UtcNow;

            commentEntity.UserId = _USER_ID;

            _context.Comments.Add(commentEntity);
            await _context.SaveChangesAsync();

            var commentDto = _mapper.Map<CommentsDto>(commentEntity);

            //enviar la notificacion a todos los usuarios
            await _hubContext.Clients.All.SendAsync("ReceiveCommentNotification", $"Nuevo comentario creado por Usuario ID: " +
                $"{commentEntity.UserId}");

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
