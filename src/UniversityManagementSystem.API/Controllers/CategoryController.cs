using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return ToActionResult(await _categoryService.GetAll());
        }
        [Authorize]
        [HttpGet("id")]
        public async Task<IActionResult> GetAData(int id)
        {
            var result = await _categoryService.GetAData(id);
            return Ok(result);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Insert(CategoryInsertRequestViewModel request)
        {
            var response = await _categoryService.AddCategory(request);
            return ToActionResult(response);
        }
        [Authorize]
        [HttpPut("id")]
        public async Task<IActionResult> Update(int id, CategoryInsertRequestViewModel request)
        {
            return Ok(await _categoryService.UpdateCategory(id, request));
        }

        [Authorize]
        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _categoryService.DeleteCategory(id));
        }

    }
}
