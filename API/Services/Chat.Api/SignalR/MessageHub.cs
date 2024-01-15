using AutoMapper;
using Chat.Api.Dtos;
using Chat.Api.Entities;
using Chat.Api.Repositories.Interfaces;
using Chat.Api.Services;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Api.SignalR
{
    public class MessageHub : Hub
    {
        private readonly IHubContext<PresenceHub> _presenceHub;
        private readonly PresenceTracker _presenceTracker;
        private readonly IIdentityService _identityService;
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public MessageHub(
            IHubContext<PresenceHub> presenceHub,
            PresenceTracker presenceTracker,
            IIdentityService identityService,
            IMessageRepository messageRepository,
            IMapper mapper)
        {
            _presenceHub = presenceHub;
            _presenceTracker = presenceTracker;
            _identityService = identityService;
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var otherUser = httpContext!.Request.Query["user"].ToString();
            var groupName = Helpers.HelpUliti.GetGroupName(_identityService.GetUserIdentity(), otherUser);

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await AddToGroup(groupName);

            //default get 24 messages
            var messagesThread = await _messageRepository.GetMessageThread(_identityService.GetUserIdentity(), otherUser);

            await Clients.Caller.SendAsync("ReceiveMessageThread", messagesThread.Messages);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var group = await RemoveFromMessageGroup();
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, group.Name);
            await base.OnDisconnectedAsync(exception);
        }

        private async Task<Group> AddToGroup(string groupName)
        {
            var group = await _messageRepository.GetMessageGroup(groupName);
            try
            {
                var connection = new Connection(Context.ConnectionId, _identityService.GetUserIdentity());
                if (group == null)
                {
                    group = new Group(groupName);
                    _messageRepository.AddGroup(group);
                }
                group.Connections.Add(connection);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return group;
        }

        public async Task SendMessage(DisplayMessageDto messageDto)
        {
            var senderUserName = _identityService.GetUserIdentity();
            if (senderUserName == messageDto.Author!.ToLower())
                throw new HubException("You cannot send message to yourself");

            var message = new Message
            {
                Sender = senderUserName,
                Receiver = messageDto.Author!.ToLower(),
                Content = messageDto.Content!
            };

            var groupName = Helpers.HelpUliti.GetGroupName(senderUserName, messageDto.Author!.ToLower());

            var connections = await _presenceTracker.GetConnectionsForUser(messageDto.Author!.ToLower());
            if (connections != null)
            {
                await _presenceHub.Clients.Clients(connections).SendAsync("NewMessageReceived", senderUserName);
            }

            _messageRepository.AddMessage(message);

            try
            {
                await Clients.Group(groupName).SendAsync("NewMessage", _mapper.Map<MessageDto>(message));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private async Task<Group> RemoveFromMessageGroup()
        {
            var group = await _messageRepository.GetGroupForConnection(Context.ConnectionId);
            try
            {
                var connection = group!.Connections.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
                group.Connections.Remove(connection!); 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return group;
        }
    }
}
