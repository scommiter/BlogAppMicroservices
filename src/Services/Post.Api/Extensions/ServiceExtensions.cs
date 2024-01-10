using Microsoft.EntityFrameworkCore;
using Post.Infrastructure.Persistence;
using Shared.Configurations;

namespace Post.Api.Extensions
{
    public static class ServiceExtensions
    {
        internal static IServiceCollection AddConfigurationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseSettings = configuration.GetSection(nameof(DatabaseSettings))
                .Get<DatabaseSettings>();
            services.AddSingleton(databaseSettings);

            services.AddDbContext<DataContext>(options => options.UseSqlServer(
               databaseSettings.ConnectionString
                ));

            return services;
        }
    }
}
