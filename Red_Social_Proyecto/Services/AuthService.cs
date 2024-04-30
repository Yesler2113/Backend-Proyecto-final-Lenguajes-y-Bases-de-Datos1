using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Red_Social_Proyecto.Dtos.Security;
using Red_Social_Proyecto.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Red_Social_Proyecto.Services.Interfaces;
using Red_Social_Proyecto.Entities;

namespace todo_list_backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<UsersEntity> _signInManager;
        private readonly UserManager<UsersEntity> _userManager;
        private readonly IConfiguration _configuration;
        private readonly HttpContext _httpContext;
        private readonly string _USER_ID;

        public AuthService(
            SignInManager<UsersEntity> signInManager,
            UserManager<UsersEntity> userManager,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _httpContext = httpContextAccessor.HttpContext!;
            var idClaim = _httpContext.User.Claims.
                Where(x => x.Type == "UserId").
                FirstOrDefault();
            _USER_ID = idClaim?.Value!;
        }

        public async Task<ResponseDto<LoginResponseDto>> LoginAsync(LoginDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync(
                dto.Email,
                dto.Password,
                isPersistent: false,
                lockoutOnFailure: false
            );

            if (result.Succeeded)
            {
                var userEntity = await _userManager.FindByEmailAsync(dto.Email);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, userEntity.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserId", userEntity.Id),
                };

                var userRoles = await _userManager.GetRolesAsync(userEntity);

                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var jwtToken = GetToken(authClaims);

                return new ResponseDto<LoginResponseDto>
                {
                    StatusCode = 200,
                    Status = true,
                    Message = "Login exitoso",
                    Data = new LoginResponseDto
                    {
                        Email = userEntity.Email,
                        Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                        TokenExpiration = jwtToken.ValidTo,
                    }
                };
            }

            return new ResponseDto<LoginResponseDto>
            {
                StatusCode = 400,
                Status = false,
                Message = "Fallo el inicio de sesión",
                Data = null 
            };
        }

        public async Task<ResponseDto<LoginResponseDto>> RefreshTokenAsync()
        {
            var userEntity = await _userManager.FindByIdAsync(_USER_ID);
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, userEntity.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserId", userEntity.Id),
                };

            var userRoles = await _userManager.GetRolesAsync(userEntity);

            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var jwtToken = GetToken(authClaims);

            return new ResponseDto<LoginResponseDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "token renobado exitosamente",
                Data = new LoginResponseDto
                {
                    Email = userEntity.Email,
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    TokenExpiration = jwtToken.ValidTo,
                }
            };


        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(
                            authSigningKey, SecurityAlgorithms.HmacSha256));

            return token;

        }
    }
}
