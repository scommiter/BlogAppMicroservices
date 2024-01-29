using Contracts.Common;
using Infrastructure;
using Infrastructure.Interfaces;
using Post.Application.Commons.Interfaces;
using Post.Domain.Dtos;
using Post.Infrastructure.Persistence;
using Shared.Dtos.Post;

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

        public async Task<PagedResult<PostDto>> GetAllPost(PagingRequest pagingRequest)
        {
            var posts = FindAll();
            var postPagings = posts.OrderByDescending(p => p.CreatedDate).Skip((pagingRequest.Page - 1) * pagingRequest.Limit)
                                .Take(pagingRequest.Limit).Select(p => new PostDto
                                {
                                    Title = p.Title,
                                    Content = p.Content,
                                    UserName = p.UserName
                                });
            var pageResult = new PagedResult<PostDto>()
            {
                Items = postPagings.ToList(),
                TotalRecords = posts.Count()
            };
            return pageResult;
        }

        public async Task<Domain.Entities.Post> GetPost(Guid id)
        {
           return await GetByIdAsync(id);
        }
    }
}
