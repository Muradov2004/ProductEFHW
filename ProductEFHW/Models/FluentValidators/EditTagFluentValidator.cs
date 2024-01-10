using FluentValidation;
using ProductEFHW.Models.ViewModels;

namespace ProductEFHW.Models.FluentValidators;

public class EditTagFluentValidator : AbstractValidator<EditTagViewModel>
{
    public EditTagFluentValidator()
    {
        RuleFor(t => t.Name)
            .NotEmpty().WithMessage("Required")
            .MinimumLength(2).WithMessage("Length must be longer than 2");
    }
}
