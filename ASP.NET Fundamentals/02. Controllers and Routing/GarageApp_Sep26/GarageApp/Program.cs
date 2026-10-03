using GarageApp.Data;
using Microsoft.EntityFrameworkCore;

namespace GarageApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            string connectionString = GetConnectionString(builder);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            /* Register GarageAppDbContext in ASP.NET Core ServiceCollection */
            /* This allows ASP.NET Core to instantiate DbContext with configured ConnectionString and
             * pass it anywhere in the application through DI */
            builder.Services.AddDbContext<GarageAppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            WebApplication app = builder.Build();

            /* The .Use{} methods work with shared resource - HTTP Request */
            /* Order of execution of .Use{} methods matters! */

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            /* Enables ASP.NET Core Routing Engine */
            /* Automatic Routing based on URL Path from HTTP Request */
            app.UseRouting();

            app.UseAuthorization();

            /* When configuring more than 1 Route Convention: */
            /* Map the most specific route patterns first */
            /* Continue with broader more generalized patterns */
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
        private static string GetConnectionString(IHostApplicationBuilder builder)
        {
            string? connectionString = builder.Configuration
                .GetConnectionString("SqlServerDev");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                /* Fall back to the default connection */
                connectionString = builder.Configuration
                    .GetConnectionString("DefaultConnection")
                    ?? throw new InvalidOperationException("Default connection string is not configured!");
            }

            return connectionString;
        }
    }
}