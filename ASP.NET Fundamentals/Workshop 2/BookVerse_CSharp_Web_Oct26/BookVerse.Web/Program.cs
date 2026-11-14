using BookVerse.Data;
using BookVerse.Services.Core;
using BookVerse.Services.Core.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookVerse.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("SqlServerDev")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<BookVerseDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                ConfigureIdentityOptions(options, builder.Configuration);
            })
            .AddEntityFrameworkStores<BookVerseDbContext>();

            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IGenreService, GenreService>();

            builder.Services.AddControllersWithViews();

            WebApplication app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();
        }

        private static void ConfigureIdentityOptions(IdentityOptions options, ConfigurationManager configuration)
        {
            options.SignIn.RequireConfirmedAccount = 
                configuration.GetValue<bool>("RequireConfirmedAccoun");
            options.Password.RequireDigit =
                configuration.GetValue<bool>("RequireDigit");
            options.Password.RequireLowercase =
                configuration.GetValue<bool>("RequireLowercase");
            options.Password.RequireNonAlphanumeric =
                configuration.GetValue<bool>("RequireNonAlphanumeric");
            options.Password.RequireUppercase =
                configuration.GetValue<bool>("RequireUppercase");
        }
    }
}
