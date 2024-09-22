using FluentValidation;
using UniversityManagementSystem.BLL.ViewModel.Requests;

namespace UniversityManagementSystem.BLL.Validation
{
    public class CategoryInsertViewModelValidator : AbstractValidator<CategoryInsertRequestViewModel>
    {
        public CategoryInsertViewModelValidator()
        {
            RuleFor(category=>category.Name).NotNull().NotEmpty()
                .MaximumLength(25).MinimumLength(5);

            RuleFor(category=>category.ShortName).NotNull().NotEmpty()
                .MinimumLength(3).MaximumLength(10);
        }
    }
}
