using Infrastructure.Interfaces;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Notification.Api.Persistence;
using Shared.Configurations;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

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
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.ConfigureDbCotext(configuration);
            services.AddInfrastructureServices();
            //services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()));

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

        private static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            return services.AddScoped(typeof(IRepositoryBase<,,>), typeof(RepositoryBase<,,>))
                .AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>))
                ;
        }

        public static IHost MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder) where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();

                try
                {
                    logger.LogInformation("Migrating mysql database");
                    ExecuteMigrations(context);
                    logger.LogInformation("Migrating mysql database");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occured while migrating the mysql database");
                }
            }
            return host;
        }

        private static void ExecuteMigrations<TContext>(TContext context) where TContext : DbContext
        {
            context.Database.Migrate();
        }
    }
}
