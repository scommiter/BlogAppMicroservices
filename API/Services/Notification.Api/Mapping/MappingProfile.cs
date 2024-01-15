using AutoMapper;
using Notification.Api.Dtos;

namespace Notification.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Entities.Notification, AddNotificationDto>().ReverseMap();
        }
    }
}
