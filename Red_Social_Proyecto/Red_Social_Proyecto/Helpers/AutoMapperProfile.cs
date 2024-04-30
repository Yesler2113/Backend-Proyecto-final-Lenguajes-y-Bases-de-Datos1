using AutoMapper;
using Red_Social_Proyecto.Dtos.Task;
using Red_Social_Proyecto.Dtos.ValidationsDto;
using Red_Social_Proyecto.Entities;

namespace Red_Social_Proyecto.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            MapsForTasks();
        }

        public void MapsForTasks()
        {
            CreateMap<UsersCreateDto, UsersEntity>();
            CreateMap<UsersEntity, UsersDto>();
            CreateMap<PublicationCreateDto, PublicationEntity>();
            CreateMap<PublicationEntity, PublicationDto>();
            CreateMap<CommentsCreateDto, CommentsEntity>();
            CreateMap<CommentsEntity, CommentsDto>();
            CreateMap<FollowEntity, FollowDto>();
            CreateMap<FollowCreateDto, FollowEntity>();
            CreateMap<InteractionEntity, InteractionDto>();
            CreateMap<InteractionCreateDto, InteractionEntity>();

        }
    }
}
