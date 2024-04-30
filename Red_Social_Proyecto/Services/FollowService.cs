using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Red_Social_Proyecto.Database;
using Red_Social_Proyecto.Dtos;
using Red_Social_Proyecto.Dtos.Task;
using Red_Social_Proyecto.Entities;
using Red_Social_Proyecto.Services.Interfaces;

namespace Red_Social_Proyecto.Services
{
    public class FollowService : IFollowService
    {
        private readonly TodoListDBContext _context;
        private readonly IMapper _mapper;

        public FollowService(TodoListDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseDto<FollowDto>> FollowUserAsync(string followerId, string followedId)
        {
            if (followerId == followedId)
            {
                return new ResponseDto<FollowDto>
                {
                    Status = false,
                    StatusCode = 400,
                    Message = "No puedes seguirte a ti mismo.",
                    Data = null
                };
            }

            var existingFollow = await _context.Follows
                .FirstOrDefaultAsync(f => f.FollowerId == followerId && f.FollowedId == followedId);
            if (existingFollow != null)
            {
                return new ResponseDto<FollowDto>
                {
                    Status = false,
                    StatusCode = 409,
                    Message = "Ya sigues a este usuario.",
                    Data = null
                };
            }

            var follow = new FollowEntity
            {
                Id = Guid.NewGuid(),
                FollowerId = followerId,
                FollowedId = followedId,
                FollowDate = DateTime.UtcNow
            };

            _context.Follows.Add(follow);
            await _context.SaveChangesAsync();

            return new ResponseDto<FollowDto>
            {
                Status = true,
                StatusCode = 201,
                Message = "Usuario seguido correctamente.",
                Data = _mapper.Map<FollowDto>(follow)
            };
        }

        public async Task<ResponseDto<bool>> UnfollowUserAsync(string followerId, string followedId)
        {
            var follow = await _context.Follows
                .FirstOrDefaultAsync(f => f.FollowerId == followerId && f.FollowedId == followedId);
            if (follow == null)
            {
                return new ResponseDto<bool>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "No sigues a este usuario.",
                    Data = false
                };
            }

            _context.Follows.Remove(follow);
            await _context.SaveChangesAsync();

            return new ResponseDto<bool>
            {
                Status = true,
                StatusCode = 200,
                Message = "Dejaste de seguir a este usuario.",
                Data = true
            };
        }

        public async Task<ResponseDto<List<FollowDto>>> GetFollowersAsync(string userId)
        {
            var followers = await _context.Follows
                .Where(f => f.FollowedId == userId)
                .ToListAsync();

            if (!followers.Any())
            {
                return new ResponseDto<List<FollowDto>>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "No hay seguidores para este usuario.",
                    Data = null
                };
            }

            return new ResponseDto<List<FollowDto>>
            {
                Status = true,
                StatusCode = 200,
                Message = "Seguidores recuperados correctamente.",
                Data = _mapper.Map<List<FollowDto>>(followers)
            };
        }


    }
}
