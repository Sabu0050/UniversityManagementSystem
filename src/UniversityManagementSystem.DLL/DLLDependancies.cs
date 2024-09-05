using Microsoft.Extensions.DependencyInjection;
using UniversityManagementSystem.DLL.Repository;
using UniversityManagementSystem.DLL.uow;

namespace UniversityManagementSystem.DLL
{
    public static class DLLDependancies
    {
        public static IServiceCollection AddDLLDependancies(this IServiceCollection services)
        {
           // services.AddScoped<ICategoryRepository, CategoryRepository>();
           // services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
