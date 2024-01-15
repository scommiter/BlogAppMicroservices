using Post.Domain.Dtos;

namespace Post.Application.Commons.Interfaces
{
    public interface IPostRepository
    {
        Task CreatePost(Domain.Entities.Post post);
        Task<Domain.Entities.Post> GetPost(Guid id);
    }
}
