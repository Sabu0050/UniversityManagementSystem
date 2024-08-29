using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UniversityManagementSystem.BLL.ViewModel.Requests;
using UniversityManagementSystem.DLL.DbContext;
using UniversityManagementSystem.DLL.Model;
using UniversityManagementSystem.DLL.Repository;

namespace UniversityManagementSystem.BLL.Service
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategories();
        Task<Category> GetCategoryById(int id);

        Task<Category> InsertCategory(CategoryInsertRequestViewModel category);

        Task<Category> UpdateCategory(int id ,CategoryInsertRequestViewModel category);

        Task<Category> DeleteCategory(int id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        public CategoryService(ICategoryRepository repository)
        {
           _repository = repository;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _repository.GetAllCategories();
        }

        public async Task<Category> GetCategoryById(int id)
        {
           return await _repository.GetCategoryById(id);
        }

        public async Task<Category> InsertCategory(CategoryInsertRequestViewModel request)
        {
            var category = new Category { 
                Name = request.Name,
                ShortName = request.ShortName,
            };

            return await _repository.InsertCategory(category);

            
        }

        public async Task<Category> UpdateCategory(int id, CategoryInsertRequestViewModel request)
        {
            var category = await GetCategoryById(id);
            if(!request.Name.IsNullOrEmpty())
            {
                category.Name = request.Name;

            }
            if(!request.ShortName.IsNullOrEmpty())
            {
                category.ShortName = request.ShortName;
            }
            
            return await _repository.UpdateCategory(category);

        }
        public async Task<Category> DeleteCategory(int id)
        {


           return await _repository.DeleteCategory(id);
        }

    }
}
