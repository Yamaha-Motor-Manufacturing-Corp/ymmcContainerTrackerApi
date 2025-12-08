using Microsoft.EntityFrameworkCore;
using YmmcContainerTrackerApi.Data;
using YmmcContainerTrackerApi.Services; // ADD THIS LINE

namespace YmmcContainerTrackerApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Controllers (API)
            builder.Services.AddControllers();

            // LDAP Infrastructure (ready for activation)
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IUserService, UserService>(); // ADD THIS LINE

            // TODO: Uncomment when ready to enable Windows Authentication
            // builder.Services.AddAuthentication(IISDefaults.AuthenticationScheme);
            // builder.Services.AddAuthorization();

            // Razor Pages (UI)
            var razor = builder.Services.AddRazorPages();
            if (builder.Environment.IsDevelopment())
            {
                razor.AddRazorRuntimeCompilation();
            }

            // Swagger (API docs)
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // EF Core
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    sqlServerOptions => sqlServerOptions.CommandTimeout(120)
                )
            );

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // If you later add css/js under wwwroot
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();
            app.MapRazorPages();

            app.Run();
        }
    }
}
