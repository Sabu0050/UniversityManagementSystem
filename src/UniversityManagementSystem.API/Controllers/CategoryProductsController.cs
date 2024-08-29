using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.API.ViewModel.Requests;
using UniversityManagementSystem.BLL.Service;
using UniversityManagementSystem.DLL.Model;

namespace UniversityManagementSystem.API.Controllers
{
    public class CategoryProductsController : ApiBaseController
    {
        private readonly IProductCategoryService _productCategoryService;

        public CategoryProductsController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productCategoryService.GetAllProduct();
            return Ok(products);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _productCategoryService.DeleteProductAsync(id);
            if(result>0) return Ok("Delete Successfully.");
            return BadRequest("Delete error.");
        }
    }
}
