using Azure.Core;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.DLL.DbContext;
using UniversityManagementSystem.DLL.Model;

namespace UniversityManagementSystem.BLL.Service
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategories();
        Task<Category> GetCategoryById(int id);

        Task<Category> InsertCategory(Category category);

        Task<Category> UpdateCategory(int id ,Category category);

        Task<int> DeleteCategory(int id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context)
        {
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

        public async Task<Category> InsertCategory(Category category)
        {
           
            _context.Categories.Add(category);

            if (await _context.SaveChangesAsync() > 0)
            {

                return category;
            }

            return null;
        }

        public async Task<Category> UpdateCategory(int id, Category request)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category != null)
            {
                category.Name = request.Name;
                _context.Categories.Update(category);

                if (await _context.SaveChangesAsync() > 0)
                {
                    return category;
                }
            }

            return null;

        }
        public async Task<int> DeleteCategory(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            _context.Categories.Remove(category);
            if (await _context.SaveChangesAsync() > 0)
            {
                return 1;
            }

            return 0;
        }
    }
}
