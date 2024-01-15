namespace EventBus.Messages.Interfaces
{
    public interface IIntegrationBaseEvent
    {
        DateTime CreationDate { get; }

        Guid Id { get; set; }
    }
}
