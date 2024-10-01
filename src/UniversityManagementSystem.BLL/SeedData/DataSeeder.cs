using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Abstractions;
using UniversityManagementSystem.DLL.Model;

namespace UniversityManagementSystem.BLL.SeedData
{
    public static class DataSeeder
    {
        public static void SeedApplication(IServiceProvider serviceProvider)
        {
            var applicationManager = serviceProvider.GetRequiredService<IOpenIddictApplicationManager>();
            if (applicationManager.FindByClientIdAsync("customer_to_bkash_main_api").Result is null)
            {
                var app = applicationManager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "customer_to_bkash_main_api",
                    ClientSecret = "901564A5-E7FE-42CB-B10D-61EF6A8F3654",
                    DisplayName = "My customer call our bkash application",
                   // PostLogoutRedirectUris = { new Uri("http://localhost:53507/signout-callback-oidc") },
                   // RedirectUris = { new Uri("http://localhost:53507/signin-oidc") },
                    Permissions =
                    {
                        //OpenIddictConstants.Permissions.Endpoints.Authorization,
                        //OpenIddictConstants.Permissions.Endpoints.Logout,
                        OpenIddictConstants.Permissions.Endpoints.Token,
                        OpenIddictConstants.Permissions.GrantTypes.Password
                    }
                }).Result;
            }
            
            if (applicationManager.FindByClientIdAsync("customer_from_facebook").Result is null)
            {
                var app = applicationManager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "customer_from_facebook",
                    ClientSecret = "901564A5-E7FE-42CB-B10D-61EF6A8F3654",
                    DisplayName = "My facebook customer call our bkash application",
                   // PostLogoutRedirectUris = { new Uri("http://localhost:53507/signout-callback-oidc") },
                   // RedirectUris = { new Uri("http://localhost:53507/signin-oidc") },
                    Permissions =
                    {
                        //OpenIddictConstants.Permissions.Endpoints.Authorization,
                        //OpenIddictConstants.Permissions.Endpoints.Logout,
                        OpenIddictConstants.Permissions.Endpoints.Token,
                        OpenIddictConstants.Permissions.GrantTypes.Password,
                        OpenIddictConstants.Permissions.GrantTypes.RefreshToken
                    }
                }).Result;
            }
        }
        public static void SeedUserRoleData(IServiceProvider serviceProvider)
        {
            string[] roleNames = { "Admin", "Customer", "Manager", "Moderator","SuperAdmin"};
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            foreach ( var roleName in roleNames )
            {
                var lowerValue = roleName.ToLower();
                var isRoleExist = roleManager.RoleExistsAsync(roleName).Result;
                if ( !isRoleExist)
                {
                    var res = roleManager.CreateAsync(new ApplicationRole() {
                        Name = lowerValue
                    }).Result;
                }

            }
        }

        public static List<Category> SeedCategories(int count)
        {
            var fakerCategory = new Faker<Category>()
                .RuleFor(c => c.Name, f => f.Commerce.Categories(1)[0])
                .RuleFor(c => c.ShortName, f => f.Commerce.ProductAdjective());

            var categories = fakerCategory.Generate(count);
            Random random = new Random();
            foreach (var category in categories)
            {
                int randomNumber  = random.Next(2, 10);
                category.Products = SeedProducts(category.Id, randomNumber); // Seed 10 products per category
            }

            return categories;
        }

        public static List<Product> SeedProducts(int categoryId, int count)
        {
            var fakerProduct = new Faker<Product>()
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
                .RuleFor(p => p.Price, f => Convert.ToDecimal(f.Commerce.Price(10, 1000)))// Generates a decimal between 10 and 1000
                .RuleFor(p => p.CategoryId, f => categoryId);

            return fakerProduct.Generate(count);
        }
    }
}
