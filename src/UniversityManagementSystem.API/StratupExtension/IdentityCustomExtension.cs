using Microsoft.AspNetCore.Identity;
using UniversityManagementSystem.DLL.DbContext;
using UniversityManagementSystem.DLL.Model;

namespace UniversityManagementSystem.API.StratupExtension
{
    public static class IdentityCustomExtensionHelper
    {
        public static IServiceCollection AddIdentityCustomExtensionHelper(this IServiceCollection services)
        {
            //services.AddAuthorization();    //add Identity Dependencies
            services.AddIdentity<ApplicationUser,ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            return services;
        }

        public static IApplicationBuilder UseOwnApplicationAuthentication(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            return app;
        }
    }
}
