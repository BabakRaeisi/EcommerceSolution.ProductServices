using BusinessLogicLayer.DTO;
using FluentValidation;
using FluentValidation.Validators;
 

namespace BusinessLogicLayer.Validators;
 
public class ProductAddRequestValidator:AbstractValidator<ProductAddRequest>
{
    public ProductAddRequestValidator()
    {
        RuleFor(x => x.ProductName)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");
        RuleFor(x=>x.Category)
            .IsInEnum().WithMessage("Category must be a valid enum value.");
        RuleFor(x => x.UnitPrice)
            .InclusiveBetween(0, double.MaxValue).WithMessage($"Unit Price should be between 0 ro {double.MaxValue}");
        RuleFor(x => x.QuantityInStock)
            .InclusiveBetween(0, int.MaxValue).WithMessage($"Qauntity should be between 0 ro {int.MaxValue}");

    }

}
 