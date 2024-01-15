using Chat.Api.EventBusConsumer;
using Chat.Api.Repositories;
using Chat.Api.Repositories.Interfaces;
using Chat.Api.Services;
using Chat.Api.SignalR;
using Infrastructure;
using Infrastructure.Interfaces;
using Shared.Configurations;

namespace Chat.Api.Extensions
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
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddScoped<NotificationConsumer>();
            services.AddSingleton<PresenceTracker>();

            return services;
        }
    }
}
