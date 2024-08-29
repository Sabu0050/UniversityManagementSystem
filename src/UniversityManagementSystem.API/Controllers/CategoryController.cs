using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.BLL.Service;
using UniversityManagementSystem.BLL.ViewModel.Requests;

namespace UniversityManagementSystem.API.Controllers
{
    public class CategoryController : ApiBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {

            return Ok(await _categoryService.GetAllCategories());
        }

        

        [HttpGet("id")]
        public async Task<IActionResult> GetAData(int id)
        {
            return Ok(await _categoryService.GetCategoryById(id));
        }

        [HttpPost]
        public async Task<ActionResult> InsertCategory(CategoryInsertRequestViewModel request) {

            return Ok(await _categoryService.InsertCategory(request));
        }

        [HttpPut("id")]
        public async Task<IActionResult> Update(int id, CategoryInsertRequestViewModel request)
        {
            return Ok(await _categoryService.UpdateCategory(id, request));
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteCategory(int Id) {  
            return Ok(await _categoryService.DeleteCategory(Id));
        }

    }
}
