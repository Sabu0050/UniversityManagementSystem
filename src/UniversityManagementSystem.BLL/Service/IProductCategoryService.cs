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
    public interface IProductCategoryService
    {
        Task<List<Product>> GetAllProduct();

        Task<int> DeleteProductAsync(int id);
    }

    public class ProductCategoryService : IProductCategoryService
    {
        private readonly ApplicationDbContext _context;

        public ProductCategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> DeleteProductAsync(int id)
        {

            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                if (await _context.SaveChangesAsync() > 0) return 1;
            }
            return 0;
        }

        public async Task<List<Product>> GetAllProduct()
        {
            return await _context.Products.AsQueryable().ToListAsync();
        }

    }
}
