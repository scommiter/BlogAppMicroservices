using IdentityServer.GrpcService;
using Serilog;
using User.Grpc.Protos;

namespace IdentityServer.Extensions;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        // uncomment if you want to add a UI
        builder.Services.AddRazorPages();

        //add services to the container
        builder.Services.ConfigureCookiePolicy();
        builder.Services.ConfigureCors();
        builder.Services.ConfigureIdentityServer(builder.Configuration);

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        // uncomment if you want to add a UI
        app.UseStaticFiles();
        app.UseCors("AllowAll");
        app.UseRouting();

        //set cookie policy before authentication/authorization setup
        app.UseCookiePolicy();
        app.UseIdentityServer();

        // uncomment if you want to add a UI
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute().RequireAuthorization();
            endpoints.MapRazorPages().RequireAuthorization();
        });

        return app;
    }

    public static IServiceCollection AddGrpcClientConfigure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<UserGrpcService>();
        services.AddGrpcClient<UserProtoService.UserProtoServiceClient>
            (o => o.Address = new Uri(configuration["GrpcSettings:UserUrl"]));
        return services;
    }
}
