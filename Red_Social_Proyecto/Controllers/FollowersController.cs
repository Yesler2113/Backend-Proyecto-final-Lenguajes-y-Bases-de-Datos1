using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Red_Social_Proyecto.Dtos.Task;
using Red_Social_Proyecto.Services.Interfaces;

namespace Red_Social_Proyecto.Controllers
{
    [Route("api/follows")]
    [ApiController]
    public class FollowersController : ControllerBase
    {
        private readonly IFollowService _followService;

        public FollowersController(IFollowService followService)
        {
            _followService = followService;
        }

        //[HttpPost("{followerId}/follow/{followedId}")]
        //public async Task<IActionResult> FollowUser([FromBody] FollowDto followDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var response = await _followService.FollowUserAsync(followDto.FollowerId, followDto.FollowedId);
        //    if (!response.Status)
        //    {
        //        return BadRequest(response.Message);
        //    }

        //    return CreatedAtAction(nameof(FollowUser), response.Data);
        //}
        [HttpPost("{followerId}/follow/{followedId}")]
        public async Task<IActionResult> FollowUser(string followerId, string followedId)
        {
            if (followerId == followedId)
            {
                return BadRequest("No puedes seguirte a ti mismo.");
            }

            var result = await _followService.FollowUserAsync(followerId, followedId);
            if (!result.Status)
            {
                return StatusCode(result.StatusCode, result.Message);
            }
            return Ok(result);
        }


        [HttpDelete("{followerId}/unfollow/{followedId}")]
        public async Task<IActionResult> UnfollowUser(string followerId, string followedId)
        {
            var result = await _followService.UnfollowUserAsync(followerId, followedId);
            if (!result.Status)
            {
                return StatusCode(result.StatusCode, result.Message);
            }
            return Ok(result.Message);
        }

        [HttpGet("{userId}/followers")]
        public async Task<IActionResult> GetFollowers(string userId)
        {
            var result = await _followService.GetFollowersAsync(userId);
            if (!result.Status)
            {
                return StatusCode(result.StatusCode, result.Message);
            }
            return Ok(result);
        }



    }
}
