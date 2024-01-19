using MongoDB.Driver;
using Shared.Configurations;
using User.Api.GrpcService;
using User.Api.Persistence;
using User.Grpc.Protos;

namespace User.Api.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var settings = services.GetService<DatabaseSettings>();
            if (settings == null || string.IsNullOrEmpty(settings.ConnectionString))
                throw new ArgumentNullException("MongoDbSettings is not configured.");

            var mongoClient = services.GetRequiredService<IMongoClient>();
            new UserDbSeed()
                .SeedDataAsync(mongoClient, settings)
                .Wait();
            return host;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.ConfigureDbCotext(configuration);
            services.AddHttpContextAccessor();


            return services;
        }


        private static IServiceCollection ConfigureDbCotext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection(nameof(DatabaseSettings))
                .Get<DatabaseSettings>()?.ConnectionString;

            return services;
        }

        public static IServiceCollection AddGrpcClientConfigure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<UserGrpcService>();
            services.AddGrpcClient<UserProtoService.UserProtoServiceClient>
                (o => o.Address = new Uri(configuration["GrpcSettings:UserUrl"]));
            return services;
        }
    }
}
