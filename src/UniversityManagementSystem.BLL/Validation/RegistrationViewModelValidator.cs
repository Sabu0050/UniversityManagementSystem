using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.BLL.ViewModel.Requests;

namespace UniversityManagementSystem.BLL.Validation
{
    public class RegistrationViewModelValidator : AbstractValidator<RegistrationViewModel>
    {
        public RegistrationViewModelValidator()
        {
            RuleFor(user => user.MobileNumber).NotNull().NotEmpty()
                .MaximumLength(11).MinimumLength(11)
                .Matches(@"^01[3-9]\d{8}$").WithMessage("Not a vaild BD mobile number");

            RuleFor(user => user.Address).NotNull().NotEmpty();
                
            RuleFor(user => user.Password).NotNull().NotEmpty()
                .MaximumLength(5).MinimumLength(5)
                .Matches(@"^\d{5}$").WithMessage("Password 5 digit only");

           
        }
    }
}
