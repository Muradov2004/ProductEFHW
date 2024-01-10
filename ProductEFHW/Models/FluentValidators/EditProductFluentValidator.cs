using FluentValidation;
using ProductEFHW.Models.ViewModels;

namespace ProductEFHW.Models.FluentValidators;

public class EditProductFluentValidator : AbstractValidator<EditProductViewModel>
{
    public EditProductFluentValidator()
    {
        
    }
}