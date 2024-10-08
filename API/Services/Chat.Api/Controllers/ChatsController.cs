﻿using Chat.Api.Dto;
using Chat.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly IMessageRepository _messageRepository;
        public ChatsController(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }                                                                                                                                                         

        [HttpGet("getMessages")]
        public async Task<IActionResult> GetMessages([FromQuery]MessageParams messageParams)
        {
            var data = await _messageRepository.GetMessageThread(messageParams);
            return Ok(data);
        }
    }
}
