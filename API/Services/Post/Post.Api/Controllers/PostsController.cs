using AutoMapper;
using Contracts.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Post.Api.Services;
using Post.Application.Commons.Interfaces;
using Post.Domain.Dtos;
using Shared.Dtos.Post;

namespace Post.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ITreePathRepository _treePathRepository;
        private readonly IIdentityService _identityService;
        public PostsController(
            IMapper mapper,
            IPostRepository postRepository,
            ICommentRepository commentRepository,
            ITreePathRepository treePathRepository,
            IIdentityService identityService)
        {
            _mapper = mapper;
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _identityService = identityService;
            _treePathRepository = treePathRepository;   
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

            return Ok(result.Result);
        }

        [HttpGet("comment-post/{id}")]
        public async Task<ActionResult> GetComment(Guid id)
        {
            var result = new DisplayPostDto();

            var post = _postRepository.GetPost(id);
            var comments = _commentRepository.GetCommentByPostId(post.Result.Id);
            var commentDto = comments.Result.ToList().Select(comment => _mapper.Map<DisplayCommentDto>(comment));
            var commentIds = commentDto.Select(c => c.Id).ToList();
            var treePath = _treePathRepository.GetAllTreePath(commentIds).Result;

            result.Comments = commentDto.ToList();
            result.TreePaths = treePath.Select(e => _mapper.Map<TreePathDto>(e)).ToList();
            return Ok(result);
        }

        [HttpGet("detail-post/{id}")]
        public async Task<ActionResult> GetPostById(Guid id)
        {
            var post = _postRepository.GetPost(id).Result;
            var result = new PostDto
            {
                Id = post.Id,
                Content = post.Content,
                CreateDate = post.CreatedDate,
                Title = post.Title,
                UpdateDate = post.LastModifiedDate,
                UserName = post.UserName
            };
            return Ok(result);
        }
    }
}
