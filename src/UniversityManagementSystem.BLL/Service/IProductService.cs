using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UniversityManagementSystem.BLL.ViewModel.Requests;
using UniversityManagementSystem.DLL.Helper;
using UniversityManagementSystem.DLL.Model;
using UniversityManagementSystem.DLL.uow;

namespace UniversityManagementSystem.BLL.Service
{
    public interface IProductService
    {
        Task<List<Product>> GetAll();
        Task<Product?> GetAData(int id);
        Task<Product> AddProduct(ProductInsertRequestViewModel request);
        Task<Product> UpdateProduct(int id, ProductInsertRequestViewModel request);
        Task<Product> DeleteProduct(int id);

    }

    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Product>> GetAll()
        {
            return await _unitOfWork.ProductRepository.FindAll().ToListAsync();
        }

        public async Task<Product> GetAData(int id)
        {
            return await _unitOfWork.ProductRepository.FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Product> AddProduct(ProductInsertRequestViewModel request)
        {
            var product = new Product {
                Name = request.Name,
                Description = request.Description,
                Price = (decimal) request.Price
            };
            _unitOfWork.ProductRepository.Create(product);

            if (await _unitOfWork.SaveChangesAsync())
            {
                return product;
            }

            throw new Exception("something went wrong");
        }

        public async Task<Product> UpdateProduct(int id, ProductInsertRequestViewModel request)
        {
            var product = await _unitOfWork.ProductRepository.FindByConditionWithTracking(x=>x.Id==id).FirstOrDefaultAsync();

            if (product == null)
            {
                throw new Exception("category not found");
            }

            if (!request.Name.IsNullOrEmpty())
            {
                product.Name = request.Name;
            }

            if (!request.Description.IsNullOrEmpty())
            {
                product.Description = request.Description;
            }
            if (request.Price>0)
            {
                product.Price = (decimal)request.Price;
            }

            _unitOfWork.ProductRepository.Update(product);

            if (await _unitOfWork.SaveChangesAsync())
            {
                return product;
            }

            throw new Exception("something went wrong");
        }


        public async Task<Product> DeleteProduct(int id)
        {
            var product = await GetAData(id);

            if (product == null)
            {
                throw new Exception("category not found");
            }

            _unitOfWork.ProductRepository.Delete(product);

            if (await _unitOfWork.SaveChangesAsync())
            {
                return product;
            }

            throw new Exception("something went wrong");
        }


    }
}
