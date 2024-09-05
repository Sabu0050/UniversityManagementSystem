using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.BLL.Service;

namespace UniversityManagementSystem.API.Controllers
{
    public class ISeedController : ApiBaseController
    {
        private readonly ISeedService _seedService;

        public ISeedController(ISeedService seedService)
        {
            _seedService = seedService;
        }

        [HttpGet("test-seed-data")]
        public async Task<IActionResult> TestSeedData()
        {
            await _seedService.GenerateSeedAsync();
            return Ok("seed data");
        }
    }
}
