using Contracts.Domains;

namespace Chat.Api.Entities
{
    public class Message : EntityAuditBase<Guid>
    {
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Content { get; set; }
    }
}
