using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.BLL.SeedData;
using UniversityManagementSystem.DLL.DbContext;

namespace UniversityManagementSystem.API.StratupExtension
{
    public static class DatabaseExtensionHelper
    {
        public static IServiceCollection AddDatabaseExtensionHelper(this IServiceCollection services, IConfiguration configuration) {

            services.AddDbContext<ApplicationDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultString"));
                opt.UseOpenIddict<int>();
            });
            

            return services;
        }

        public static IApplicationBuilder RunMigration(
        this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.Migrate();
                
                if(!db.Categories.Any() && !db.Products.Any()) {
                    var categories = DataSeeder.SeedCategories(50);
                    db.Categories.AddRange(categories);
                    db.SaveChanges();
                }
                DataSeeder.SeedUserRoleData(scope.ServiceProvider );
                DataSeeder.SeedApplication(scope.ServiceProvider);
            }
            return app;
        }
    }
}
