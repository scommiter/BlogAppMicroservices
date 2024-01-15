using Contracts.Domains;

namespace Chat.Api.Dtos
{
    public class MessageDto : EntityAuditBase<Guid>
    {
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Content { get; set; }
    }
}
