using AutoMapper;
using Post.Domain.Dtos;
using Post.Domain.Entities;
using Shared.Dtos.Post;

namespace Post.Application.Mappings
{
    public class AutoMapperProfile : IMapFrom<Domain.Entities.Post>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Post, CreatePostDto>().ReverseMap();
            profile.CreateMap<Comment, CreateCommentDto>().ReverseMap();
            profile.CreateMap<Comment, DisplayCommentDto>().ReverseMap();
            profile.CreateMap<Domain.Entities.Post, PostDto>().ReverseMap();
            profile.CreateMap<TreePath, TreePathDto>().ReverseMap();
        }
    }
}
