using Homies.Data;
using Homies.Services.Core.Contracts;
using Homies.Services.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<ITypeService, TypeService>();
            return services;
        }

        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("SqlServerDev")
                ?? throw new InvalidOperationException("Connection String is not configured properly!");

            services.AddDbContext<HomiesDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddDatabaseDeveloperPageExceptionFilter();

            return services;
        }
        
        public static IServiceCollection AddApplicationIdentity(this IServiceCollection services)
        {
            services.AddDefaultIdentity<IdentityUser>(options =>
            {
                /* Caution: Dev Only options */
                /* TODO: Use appsettings.json to store different options depending on the app environment */
                options.User.RequireUniqueEmail = false;

                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 3;

                options.SignIn.RequireConfirmedAccount = false;
            })
                .AddEntityFrameworkStores<HomiesDbContext>();

            return services;
        }
    }
}
