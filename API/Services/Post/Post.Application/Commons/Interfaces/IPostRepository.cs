using Contracts.Common;
using Post.Domain.Dtos;
using Shared.Dtos.Post;

namespace Post.Application.Commons.Interfaces
{
    public interface IPostRepository
    {
        Task CreatePost(Domain.Entities.Post post);
        Task<Domain.Entities.Post> GetPost(Guid id);
        Task<PagedResult<PostDto>> GetAllPost(PagingRequest pagingRequest);
    }
}
