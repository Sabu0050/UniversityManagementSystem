using Azure.Core;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.BLL.GenericResponseFormat;
using UniversityManagementSystem.BLL.Validation;
using UniversityManagementSystem.BLL.ViewModel.Requests;
using UniversityManagementSystem.DLL.Model;

namespace UniversityManagementSystem.BLL.Service
{
    public interface IAuthenticateService
    {
        Task<ApiResponse<string>> RegistrationProcess(RegistrationViewModel registrationViewModel);
    }

    public class AuthenticateService : IAuthenticateService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthenticateService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApiResponse<string>> RegistrationProcess(RegistrationViewModel request)
        {
            var validator = await new RegistrationViewModelValidator().ValidateAsync(request);
            if (!validator.IsValid)
            {
                return new ApiResponse<string>(validator.Errors);

            }
            var user = new ApplicationUser {
                Address = request.Address,
                PhoneNumber = request.MobileNumber,
                PhoneNumberConfirmed = true,
                UserName = request.MobileNumber

            };

            var userResult = await _userManager.CreateAsync(user,request.Password);
            if (!userResult.Succeeded) {
                return new ApiResponse<string>(userResult.Errors);
            }

            var roleAssign = await _userManager.AddToRoleAsync(user,"customer");

             return new ApiResponse<string>("Welcome to our system",true, "Welcome to our system");
        }
    }
}
