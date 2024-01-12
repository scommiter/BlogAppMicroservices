using Post.Domain.Dtos;
using Post.Domain.Entities;

namespace Post.Application.Commons.Interfaces
{
    public interface ICommentRepository
    {
        Task CreateComment(Comment comment);
    }
}
