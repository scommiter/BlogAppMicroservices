using AutoMapper;
using Chat.Api.Dtos;

namespace Chat.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Entities.Message, MessageDto>().ReverseMap();
        }
    }
}
