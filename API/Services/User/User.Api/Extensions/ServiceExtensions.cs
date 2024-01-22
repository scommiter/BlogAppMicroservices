using Infrastructure;
using Infrastructure.Interfaces;
using MongoDB.Driver;
using Shared.Configurations;
using User.Api.Mapping;
namespace User.Api.Extensions
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

        public static void AddInfrastructureService(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()));
        }

        public static void ConfigureMongoDbClientUserAPI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMongoClient>(
                new MongoClient(services.GetMongoConnectionString(configuration)))
                .AddScoped(x => x.GetService<IMongoClient>()?.StartSession());
        }

        private static string GetMongoConnectionString(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseSettings = configuration.GetSection(nameof(DatabaseSettings))
                    .Get<DatabaseSettings>();
            if (databaseSettings == null || string.IsNullOrEmpty(databaseSettings.ConnectionString))
                throw new ArgumentNullException("MongoDbSettings is not configured.");

            var databaseName = databaseSettings.DatabaseName;
            var mongoDbConnectionString = databaseSettings.ConnectionString + "/" + databaseName + "?authSource=admin";
            return mongoDbConnectionString;
        }
    }
}
