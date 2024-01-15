using Shared.Configurations;

namespace User.Api.Extensions
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


            return services;
        }


        private static IServiceCollection ConfigureDbCotext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection(nameof(DatabaseSettings))
                .Get<DatabaseSettings>()?.ConnectionString;

            return services;
        }
    }
}
