using FluentValidation;
using ProductEFHW.Models.ViewModels;
using System.Text.RegularExpressions;

namespace ProductEFHW.Models.FluentValidators;

public class AddProductFluentValidator : AbstractValidator<AddProductViewModel>
{
    public AddProductFluentValidator()
    {
        RuleFor(p => p.Title)
            .NotEmpty().WithMessage("Required")
            .MinimumLength(2).WithMessage("Length must be longer than 2");

        RuleFor(p => p.Description)
            .NotEmpty().WithMessage("Required")
            .MinimumLength(15).WithMessage("Length must be longer than 15");

        RuleFor(p => p.Price)
            .NotEmpty().WithMessage("Required")
            .GreaterThan(0).WithMessage("Price must be greater than 0");

        RuleFor(p => p.ImageUrl)
            .NotEmpty().WithMessage("Required")
            .Must(IsValidImage!).WithMessage("The provided URL is not a valid image.");

        RuleFor(p => p.CategoryId)
            .NotEmpty().WithMessage("Required");

        RuleFor(p => p.TagIds)
            .NotEmpty().WithMessage("Required");
    }

    private bool IsValidImage(string url)
    {
        if (string.IsNullOrEmpty(url))
            return false;

        return Regex.IsMatch(url, @"(.*?).(jpg|jpeg|png|gif|bmp)$", RegexOptions.IgnoreCase);
    }
}
