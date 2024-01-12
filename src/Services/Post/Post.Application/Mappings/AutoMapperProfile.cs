using AutoMapper;
using Post.Domain.Dtos;
using Post.Domain.Entities;

namespace Post.Application.Mappings
{
    public class AutoMapperProfile : IMapFrom<Domain.Entities.Post>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Post, CreatePostDto>().ReverseMap();
            profile.CreateMap<Comment, CreateCommentDto>().ReverseMap();
        }
    }
}
