using AutoMapper;
using Shared.Dtos.User;
using User.Grpc.Entities;

namespace User.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserEntry, UserDto>().ReverseMap();
            CreateMap<UserEntry, CreateUserDto>().ReverseMap();
        }
    }
}
