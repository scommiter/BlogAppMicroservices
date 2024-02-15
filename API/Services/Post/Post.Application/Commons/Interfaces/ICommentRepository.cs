using Post.Domain.Dtos;
using Post.Domain.Entities;

namespace Post.Application.Commons.Interfaces
{
    public interface ICommentRepository
    {
        Task CreateComment(Comment comment);
        Task<IQueryable<Comment>> GetComment(int id);
        Task<IQueryable<Comment>> GetCommentByPostId(Guid id);
        Task DeleteComment(int id);
    }
}
