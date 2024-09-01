using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.BLL.ViewModel.Requests;
using UniversityManagementSystem.DLL.DbContext;
using UniversityManagementSystem.DLL.Model;
using UniversityManagementSystem.DLL.Repository;

namespace UniversityManagementSystem.BLL.Service
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);

        Task<Product> InsertProduct(ProductInsertRequestViewModel product);

        Task<Product> UpdateProduct(int id, ProductInsertRequestViewModel product);

        Task<Product> DeleteProduct(int id);
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;


        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _productRepository.FindAll().ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _productRepository.FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Product> DeleteProduct(int id)
        {
            var product = await GetProductById(id);

            if (product == null)
            {
                throw new Exception("product not found");
            }

            _productRepository.Delete(product);

            if (await _productRepository.SaveChangesAsync())
            {
                return product;
            }

            throw new Exception("something went wrong");
        }



        public async Task<Product> InsertProduct(ProductInsertRequestViewModel request)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price
            };
            _productRepository.Create(product);

            if (await _productRepository.SaveChangesAsync())
            {
                return product;
            }

            throw new Exception("something went wrong");
        }

        public async Task<Product> UpdateProduct(int id, ProductInsertRequestViewModel request)
        {
            var product = await GetProductById(id);
            if (!request.Name.IsNullOrEmpty())
            {
                product.Name = request.Name;
            }

            if (!request.Description.IsNullOrEmpty())
            {
                product.Description = request.Description;
            }
            if (request.Price != 0)
            {
                product.Price = request.Price;
            }

            _productRepository.Update(product);

            if (await _productRepository.SaveChangesAsync())
            {
                return product;
            }

            throw new Exception("something went wrong");
        }
    }
}
