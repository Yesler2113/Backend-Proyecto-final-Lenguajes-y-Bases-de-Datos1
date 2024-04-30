
using Red_Social_Proyecto.Dtos;
using Red_Social_Proyecto.Dtos.Security;
using Red_Social_Proyecto.Dtos.Task;
using Red_Social_Proyecto.Dtos.ValidationsDto;

namespace Red_Social_Proyecto.Services.Interfaces
{
    public interface IUsersService
    {
        Task<ResponseDto<UsersDto>> CreateUserAsync(UsersCreateDto model);
        
    }
}