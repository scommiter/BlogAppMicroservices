using Post.Api.GrpcService;
using User.Grpc.Protos;

namespace Post.Api.Extensions
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

        public static IServiceCollection AddGrpcClientConfigure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<UserGrpcService>();
            services.AddGrpcClient<UserProtoService.UserProtoServiceClient>
                (o => o.Address = new Uri(configuration["GrpcSettings:UserUrl"]));
            return services;
        }
    }
}
