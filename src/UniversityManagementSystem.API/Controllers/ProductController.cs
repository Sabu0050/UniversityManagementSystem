using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.BLL.ViewModel.Requests;
using UniversityManagementSystem.BLL.Service;
using Microsoft.AspNetCore.Authorization;

namespace UniversityManagementSystem.API.Controllers
{
    public class ProductController : ApiBaseController
    {
        
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;

        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _productService.GetAll());
        }
        [Authorize]
        [HttpGet("id")]
        public async Task<IActionResult> GetAProductData(int id)
        {
            return Ok(await _productService.GetAData(id));
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> InsertProduct(ProductInsertRequestViewModel request)
        {
            return Ok(await _productService.AddProduct(request));
        }
        [Authorize]
        [HttpPut("id")]
        public async Task<IActionResult> UpdateProduct(int id, ProductInsertRequestViewModel request)
        {
            return Ok(await _productService.UpdateProduct(id, request));
        }
        [Authorize]
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteProduct(int Id)
        {
            return Ok(await _productService.DeleteProduct(Id));
        }
    }
}
