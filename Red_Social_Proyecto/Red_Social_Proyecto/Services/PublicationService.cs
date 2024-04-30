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
    public class PublicationService : IPublicationService
    {
        private readonly TodoListDBContext _context;
        private readonly IMapper _mapper;
        private readonly IHubContext<PublicationHub> _hubcontext;
        private readonly HttpContext _httpContext;
        private readonly string _USER_ID;

        public PublicationService(TodoListDBContext context, IMapper mapper, 
            IHttpContextAccessor httpContextAccessor, IHubContext<PublicationHub> hubcontext)
        {
            _context = context;
            _mapper = mapper;
            _hubcontext = hubcontext;
            _httpContext = httpContextAccessor.HttpContext!; //para mandar el id del usuario que a creado la tarea
            var idClaim = _httpContext.User.Claims.
                Where(x => x.Type == "UserId"). //Revertir el token y obtener el id del usuario
                FirstOrDefault();
            _USER_ID = idClaim?.Value!;
        }

        public async Task<ResponseDto<PublicationDto>> CreatePublicationAsync(PublicationCreateDto model)
        {
            var publicationEntity = _mapper.Map<PublicationEntity>(model);

            publicationEntity.PublicationDate = DateTime.UtcNow; 

            publicationEntity.UserId = _USER_ID;

            _context.Publications.Add(publicationEntity);
            await _context.SaveChangesAsync();

            var publicationDto = _mapper.Map<PublicationDto>(publicationEntity);

            await _hubcontext.Clients.All.SendAsync("ReceivePublication", $"Nueva publicación de {publicationDto.UserId}");

            return new ResponseDto<PublicationDto>
            {
                Status = true,
                StatusCode = 201,
                Message = "Publicación creada correctamente",
                Data = publicationDto
            };
        }

        public async Task<List<PublicationDto>> GetPublicationsByUserAsync(Guid userId)
        {
            var publications = await _context.Publications
                .Where(p => p.UserId == userId.ToString())
                .ToListAsync();

            return _mapper.Map<List<PublicationDto>>(publications);
        }

        public async Task<ResponseDto<List<PublicationDto>>> GetListAsync(string searchTerm = "")
        {
            var tasksEntity = await _context.Publications
                .Where(t => t.Content.Contains(searchTerm) &&
                    t.UserId == _USER_ID).ToListAsync();

            var tasksDto = _mapper.Map<List<PublicationDto>>(tasksEntity);

            return new ResponseDto<List<PublicationDto>>
            {
                Status = true,
                StatusCode = 200,
                Message = "Datos obtenidos correctamente",
                Data = tasksDto
            };
        }

        public async Task<ResponseDto<bool>> DeletePublicationAsync(Guid publicationId)
        {

            var publication = await _context.Publications.FindAsync(publicationId);
            publication.UserId = _USER_ID;
            if (publication == null)
            {
                return new ResponseDto<bool>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Publicación no encontrada",
                    Data = false
                };
            }

            _context.Publications.Remove(publication);
            await _context.SaveChangesAsync();

            return new ResponseDto<bool>
            {
                Status = true,
                StatusCode = 200,
                Message = "Publicación eliminada correctamente",
                Data = true
            };
        }

    }
}
