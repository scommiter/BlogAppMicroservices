namespace EventBus.Messages
{
    public class NotificationEvent : IntegrationBaseEvent
    {
        public string UserNameSentMessage { get; set; }
        public string UserNameComment { get; set; }
        public string Message { get; set; }
        public string PostId { get; set; }
    }
}
