using Red_Social_Proyecto.Dtos;
using Red_Social_Proyecto.Dtos.Task;

namespace Red_Social_Proyecto.Services.LogsService.Interface
{
    public interface ILogService
    {
        Task<List<UserLogDto>> GetUserLogsAsync(string userId);
        Task<ResponseDto<UserLogDto>> LogActionAsync(string action);
        Task<ResponseDto<UserLogDto>> LogActionAsync(string action, string userId);
    }
}