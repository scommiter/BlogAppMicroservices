using Contracts.Common;
using Infrastructure;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Post.Application.Commons.Interfaces;
using Post.Domain.Dtos;
using Post.Infrastructure.Persistence;
using Shared.Dtos.Post;

namespace Post.Infrastructure.Repositories
{
    public class PostRepository : RepositoryBase<Domain.Entities.Post, Guid, DataContext>, IPostRepository
    {
        private DataContext _dataContext;
        public PostRepository(DataContext dbContext, IUnitOfWork<DataContext> unitOfWork) : base(dbContext, unitOfWork)
        {
            _dataContext = dbContext;
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
                                    Id = p.Id,
                                    Title = p.Title,
                                    Content = p.Content,
                                    UserName = p.UserName,
                                    CreateDate = p.CreatedDate,
                                    UpdateDate = p.LastModifiedDate
                                });
            var totalCount = _dataContext.Posts.Count();
            var pageResult = new PagedResult<PostDto>()
            {
                Items = postPagings.ToList(),
                TotalRecords = totalCount,
                PageSize = pagingRequest.Limit,
                PageIndex = pagingRequest.Page
            };
            return pageResult;
        }

        public async Task<Domain.Entities.Post> GetPost(Guid id)
        {
           return await GetByIdAsync(id);
        }
    }
}
