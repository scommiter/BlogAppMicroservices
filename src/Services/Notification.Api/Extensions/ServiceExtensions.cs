using Infrastructure.Interfaces;
using Infrastructure;
using Notification.Api.EventBusConsumer;
using Notification.Api.Repositories.Interfaces;
using Notification.Api.Repositories;
using Notification.Api.Services.Interfaces;
using Notification.Api.Services;
using Shared.Configurations;
namespace Notification.Api.Extensions
{
    public static class ServiceExtensions
    {
        internal static IServiceCollection AddConfigurationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseSettings = configuration.GetSection(nameof(DatabaseSettings))
                .Get<DatabaseSettings>();
            services.AddSingleton(databaseSettings);

            var eventBusSettings = configuration.GetSection(nameof(EventBusSettings))
                .Get<EventBusSettings>();
            services.AddSingleton(eventBusSettings);

            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<AddNotificationConsumer>();

            return services;
        }
    }
}
