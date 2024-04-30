using Red_Social_Proyecto.Dtos;
using Red_Social_Proyecto.Dtos.Task;

namespace Red_Social_Proyecto.Services.Interfaces
{
    public interface IFollowService
    {
        Task<ResponseDto<FollowDto>> FollowUserAsync(string followerId, string followedId);
        Task<ResponseDto<List<FollowDto>>> GetFollowersAsync(string userId);
        Task<ResponseDto<bool>> UnfollowUserAsync(string followerId, string followedId);
    }
}