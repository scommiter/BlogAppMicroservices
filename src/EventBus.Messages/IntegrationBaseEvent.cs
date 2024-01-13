using EventBus.Messages.Interfaces;

namespace EventBus.Messages
{
    public class IntegrationBaseEvent : IIntegrationBaseEvent
    {
        public DateTime CreationDate { get; } = DateTime.Now;

        public Guid Id { get; set; }
    }
}
