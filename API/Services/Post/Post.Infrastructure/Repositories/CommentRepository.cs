using Infrastructure;
using Infrastructure.Interfaces;
using Post.Application.Commons.Interfaces;
using Post.Domain.Entities;
using Post.Infrastructure.Persistence;
using System.ComponentModel.Design;

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
        {
            return _dbContext.Comments
                    .Join(
                        _dbContext.TreePaths,
                        c => c.Id,
                        t => t.Ancestor,
                        (c, t) => new { Comment = c, TreePath = t }
                    )
                    .Where(joinResult => joinResult.TreePath.Ancestor == joinResult.TreePath.Descendant && joinResult.Comment.PostId == id)
                    .Select(joinResult => new Comment
                    {
                        Id = joinResult.Comment.Id,
                        PostId = id,
                        Author = joinResult.Comment.Author,
                        Content = joinResult.Comment.Content,
                        CreatedDate = joinResult.Comment.CreatedDate,
                        LastModifiedDate = joinResult.Comment.LastModifiedDate
                    }).OrderByDescending(e => e.CreatedDate);
        }

        public async Task<IQueryable<Comment>> GetComment(int id)
        => _dbContext.Comments
                    .Join(
                        _dbContext.TreePaths,
                        c => c.Id,
                        t => t.Descendant,
                        (c, t) => new { Comment = c, TreePath = t }
                    )
                    .Where(joinResult => joinResult.TreePath.Ancestor == id)
                    .Select(joinResult => new Comment
                    {
                        Id = joinResult.Comment.Id,
                        Author = joinResult.Comment.Author,
                        Content = joinResult.Comment.Content,
                        CreatedDate = joinResult.Comment.CreatedDate,
                        LastModifiedDate = joinResult.Comment.LastModifiedDate
                    });

        public async Task DeleteComment(int id)
        {
            var commentIdsToDelete = GetCommentIdsToDelete(id);
            var commentDeletes = FindAll().Where(e => commentIdsToDelete.Contains(e.Id)).ToList();
            await DeleteListAsync(commentDeletes);
        }

        private IEnumerable<int> GetCommentIdsToDelete(int commentId)
        {
            var commentIds = new List<int> { commentId };
            var descendantIds = _dbContext.TreePaths
                .Where(e => e.Ancestor == commentId && e.Ancestor != e.Descendant)
                .Select(e => e.Descendant)
                .ToList();

            foreach (var descendantId in descendantIds)
            {
                commentIds.AddRange(GetCommentIdsToDelete(descendantId));
            }

            return commentIds;
        }
    }
}
