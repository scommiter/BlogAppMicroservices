using AutoMapper;
using EventBus.Messages;
using MassTransit;
using Notification.Api.Dtos;
using Notification.Api.Repositories.Interfaces;

namespace Notification.Api.EventBusConsumer
{
    public class AddNotificationConsumer : IConsumer<NotificationEvent>
    {
        private readonly ILogger<AddNotificationConsumer> _logger;
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;

        public AddNotificationConsumer(
            ILogger<AddNotificationConsumer> logger,
            INotificationRepository notificationRepository,
            IMapper mapper)
        {
            _logger = logger;
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<NotificationEvent> context)
        {
            var addNotification = new AddNotificationDto
            {
                Username = context.Message.Sender,
                UsernameComment = context.Message.Commentator,
                Content = context.Message.Message,
                PostId = context.Message.PostId
            };
            var result = await _notificationRepository.AddNotification(_mapper.Map<Entities.Notification>(addNotification));
            _logger.LogInformation("Add NotificationEvent Consume success. Id : {Id}", result);
        }
    }
}
