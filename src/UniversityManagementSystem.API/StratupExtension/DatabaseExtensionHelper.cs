using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.DLL.DbContext;

namespace UniversityManagementSystem.API.StratupExtension
{
    public static class DatabaseExtensionHelper
    {
        public static IServiceCollection AddDatabaseExtensionHelper(this IServiceCollection service, IConfiguration configuration) {

            service.AddDbContext<ApplicationDbContext>(opt=>
            opt.UseSqlServer(configuration.GetConnectionString("DefaultString")));

            return service;
        }

        public static IApplicationBuilder RunMigration(
        this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.Migrate();
            }
            return app;
        }
    }
}
