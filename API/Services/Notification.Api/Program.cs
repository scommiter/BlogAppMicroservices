using Common.Logging;
using Notification.Api.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(Serilogger.Configure);

try
{
    builder.Host.AddAppConfigurations();
    builder.Services.AddConfigurationSettings(builder.Configuration);
    builder.Services.AddInfrastructure(builder.Configuration);

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
    {
        throw;
    }
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information($"Shut down {builder.Environment.ApplicationName} complete");
    Log.CloseAndFlush();
}