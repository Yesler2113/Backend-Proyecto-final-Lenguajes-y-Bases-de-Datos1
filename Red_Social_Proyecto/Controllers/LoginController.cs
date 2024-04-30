using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Red_Social_Proyecto.Dtos;
using Red_Social_Proyecto.Dtos.Security;
using Red_Social_Proyecto.Dtos.Task;
using Red_Social_Proyecto.Dtos.ValidationsDto;
using Red_Social_Proyecto.Services.Interfaces;

namespace Red_Social_Proyecto.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IAuthService _authService;

        public LoginController(IUsersService usersService, IAuthService authService)
        {
            this._usersService = usersService;
            this._authService = authService;
        }

        [HttpPost]

        public async Task<ActionResult<ResponseDto<UsersDto>>> Create([FromBody] UsersCreateDto model)
        {
            
            var response = await _usersService.CreateUserAsync(model);

            if (!response.Status)
            {
                return BadRequest(response);  
            }

            return Ok(response);
        }

        [HttpPost("init")]
        public async Task<ActionResult<ResponseDto<LoginResponseDto>>> Login([FromBody] LoginDto dto)
        {
            var response = await _authService.LoginAsync(dto);

            if (!response.Status)
            {
                return BadRequest(response);  
            }

            return Ok(response);
        }

        [HttpPost("regresh-token")]
        [Authorize(AuthenticationSchemes = "Bearer")]

        public async Task<ActionResult<ResponseDto<LoginResponseDto>>> RefreshToken()
        {
            var authresponse = await _authService.RefreshTokenAsync();

            return StatusCode(authresponse.StatusCode, authresponse);
        }


    }
}
