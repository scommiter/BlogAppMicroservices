using Chat.Api.Extensions;
using Chat.Api.SignalR;
using Common.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Host.UseSerilog(Serilogger.Configure);

try
{
    builder.Host.AddAppConfigurations();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddConfigurationSettings(builder.Configuration);
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: MyAllowSpecificOrigins,
                          builder =>
                          {
                              builder.WithOrigins("http://localhost:4000")
                                          .AllowAnyHeader()
                                          .AllowAnyMethod()
                                          .AllowCredentials();
                          });
    });

    //// Thêm dịch vụ xác thực JWT
    //builder.Services.AddAuthentication(options =>
    //{
    //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    //})
    //.AddJwtBearer(options =>
    //{
    //    // Parmeter check token
    //    options.TokenValidationParameters = new TokenValidationParameters
    //    {
    //        ValidateIssuer = true, // Validate issuer of token
    //        ValidateAudience = true, // Xác minh audience của token
    //        ValidateLifetime = true, // Kiểm tra thời hạn của token (token hết hạn sẽ không hợp lệ)
    //        ValidateIssuerSigningKey = true, // Kiểm tra chữ ký của token

    //        // Issuer và Audience phải khớp với giá trị trong token
    //        ValidIssuer = "http://localhost:7000", // Địa chỉ của issuer (ví dụ Identity Server)
    //        ValidAudience = "https://your-api.com", // Đối tượng mà token có thể truy cập (thường là URL của API)

    //        // Khóa bí mật dùng để kiểm tra chữ ký của token
    //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secret-key"))
    //    };
    //});
    builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.RequireHttpsMetadata = false;
        options.Authority = builder.Configuration["IdentityServer:BaseUrl"]; //url Identity Server
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false// fix 401 response in docker
        };

        options.Events = new JwtBearerEvents
        {   
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];

                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
                {
                    context.Token = accessToken;
                }

                return Task.CompletedTask;
            }
        };
    });

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseDefaultFiles();
    app.UseStaticFiles();

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.UseCors(MyAllowSpecificOrigins);

    app.MapHub<PresenceHub>("hubs/presence");
    app.MapHub<MessageHub>("hubs/message");

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