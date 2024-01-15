using Contracts.Domains;

namespace Notification.Api.Entities
{
    public class Notification : EntityAuditBase<Guid>
    {
        public string Content { get; set; }
        public string Username { get; set; }
        public string UsernameComment { get; set; }
        public string? PostId { get; set; }
        public string? CommentId { get; set; }
    }
}
