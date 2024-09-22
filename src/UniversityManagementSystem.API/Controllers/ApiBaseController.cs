using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.BLL.GenericResponseFormat;

namespace UniversityManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ApiBaseController : ControllerBase
    {
        public IActionResult ToActionResult<T>(ApiResponse<T> response)
        {
            if (!response.IsSuccess) 
            {
                if (response.Errors != null)
                {
                    return UnprocessableEntity(response);
                }
                return BadRequest(response);
            }
            return Ok(response);
        }
         
    }
}


