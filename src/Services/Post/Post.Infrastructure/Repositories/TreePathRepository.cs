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
        public TreePathRepository(DataContext dbContext, IUnitOfWork<DataContext> unitOfWork) : base(dbContext, unitOfWork)
        {
        }

        public async Task CreateTreePath(int commentId) => await CreateAsync(new TreePath(commentId, commentId));

        public async Task CreateTreePathChild(int ancestor, int descendant) => await CreateListAsync(new List<TreePath>() { new TreePath(descendant, descendant), new TreePath(ancestor, descendant) });

        public async Task DeleteAllChildTreePathComment(int commentId)
        {
            var listTreePaths = await FindByCondition(x => x.Ancestor == commentId).ToListAsync();
            await DeleteListAsync(listTreePaths);
        }

        public async Task GetAllChildTreePathComment(int commentId) => await FindByCondition(x => x.Ancestor == commentId).ToListAsync();
    }
}
