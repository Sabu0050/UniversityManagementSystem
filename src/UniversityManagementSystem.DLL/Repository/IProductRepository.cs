using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.DLL.DbContext;
using UniversityManagementSystem.DLL.Model;

namespace UniversityManagementSystem.DLL.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);

        Task<Product> InsertProduct(Product product);

        Task<Product> UpdateProduct(Product product);

        Task<Product> DeleteProduct(int id);
    }
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product> DeleteProduct(int id)
        {
            var product = await GetProductById(id);

            _context.Products.Remove(product);
            if (await _context.SaveChangesAsync() > 0)
            {
                return product;
            }

            throw new Exception("Something went wrong");
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.AsQueryable().ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> InsertProduct(Product request)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price
            };

            _context.Products.Add(product);

            if (await _context.SaveChangesAsync() > 0)
            {

                return product;
            }

            throw new Exception("Something went wrong.");
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            _context.Products.Update(product);

            if (await _context.SaveChangesAsync() > 0)
            {
                return product;
            }
            throw new Exception("Something went wrong");
        }
    }
}
