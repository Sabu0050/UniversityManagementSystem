using Bogus;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.DLL.Model;
using UniversityManagementSystem.DLL.uow;

namespace UniversityManagementSystem.BLL.Service
{
    public interface ISeedService
    {
        Task GenerateSeedAsync();
    }

    public class SeedService : ISeedService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SeedService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task GenerateSeedAsync()
        {
            //// Generate fake categories
            //var categoryFaker = new Faker<Category>()
            //    .RuleFor(c => c.Name, f => f.Commerce.Categories(1)[0])
            //    .RuleFor(c => c.ShortName, (f, c) => c.Name.Substring(0, 3).ToUpper());

            //var categories = categoryFaker.Generate(50);

            //// Generate fake products
            //var productFaker = new Faker<Product>()
            //    .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            //    .RuleFor(p => p.Description, f => f.Lorem.Sentence())
            //    .RuleFor(p => p.Price, 100);

            //var products = productFaker.Generate(100);

            var categories = await _unitOfWork.CategoryRepository.FindAll().ToListAsync();
            var products = await _unitOfWork.ProductRepository.FindAll().ToListAsync();

            // Generate fake many-to-many relationships between categories and products
            var categoryProductFaker = new Faker<CategoryProduct>()
                .RuleFor(cp => cp.CategoryId, f => f.PickRandom(categories).Id)
                .RuleFor(cp => cp.ProductId, f => f.PickRandom(products).Id);

            var categoryProducts =
                categoryProductFaker.Generate(500); // Adjust the number to increase or decrease many-to-many relationships

            var uniqueCategoryProducts = categoryProducts
                .GroupBy(cp => new { cp.CategoryId, cp.ProductId })
                .Select(group => group.First())
                .ToList();

            //_unitOfWork.CategoryRepository.CreateRange(categories);
            //_unitOfWork.ProductRepository.CreateRange(products);
            _unitOfWork.CategoryProductRepository.CreateRange(uniqueCategoryProducts);

            var result = await _unitOfWork.SaveChangesAsync();
        }
    }
}
