using Red_Social_Proyecto.Dtos;
using Red_Social_Proyecto.Dtos.Security;

namespace Red_Social_Proyecto.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseDto<LoginResponseDto>> LoginAsync(LoginDto dto);
        Task<ResponseDto<LoginResponseDto>> RefreshTokenAsync();
    }
}