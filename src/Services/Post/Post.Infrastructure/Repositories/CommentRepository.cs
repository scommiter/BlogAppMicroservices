using Infrastructure;
using Infrastructure.Interfaces;
using Post.Application.Commons.Interfaces;
using Post.Domain.Entities;
using Post.Infrastructure.Persistence;

namespace Post.Infrastructure.Repositories
{
    public class CommentRepository : RepositoryBase<Comment, int, DataContext>, ICommentRepository
    {
        public CommentRepository(DataContext dbContext, IUnitOfWork<DataContext> unitOfWork) : base(dbContext, unitOfWork)
        {
        }

        public async Task CreateComment(Comment comment)
        {
            await CreateAsync(comment);
        }
    }
}
