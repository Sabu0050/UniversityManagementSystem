using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.DLL.DbContext;
using UniversityManagementSystem.DLL.Model;

namespace UniversityManagementSystem.DLL.Repository
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategories();
        Task<Category> GetCategoryById(int id);

        Task<Category> InsertCategory(Category category);

        Task<Category> UpdateCategory(Category category);

        Task<Category> DeleteCategory(int id);
    }
    public class CategoryRepository : ICategoryRepository { 
    
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories.AsQueryable().ToListAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category> InsertCategory(Category request)
        {
            var category = new Category
            {
                Name = request.Name,
                ShortName = request.ShortName,
            };

            _context.Categories.Add(category);

            if (await _context.SaveChangesAsync() > 0)
            {

                return category;
            }

            throw new Exception("Something went wrong.");
        }

        public async Task<Category> UpdateCategory(Category category)
        {
             _context.Categories.Update(category);

            if (await _context.SaveChangesAsync() > 0)
            {
                return category;
            }
            throw new Exception("Something went wrong");

        }
        public async Task<Category> DeleteCategory(int id)
        {
            var category = await GetCategoryById(id);

            _context.Categories.Remove(category);
            if (await _context.SaveChangesAsync() > 0)
            {
                return category;
            }

            throw new Exception("Something went wrong");
        }
    }
}
