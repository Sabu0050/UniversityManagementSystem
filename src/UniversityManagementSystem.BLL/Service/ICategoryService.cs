using Azure.Core;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UniversityManagementSystem.BLL.GenericResponseFormat;
using UniversityManagementSystem.BLL.Validation;
using UniversityManagementSystem.BLL.ViewModel.Requests;
using UniversityManagementSystem.DLL.Model;
using UniversityManagementSystem.DLL.Repository;
using UniversityManagementSystem.DLL.uow;

namespace UniversityManagementSystem.BLL.Service
{
    public interface ICategoryService
    {
        Task<ApiResponse<List<Category>>> GetAll();
        Task<Category?> GetAData(int id);
        Task<ApiResponse<Category>> AddCategory(CategoryInsertRequestViewModel request);
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

        public async Task<ApiResponse<List<Category>>> GetAll()
        {
            var categoryQuary = _unitOfWork.CategoryRepository.FindAll();
            var totalData = await categoryQuary.CountAsync();
            var result = await categoryQuary.ToListAsync();
            return new ApiPaginateResponse<List<Category>>(result,1,10,totalData);
        }

        public async Task<Category> GetAData(int id)
        {
            return await _unitOfWork.CategoryRepository.FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ApiResponse<Category>> AddCategory(CategoryInsertRequestViewModel request)
        {
            var validator = await new CategoryInsertViewModelValidator().ValidateAsync(request);
            if (!validator.IsValid)
            {
                return new ApiResponse<Category>(validator.Errors);

            }

            var categoryValidation = await IsCategoryAlreadyExists(request);

            if (categoryValidation.IsFailure) {
                return new ApiResponse<Category>(null, false, categoryValidation.Error);
            }

            var category = new Category
            {
                Name = request.Name,
                ShortName = request.ShortName
            };
            _unitOfWork.CategoryRepository.Create(category);

            if (await _unitOfWork.SaveChangesAsync())
            {
                return new ApiResponse<Category>(category, true, "Data Insert Successfully.");
            }

            return new ApiResponse<Category>(null, false, "somthing wend wrong");
        }
        public async Task<Category> UpdateCategory(int id, CategoryInsertRequestViewModel request)
        {
            var category = await _unitOfWork.CategoryRepository.FindByConditionWithTracking(x => x.Id == id).FirstOrDefaultAsync();

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

         private async Task<Result<bool>> IsCategoryAlreadyExists(CategoryInsertRequestViewModel request)
        {
            var categoryInDB = await _unitOfWork.CategoryRepository.FindByCondition(x => x.Name == request.Name && x.ShortName == request.ShortName).ToListAsync();
            if (categoryInDB.Any())
            {
                var message = "";
                var nameExist = categoryInDB.Exists(x => x.Name == request.Name);
                if (nameExist)
                {
                    message += "Name already exist.\n";
                }

                var shortNameExist = categoryInDB.Exists(x => x.ShortName == request.ShortName);
                if (shortNameExist)
                {
                    message += "Short name already exist.\n";
                }

                return Result.Failure<bool>(message);
            }
            return Result.Success(true);
        }

    }
}
