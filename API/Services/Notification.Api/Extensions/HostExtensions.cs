using Chat.Api.Mapping;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Notification.Api.EventBusConsumer;
using Notification.Api.Persistence;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Shared.Configurations;

namespace Notification.Api.Extensions
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
            services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()));

            // MassTransit-RabbitMQ Configuration
            services.AddMassTransit(config =>
            {
                config.AddConsumer<AddNotificationConsumer>();
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(configuration["EventBusSettings:HostAddress"]);
                    //cfg.UseHealthCheck(ctx);
                    //cfg.ReceiveEndpoint(EventBusConstants.AddNotificationQueue, c => {
                    //    c.ConfigureConsumer<AddNotificationConsumer>(ctx);
                    //});
                    cfg.ConfigureEndpoints(ctx);
                });
            });

            return services;
        }


        private static IServiceCollection ConfigureDbCotext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection(nameof(DatabaseSettings))
                .Get<DatabaseSettings>()?.ConnectionString;
            var builder = new MySqlConnectionStringBuilder(connectionString);

            services.AddDbContext<DataContext>(m => m.UseMySql(builder.ConnectionString,
                ServerVersion.AutoDetect(builder.ConnectionString),
                e =>
                {
                    e.MigrationsAssembly("Notification.Api");
                    e.SchemaBehavior(MySqlSchemaBehavior.Ignore);
                }));
            return services;
        }
    }
}
