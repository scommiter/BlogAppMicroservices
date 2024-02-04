using Infrastructure;
using Infrastructure.Interfaces;
using Post.Application.Commons.Interfaces;
using Post.Domain.Dtos;
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

        public async Task<List<TreeCommentDto>> CreateTreeComments(List<DisplayCommentDto> Comments, List<TreePathDto> TreePaths)
        {
            var result = new List<TreeCommentDto>();

            return result;
        }

        private List<TreeCommentDto> BuildTree(List<DisplayCommentDto> Comments, List<TreePathDto> TreePaths, List<TreeCommentDto> treeResult)
        {


            return treeResult;
        }
    }
}
