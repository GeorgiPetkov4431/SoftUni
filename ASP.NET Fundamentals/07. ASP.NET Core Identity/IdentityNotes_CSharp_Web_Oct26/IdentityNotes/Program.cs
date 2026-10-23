using IdentityNotes.Data;
using IdentityNotes.Services;
using IdentityNotes.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityNotes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            string connectionString = builder.Configuration.GetConnectionString("SqlServerDev")
                ?? throw new InvalidOperationException("Connection string 'SqlServerDev' not found.");
            builder.Services.AddDbContext<IdentityNotesDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                ConfigureIdentityOptions(options, builder.Configuration);
            })
                .AddEntityFrameworkStores<IdentityNotesDbContext>();

            /* AddSingleton() -> registers static singleton instance of service, lifetime = application lifetime, suitable for state management */
            /* AddScoped() -> register scoped instance of service, lifetime = scope lifetime, ASP.NET Core Specifics -> if injection is performed in Controller, lifetime = HTTP Request - Response lifetime */
            /* AddTransient() -> register transient instance of service, no lifetime, each injection = new instance */
            builder.Services.AddScoped<INoteService, NoteService>();

            builder.Services.AddControllersWithViews();

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            /* Enables middleware, which allows the app to extract user information from the request 
             * and set the User property on HttpContext. */
            app.UseAuthentication();

            /* Enables middleware, which allows the app to determine whether a user is authorized to access a resource. */
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }

        private static void ConfigureIdentityOptions(IdentityOptions options, ConfigurationManager configuration)
        {
            options.Password.RequireDigit = 
                configuration.GetValue<bool>("Identity:Password:RequireDigit");
            options.Password.RequireLowercase = 
                configuration.GetValue<bool>("Identity:Password:RequireLowercase");
            options.Password.RequireUppercase = 
                configuration.GetValue<bool>("Identity:Password:RequireUppercase");
            options.Password.RequireNonAlphanumeric =
                configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
            options.Password.RequiredUniqueChars =
                configuration.GetValue<int>("Identity:Password:RequiredUniqueChars");
            options.Password.RequiredLength =
                configuration.GetValue<int>("Identity:Password:RequiredLength");

            options.SignIn.RequireConfirmedAccount =
                configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
            
            options.User.RequireUniqueEmail =
                configuration.GetValue<bool>("Identity:User:RequireUniqueEmail");
        }
    }
}
