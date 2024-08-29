using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.DLL.DbContext;
using UniversityManagementSystem.DLL.Model;

namespace UniversityManagementSystem.BLL.Service
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);

        Task<Product> InsertProduct(Product product);

        Task<Product> UpdateProduct(int id, Product product);

        Task<int> DeleteProduct(int id);
    }

    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> DeleteProduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            _context.Products.Remove(product);
            if (await _context.SaveChangesAsync() > 0)
            {
                return 1;
            }

            return 0;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.AsQueryable().ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Product> InsertProduct(Product product)
        {
            _context.Products.Add(product);

            if (await _context.SaveChangesAsync() > 0)
            {

                return product;
            }

            return null;
        }

        public async Task<Product> UpdateProduct(int id, Product request)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product != null)
            {
                product.Name = request.Name;
                product.Description = request.Description;
                product.Price = request.Price;
                _context.Products.Update(product);

                if (await _context.SaveChangesAsync() > 0)
                {
                    return product;
                }
            }

            return null;
        }
    }
}
