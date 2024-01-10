using FluentValidation;
using ProductEFHW.Models.ViewModels;

namespace ProductEFHW.Models.FluentValidators;

public class AddCategoryFlluentValidator : AbstractValidator<AddCategoryViewModel>
{
    public AddCategoryFlluentValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Required")
            .MinimumLength(2).WithMessage("Length must be longer than 2");
    }
}