using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using UniversityManagementSystem.API.ViewModel.Requests;
using UniversityManagementSystem.BLL.Service;
using UniversityManagementSystem.DLL.DbContext;
using UniversityManagementSystem.DLL.Model;

namespace UniversityManagementSystem.API.Controllers
{
    public class CategoryController : ApiBaseController
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {

            var category = await _categoryService.GetAllCategories();
            return Ok(category);
        }

        

        [HttpGet("id")]
        public async Task<IActionResult> GetAData(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound("Data not found");
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult> InsertCategory(CategoryInsertRequestViewModel request) {

           var category = new Category { 
                Name = request.Name
           };
            var result = await _categoryService.InsertCategory(category);

            if (result == null) {
                return NotFound("Data not found.");
            }
            return Ok(result);

        }

        [HttpPut("id")]
        public async Task<IActionResult> Update(int id, CategoryInsertRequestViewModel request)
        {
            var category = new Category
            {
                Name = request.Name
            };
            var result = await _categoryService.UpdateCategory(id, category);

            if (result == null)
            {
                return NotFound("Data not found");
            }

            return Ok(result);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteCategory(int Id) {
            
            var result = await _categoryService.DeleteCategory(Id);

            if (result != 0) return Ok("Data delete successfully.");

            return BadRequest("Data request format is incorrect.");
        }

    }
}
