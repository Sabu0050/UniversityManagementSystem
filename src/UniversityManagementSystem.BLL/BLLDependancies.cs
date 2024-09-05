using Microsoft.Extensions.DependencyInjection;
using UniversityManagementSystem.BLL.Service;

namespace UniversityManagementSystem.BLL
{
    public static class BLLDependancies
    {
        public static IServiceCollection AddBLLDependancies(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISeedService, SeedService>();
            return services;
        }
    }
}
