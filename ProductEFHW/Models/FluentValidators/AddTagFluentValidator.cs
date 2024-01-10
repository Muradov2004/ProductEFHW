using FluentValidation;
using ProductEFHW.Models.ViewModels;

namespace ProductEFHW.Models.FluentValidators;

public class AddTagFluentValidator : AbstractValidator<AddTagViewModel>
{
    public AddTagFluentValidator()
    {
        RuleFor(t => t.Name)
            .NotEmpty().WithMessage("Required")
            .MinimumLength(2).WithMessage("Length must be longer than 2");
    }
}
