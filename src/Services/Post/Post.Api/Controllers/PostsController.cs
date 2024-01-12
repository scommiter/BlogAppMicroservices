using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Post.Application.Commons.Interfaces;
using Post.Domain.Dtos;

namespace Post.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;
        public PostsController(IMapper mapper, IPostRepository postRepository)
        {
            _mapper = mapper;
            _postRepository = postRepository;
        }

        [HttpPost]
        public async Task<ActionResult> Post(CreatePostDto createPostDto)
        {
            //get username from identity
            //var userId = _identityService.GetUserIdentity();

            var post = _mapper.Map<Domain.Entities.Post>(createPostDto);
            post.Id = Guid.NewGuid();
            post.UserName = Guid.NewGuid().ToString();
            await _postRepository.CreatePost(post);

            return Ok(post);
        }
    }
}
