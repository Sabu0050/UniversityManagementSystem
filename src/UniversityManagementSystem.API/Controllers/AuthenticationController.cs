using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.BLL.Service;
using UniversityManagementSystem.BLL.ViewModel.Requests;

namespace UniversityManagementSystem.API.Controllers
{
    public class AuthenticationController : ApiBaseController
    {
        private readonly IAuthenticateService _authenticateService;

        public AuthenticationController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationViewModel request) {

            return ToActionResult(await _authenticateService.RegistrationProcess(request));
        }
    }
}
