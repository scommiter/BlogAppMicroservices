using Infrastructure;
using Infrastructure.Interfaces;
using Notification.Api.Persistence;
using Notification.Api.Repositories.Interfaces;

namespace Notification.Api.Repositories
{
    public class NotificationRepository : RepositoryBase<Entities.Notification, Guid, DataContext>, INotificationRepository
    {
        public NotificationRepository(DataContext dbContext, IUnitOfWork<DataContext> unitOfWork) : base(dbContext, unitOfWork)
        {
        }

        public async Task<Guid> AddNotification(Entities.Notification notification) => await CreateAsync(notification);
    }
}
