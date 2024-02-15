using Infrastructure;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Post.Application.Commons.Interfaces;
using Post.Domain.Entities;
using Post.Infrastructure.Persistence;

namespace Post.Infrastructure.Repositories
{
    public class TreePathRepository : RepositoryMultiKeyBaseAsync<TreePath, DataContext>, ITreePathRepository
    {
        private readonly DataContext _dbContext;
        public TreePathRepository(DataContext dbContext, IUnitOfWork<DataContext> unitOfWork) : base(dbContext, unitOfWork)
        {
            _dbContext = dbContext; 
        }

        public async Task CreateTreePath(int commentId) => await CreateAsync(new TreePath(commentId, commentId));

        public async Task CreateTreePathChild(int ancestor, int descendant) => await CreateListAsync(new List<TreePath>() { new TreePath(descendant, descendant), new TreePath(ancestor, descendant) });

        public async Task DeleteAllChildTreePathComment(int commentId)
        {
            var commentIdsToDelete = GetCommentIdsToDelete(commentId);
            var treePathsDeletes = FindAll().Where(e => commentIdsToDelete.Contains(e.Ancestor) || commentIdsToDelete.Contains(e.Descendant)).ToList();
            await DeleteListAsync(treePathsDeletes);
        }

        public async Task GetAllChildTreePathComment(int commentId) => await FindByCondition(x => x.Ancestor == commentId).ToListAsync();

        public async Task<List<TreePath>> GetAllTreePath(List<int> commentIds)
        {
            var treePaths = FindAll();
            var treePathsMapPost = treePaths.Where(e => commentIds.Contains(e.Ancestor));
            return treePathsMapPost.ToList();
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
