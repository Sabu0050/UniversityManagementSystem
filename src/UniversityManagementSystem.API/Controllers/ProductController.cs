using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.API.ViewModel.Requests;
using UniversityManagementSystem.BLL.Service;
using UniversityManagementSystem.DLL.DbContext;
using UniversityManagementSystem.DLL.Model;

namespace UniversityManagementSystem.API.Controllers
{
    public class ProductController : ApiBaseController
    {
        
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;

        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {

            var products = await _productService.GetAllProducts();
            return Ok(products);
        }



        [HttpGet("id")]
        public async Task<IActionResult> GetAProductData(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound("Data not found");
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> InsertProduct(ProductInsertRequestViewModel request)
        {

            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price
            };
            var result = await _productService.InsertProduct(product);

            if (result == null)
            {
                return NotFound("Data not found.");
            }
            return Ok(result);

        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateProduct(int id, ProductInsertRequestViewModel request)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price
            };
            var result = await _productService.UpdateProduct(id, product);

            if (result == null)
            {
                return NotFound("Data not found");
            }

            return Ok(result);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteProduct(int Id)
        {

            var result = await _productService.DeleteProduct(Id);

            if (result != 0) return Ok("Data delete successfully.");

            return BadRequest("Data request format is incorrect.");
        }
    }
}
