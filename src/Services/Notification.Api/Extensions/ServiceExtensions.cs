using Infrastructure;
using Infrastructure.Interfaces;
using Shared.Configurations;
using System.Reflection;

namespace Notification.Api.Extensions
{
    public static class ServiceExtensions
    {
        internal static IServiceCollection AddConfigurationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseSettings = configuration.GetSection(nameof(DatabaseSettings))
                .Get<DatabaseSettings>();
            services.AddSingleton(databaseSettings);


            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));


            return services;
        }

        //public static IServiceCollection AddApplicationServices(this IServiceCollection services) =>
        //    services.AddAutoMapper(Assembly.GetExecutingAssembly())
        //            .AddAutoMapper(typeof(MappingProfile))
        //    ;
    }
}
