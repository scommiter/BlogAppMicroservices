using Infrastructure;
using Infrastructure.Interfaces;
using Post.Application.Commons.Interfaces;
using Post.Domain.Dtos;
using Post.Infrastructure.Persistence;

namespace Post.Infrastructure.Repositories
{
    public class PostRepository : RepositoryBase<Domain.Entities.Post, Guid, DataContext>, IPostRepository
    {
        public PostRepository(DataContext dbContext, IUnitOfWork<DataContext> unitOfWork) : base(dbContext, unitOfWork)
        {
        }

        public async Task CreatePost(Domain.Entities.Post post)
        {
            await CreateAsync(post);

        }

        public async Task<Domain.Entities.Post> GetPost(Guid id)
        {
           return await GetByIdAsync(id);
        }
    }
}
