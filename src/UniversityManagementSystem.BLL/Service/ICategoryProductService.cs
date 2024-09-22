using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.BLL.GenericResponseFormat;
using UniversityManagementSystem.DLL.Model;
using UniversityManagementSystem.DLL.uow;

namespace UniversityManagementSystem.BLL.Service
{
    public interface ICategoryProductService
    {
        Task<ApiResponse<List<Product>>> GetAllProductByCategory(int id);
    }
    public class CategoryProductService : ICategoryProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<List<Product>>> GetAllProductByCategory(int id)
        {
            var product = await _unitOfWork.ProductRepository
                .FindByCondition(x => x.Category.Id == id).ToListAsync();
            return new ApiResponse<List<Product>>(product, true, "Products fetched successfully");
        }
    }
}
