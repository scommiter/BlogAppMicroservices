using Infrastructure;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Post.Api.Services;
using Post.Application.Commons.Interfaces;
using Post.Application.Mappings;
using Post.Infrastructure.Persistence;
using Post.Infrastructure.Repositories;
using Shared.Configurations;
using System.Reflection;

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
            services.AddScoped<Seed>().AddScoped<ILogger, Logger<Seed>>(); ;
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddHttpContextAccessor();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ITreePathRepository, TreePathRepository>();
            services.AddTransient<IIdentityService, IdentityService>();

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services) =>
            services.AddAutoMapper(Assembly.GetExecutingAssembly())
                    .AddAutoMapper(typeof(MappingProfile))
            ;
    }
}
