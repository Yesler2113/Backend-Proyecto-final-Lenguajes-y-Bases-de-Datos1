using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Red_Social_Proyecto.Database;
using Red_Social_Proyecto.Dtos;
using Red_Social_Proyecto.Dtos.Task;
using Red_Social_Proyecto.Entities;
using Red_Social_Proyecto.Services.LogsService.Interface;

namespace Red_Social_Proyecto.Services.LogsService
{
    public class LogService : ILogService
    {
        private readonly LogDbContext _logContext;
        private readonly IMapper _mapper;
        private readonly HttpContext _httpContext;
        private readonly string _USER_ID;

        public LogService(LogDbContext logContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _logContext = logContext;
            _mapper = mapper;
            _httpContext = httpContextAccessor.HttpContext!;
            var idClaim = _httpContext.User.Claims
                .FirstOrDefault(c => c.Type == "UserId");
            _USER_ID = idClaim?.Value!;
        }

        public async Task<ResponseDto<UserLogDto>> LogActionAsync(string action)
        {
            var userLog = new LogsEntity
            {
                UserId = _USER_ID,
                Date = DateTime.UtcNow, 
                Action = action
            };

            _logContext.Logs.Add(userLog);
            await _logContext.SaveChangesAsync();

            var userLogDto = _mapper.Map<UserLogDto>(userLog);

            return new ResponseDto<UserLogDto>
            {
                Status = true,
                StatusCode = 201,
                Message = "Log registrado correctamente",
                Data = userLogDto
            };
        }

        public async Task<ResponseDto<UserLogDto>> LogActionAsync(string action, string userId)
        {
            var userLog = new LogsEntity
            {
                UserId = userId,
                Date = DateTime.UtcNow,
                Action = action
            };

            _logContext.Logs.Add(userLog);
            await _logContext.SaveChangesAsync();

            var userLogDto = _mapper.Map<UserLogDto>(userLog);

            return new ResponseDto<UserLogDto>
            {
                Status = true,
                StatusCode = 201,
                Message = "Log registrado correctamente",
                Data = userLogDto
            };
        }

        public async Task<List<UserLogDto>> GetUserLogsAsync(string userId)
        {
            var logs = await _logContext.Logs
                .Where(log => log.UserId == userId)
                .ToListAsync();

            return _mapper.Map<List<UserLogDto>>(logs);
        }

        //internal Task LogActionAsync(UserLogDto userLogDto)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
