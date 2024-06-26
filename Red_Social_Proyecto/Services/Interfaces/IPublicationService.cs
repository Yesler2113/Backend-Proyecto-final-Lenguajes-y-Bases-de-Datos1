﻿using Red_Social_Proyecto.Dtos;
using Red_Social_Proyecto.Dtos.Task;
using Red_Social_Proyecto.Dtos.ValidationsDto;

namespace Red_Social_Proyecto.Services.Interfaces
{
    public interface IPublicationService
    {
        
        Task<ResponseDto<PublicationDto>> CreatePublicationAsync(PublicationCreateDto model);
        Task<ResponseDto<bool>> DeletePublicationAsync(Guid publicationId);
        Task<ResponseDto<List<PublicationDto>>> GetListAsync(string searchTerm = "");
        Task<List<PublicationDto>> GetPublicationsByUserAsync(Guid userId);
        Task<List<PublicationDto>> GetPublicationsForUserAndFollowersAsync(string userId);
    }
}