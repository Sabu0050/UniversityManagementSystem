using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.BLL.Service;
using UniversityManagementSystem.BLL.ViewModel.Requests;
using UniversityManagementSystem.DLL.Model;

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
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _categoryService.GetAll());
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetAData(int id)
        {
            return Ok(await _categoryService.GetAData(id));
        }

        [HttpPost]
        public async Task<IActionResult> Insert(CategoryInsertRequestViewModel request)
        {
            return Ok(await _categoryService.AddCategory(request));
        }

        [HttpPut("id")]
        public async Task<IActionResult> Update(int id, CategoryInsertRequestViewModel request)
        {
            return Ok(await _categoryService.UpdateCategory(id, request));
        }


        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _categoryService.DeleteCategory(id));
        }

    }
}
