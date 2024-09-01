using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.BLL.ViewModel.Requests;
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
            return Ok(await _productService.GetAllProducts());
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetAProductData(int id)
        {
            return Ok(await _productService.GetProductById(id));
        }

        [HttpPost]
        public async Task<ActionResult> InsertProduct(ProductInsertRequestViewModel request)
        {
            return Ok(await _productService.InsertProduct(request));
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateProduct(int id, ProductInsertRequestViewModel request)
        {
            return Ok(await _productService.UpdateProduct(id, request));
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteProduct(int Id)
        {
            return Ok(await _productService.DeleteProduct(Id));
        }
    }
}
