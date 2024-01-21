using AutoMapper;
using EventBus.Messages;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Post.Api.Services;
using Post.Application.Commons.Interfaces;
using Post.Domain.Dtos;
using Post.Domain.Entities;

namespace Post.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICommentRepository _commentRepository;
        private readonly ITreePathRepository _treepathRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IIdentityService _identityService;
        public CommentsController(
            IMapper mapper,
            ICommentRepository commentRepository,
            ITreePathRepository treepathRepository,
            IPublishEndpoint publishEndpoint,
            IIdentityService identityService)
        {
            _mapper = mapper;
            _commentRepository = commentRepository;
            _treepathRepository = treepathRepository;
            _publishEndpoint = publishEndpoint;
            _identityService = identityService;
        }

        [HttpPost]
        public async Task<ActionResult> AddComment(CreateCommentDto createCommentDto)
        {

            var comment = _mapper.Map<Comment>(createCommentDto);
            await _commentRepository.CreateComment(comment);
            if (createCommentDto.AncestorId != null)
            {
                await _treepathRepository.CreateTreePathChild(createCommentDto.AncestorId ?? default(int), comment.Id);
            }
            else
            {
                await _treepathRepository.CreateTreePath(comment.Id);
            }

            await _publishEndpoint.Publish(new NotificationEvent
            {
                Sender = createCommentDto.Author,
                Commentator = "Fix cung",
                Message = $"comment into your post",
                PostId = createCommentDto.PostId.ToString()
            });

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetComment(int id)
        {
            var userId = _identityService.GetUserIdentity();
            if (string.IsNullOrEmpty(userId)) return BadRequest("Username is Null");

            var comments = _commentRepository.GetComment(id);
            var commentDto = comments.Result.ToList().Select(comment => _mapper.Map<DisplayCommentDto>(comment));
            return Ok(commentDto);
        }
    }
}
