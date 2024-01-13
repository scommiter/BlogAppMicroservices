using AutoMapper;
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
        private readonly ICommentRepository _commentRepository;
        public PostsController(
            IMapper mapper,
            IPostRepository postRepository,
            ICommentRepository commentRepository)
        {
            _mapper = mapper;
            _postRepository = postRepository;
            _commentRepository = commentRepository;

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
