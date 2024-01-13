namespace Notification.Api.Repositories.Interfaces
{
    public interface INotificationRepository
    {
        Task<Guid> AddNotification(Entities.Notification notification);
    }
}
