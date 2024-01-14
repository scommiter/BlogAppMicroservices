using Chat.Api.Dto;
using Chat.Api.Dtos;
using Chat.Api.Entities;
using Contracts.Common;

namespace Chat.Api.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        Task<PagedResult<MessageDto>> GetMessageThread(MessageParams messageParams);
        Task<MessageThread> GetMessageThread(string currentUsername, string recipientUsername);
        Task<Group?> GetMessageGroup(string groupName);
        Task<Group?> GetGroupForConnection(string connectionId);
        void AddGroup(Group group);
        void AddMessage(Message mess);
    }
}
