namespace EventBus.Messages
{
    public class NotificationEvent : IntegrationBaseEvent
    {
        public string Sender { get; set; }
        public string Commentator { get; set; }
        public string Message { get; set; }
        public string PostId { get; set; }
    }
}
