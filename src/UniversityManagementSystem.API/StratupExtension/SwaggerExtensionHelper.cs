using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using UniversityManagementSystem.DLL.DbContext;

namespace UniversityManagementSystem.API.StratupExtension
{
    public static class SwaggerExtensionHelper
    {
        public static IServiceCollection AddSwaggerExtensionHelper(this IServiceCollection services)
        {
            services.AddSwaggerGen(c => {
                //Add security description for the Bearer token
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] then token in the text input below."
                });
                //Add a global security requirements for the Bearer token
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    { 
                        new OpenApiSecurityScheme{ 
                            Reference = new OpenApiReference{
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        }, new string[] {}  
                    }
                });
            });
            return services;
        }
    }
}
