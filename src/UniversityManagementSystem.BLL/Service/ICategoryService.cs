using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UniversityManagementSystem.BLL.ViewModel.Requests;
using UniversityManagementSystem.DLL.Model;
using UniversityManagementSystem.DLL.Repository;
using UniversityManagementSystem.DLL.uow;

namespace UniversityManagementSystem.BLL.Service
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAll();
        Task<Category?> GetAData(int id);
        Task<Category> AddCategory(CategoryInsertRequestViewModel request);
        Task<Category> UpdateCategory(int id, CategoryInsertRequestViewModel request);
        Task<Category> DeleteCategory(int id);

    }

    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Category>> GetAll()
        {
            return await _unitOfWork.CategoryRepository.FindAll().ToListAsync();
        }

        public async Task<Category> GetAData(int id)
        {
            return await _unitOfWork.CategoryRepository.FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Category> AddCategory(CategoryInsertRequestViewModel request)
        {
            var category = new Category { 
                Name = request.Name,
                ShortName = request.ShortName
            };
            _unitOfWork.CategoryRepository.Create(category);

            if (await _unitOfWork.SaveChangesAsync())
            {
                return category;
            }

            throw new Exception("something went wrong");
        }

        public async Task<Category> UpdateCategory(int id, CategoryInsertRequestViewModel request)
        {
            var category = await GetAData(id);

            if (category == null)
            {
                throw new Exception("category not found");
            }

            if (!request.Name.IsNullOrEmpty())
            {
                category.Name = request.Name;
            }

            if (!request.ShortName.IsNullOrEmpty())
            {
                category.ShortName = request.ShortName;
            }

            _unitOfWork.CategoryRepository.Update(category);

            if (await _unitOfWork.SaveChangesAsync())
            {
                return category;
            }

            throw new Exception("something went wrong");
        }


        public async Task<Category> DeleteCategory(int id)
        {
            var category = await GetAData(id);

            if (category == null)
            {
                throw new Exception("category not found");
            }

            _unitOfWork.CategoryRepository.Delete(category);

            if (await _unitOfWork.SaveChangesAsync())
            {
                return category;
            }

            throw new Exception("something went wrong");
        }


    }
}
