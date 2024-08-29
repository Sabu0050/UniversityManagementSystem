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
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Product> DeleteProduct(int id)
        {
            return await _repository.DeleteProduct(id);
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _repository.GetAllProducts();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _repository.GetProductById(id);
        }

        public async Task<Product> InsertProduct(ProductInsertRequestViewModel request)
        {
            var product = new Product {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price
            };
            return await _repository.InsertProduct(product);
        }

        public async Task<Product> UpdateProduct(int id, ProductInsertRequestViewModel request)
        {
            var product = await GetProductById(id);
            if(!request.Name.IsNullOrEmpty())
            {
                product.Name = request.Name;
            }

            if(!request.Description.IsNullOrEmpty()) 
            {
                product.Description = request.Description;
            }
            if(request.Price !=0)
            {
                product.Price = request.Price;
            }

            return await _repository.UpdateProduct(product);
        }
    }
}
