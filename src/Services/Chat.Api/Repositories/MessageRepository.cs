using AutoMapper;
using AutoMapper.QueryableExtensions;
using Chat.Api.Dto;
using Chat.Api.Dtos;
using Chat.Api.Entities;
using Chat.Api.Persistence;
using Chat.Api.Repositories.Interfaces;
using Contracts.Common;
using Microsoft.EntityFrameworkCore;

namespace Chat.Api.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MessageRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedResult<MessageDto>> GetMessageThread(MessageParams messageParams)
        {
            var messages = _context.Messages!
                .Where(m => m.Receiver == messageParams.CurrentUsername && m.Sender == messageParams.Receiver || m.Receiver == messageParams.Receiver && m.Sender == messageParams.CurrentUsername)
                .OrderByDescending(m => m.CreatedDate)
                .ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

            var totalItems = await messages.CountAsync();

            var items = await messages.Skip((messageParams.PageNumber - 1) * messageParams.PageSize).Take(messageParams.PageSize).ToListAsync();

            var result = new PagedResult<MessageDto>();
            result.TotalRecords = totalItems;
            result.Items = items;
            result.PageSize = messageParams.PageSize;
            return result;
        }

        public async Task<MessageThread> GetMessageThread(string currentUsername, string recipientUsername)
        {
            var messages = _context.Messages
                .Where(m => m.Receiver == currentUsername && m.Sender == recipientUsername || m.Receiver == recipientUsername && m.Sender == currentUsername)
                .OrderByDescending(m => m.CreatedDate)
                .ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

            var totalItems = await messages.CountAsync();

            var pageNumber = 1;
            var pageSize = 24;

            var items = await messages.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new MessageThread(totalItems, pageSize, items);
        }

        public async Task<Group?> GetMessageGroup(string groupName)
        {
            return await _context.Groups!.Include(x => x.Connections).FirstOrDefaultAsync(x => x.Name == groupName);
        }

        public async Task<Group?> GetGroupForConnection(string connectionId)
        {
            return await _context.Groups!.Include(x => x.Connections)
                .Where(x => x.Connections.Any(c => c.ConnectionId == connectionId))
                .FirstOrDefaultAsync();
        }

        public void AddGroup(Group group)
        {
            _context.Groups.Add(group);
        }

        public void AddMessage(Message mess)
        {
            _context.Messages.Add(mess);
        }
    }
}
