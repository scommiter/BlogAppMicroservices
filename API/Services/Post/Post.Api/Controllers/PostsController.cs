using AutoMapper;
using Contracts.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Post.Api.Services;
using Post.Application.Commons.Interfaces;
using Post.Domain.Dtos;

namespace Post.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IIdentityService _identityService;
        public PostsController(
            IMapper mapper,
            IPostRepository postRepository,
            ICommentRepository commentRepository,
            IIdentityService identityService)
        {
            _mapper = mapper;
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _identityService = identityService;
        }

        [HttpPost]
        public async Task<ActionResult> Post(CreatePostDto createPostDto)
        {
            var userId = _identityService.GetUserIdentity();
            if (string.IsNullOrEmpty(userId)) return BadRequest("Username is Null");

            var post = _mapper.Map<Domain.Entities.Post>(createPostDto);
            post.Id = Guid.NewGuid();
            post.UserName = userId;
            await _postRepository.CreatePost(post);

            return Ok(post);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllPost([FromQuery] PagingRequest pagingRequest)
        {
            var userId = _identityService.GetUserIdentity();
            if (string.IsNullOrEmpty(userId)) return BadRequest("Username is Null");

            var result = _postRepository.GetAllPost(pagingRequest);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetComment(Guid id)
        {
            var post = _postRepository.GetPost(id);
            var comments = _commentRepository.GetCommentByPostId(post.Result.Id);
            var commentDto = comments.Result.ToList().Select(comment => _mapper.Map<DisplayCommentDto>(comment));
            return Ok(commentDto);
        }
    }
}
