using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.DLL.Model;

namespace UniversityManagementSystem.BLL.SeedData
{
    public static class DataSeeder
    {
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
