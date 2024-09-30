using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using UniversityManagementSystem.BLL.Service;
using UniversityManagementSystem.BLL.Validation;
using UniversityManagementSystem.BLL.ViewModel.Requests;

namespace UniversityManagementSystem.BLL
{
    public static class BLLDependancies
    {
        public static IServiceCollection AddBLLDependancies(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            AllFluentValidator(services);
            return services;
        }

        public static void AllFluentValidator(IServiceCollection services)
        { 
            services.AddScoped<IValidator<CategoryInsertRequestViewModel>,CategoryInsertViewModelValidator>();
            services.AddScoped<IValidator<RegistrationViewModel>, RegistrationViewModelValidator>();
        }
    }
}
