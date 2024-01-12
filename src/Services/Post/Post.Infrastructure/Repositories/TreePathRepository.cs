using Infrastructure;
using Infrastructure.Interfaces;
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
        public async Task CreateTreePath(int ancestor, int descendant)
        {
            await CreateListAsync(new List<TreePath>() { new TreePath(descendant, descendant), new TreePath(ancestor, descendant) });
        }
    }
}
