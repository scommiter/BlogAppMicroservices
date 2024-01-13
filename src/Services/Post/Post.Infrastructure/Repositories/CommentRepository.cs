using Infrastructure;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Post.Application.Commons.Interfaces;
using Post.Domain.Entities;
using Post.Infrastructure.Persistence;

namespace Post.Infrastructure.Repositories
{
    public class CommentRepository : RepositoryBase<Comment, int, DataContext>, ICommentRepository
    {
        private readonly DataContext _dbContext;
        public CommentRepository(DataContext dbContext, IUnitOfWork<DataContext> unitOfWork) : base(dbContext, unitOfWork)
        {
            _dbContext = dbContext;
        }

        public async Task CreateComment(Comment comment) => await CreateAsync(comment);

        public async Task<IQueryable<Comment>> GetCommentByPostId(Guid id)
            => _dbContext.Comments
                    .Join(
                        _dbContext.TreePaths,
                        c => c.Id,
                        t => t.Ancestor,
                        (c, t) => new { Comment = c, TreePath = t }
                    )
                    .Where(joinResult => joinResult.TreePath.Ancestor == joinResult.TreePath.Descendant)
                    .Select(joinResult => new Comment
                    {
                        Id = joinResult.Comment.Id,
                        PostId = id,
                        Author = joinResult.Comment.Author,
                        Content = joinResult.Comment.Content,
                        CreatedDate = joinResult.Comment.CreatedDate,
                        LastModifiedDate = joinResult.Comment.LastModifiedDate
                    });

        public async Task<Comment> GetComment(int id) => await GetByIdAsync(id);
    }
}
