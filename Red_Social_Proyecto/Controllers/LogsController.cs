using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Red_Social_Proyecto.Database;
using Red_Social_Proyecto.Dtos;
using Red_Social_Proyecto.Dtos.Task;
using Red_Social_Proyecto.Services.LogsService.Interface;
using System.Security.Claims;

namespace Red_Social_Proyecto.Controllers
{
    [Route("api/logs")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly LogDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogService _logService;

        public LogsController(LogDbContext context, IMapper mapper, ILogService logService)
        {
            _context = context;
            _mapper = mapper;
            _logService = logService;
        }

        [HttpGet("{userId}")]
       public async Task<ActionResult> GetLogsByUser(string userId)
        {
            var logs = await _logService.GetUserLogsAsync(userId);

            if (logs == null)
            {
                return NotFound("no hay registros");
            }

            return Ok(logs);
        }
    }
}
