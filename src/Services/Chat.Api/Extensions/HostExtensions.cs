﻿using Chat.Api.EventBusConsumer;
using Chat.Api.Mapping;
using Chat.Api.Persistence;
using Contracts.Common.Constants;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared.Configurations;

namespace Chat.Api.Extensions
{
    public static class HostExtensions
    {
        public static void AddAppConfigurations(this ConfigureHostBuilder host)
        {
            host.ConfigureAppConfiguration((context, config) =>
            {
                var env = context.HostingEnvironment;
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                      .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                      .AddEnvironmentVariables();
            });
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.ConfigureDbCotext(configuration);
            services.AddHttpContextAccessor();
            services.AddSignalR();
            services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()));

            // MassTransit-RabbitMQ Configuration
            services.AddMassTransit(config =>
            {
                config.AddConsumer<NotificationConsumer>();
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(configuration["EventBusSettings:HostAddress"]);
                    //cfg.UseHealthCheck(ctx);
                    cfg.ReceiveEndpoint(EventBusConstants.NotificationChatQueue, c => {
                        c.ConfigureConsumer<NotificationConsumer>(ctx);
                    });
                    //cfg.ConfigureEndpoints(ctx);
                });
            });

            return services;
        }


        private static IServiceCollection ConfigureDbCotext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection(nameof(DatabaseSettings))
                .Get<DatabaseSettings>()?.ConnectionString;
            services.AddDbContext<DataContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(connectionString!));
            });
            return services;
        }
    }
}
