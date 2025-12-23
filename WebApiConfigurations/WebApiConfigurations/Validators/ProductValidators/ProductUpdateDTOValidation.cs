using FluentValidation;
using WebApiConfigurations.DTOs.ProductDTOs;

namespace WebApiConfigurations.Validators.ProductValidators
{
    public class ProductUpdateDTOValidation: AbstractValidator<UpdateProductDTO>
    {
        public ProductUpdateDTOValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required")
                .NotNull().WithMessage("Can not be empty").MaximumLength(100).WithMessage("Name size can be maximum 100");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Descrption is required")
                .NotNull().WithMessage("Can not be empty").MaximumLength(500).WithMessage("Description size can be maximum 500");
            RuleFor(x => x.Count).NotEmpty().WithMessage("Count is required").NotNull().WithMessage("Can not be empty");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price is required").NotNull().WithMessage("Can not be empty");
            RuleFor(x => x.Currency).NotEmpty().WithMessage("Currency is required")
                .NotNull().WithMessage("Can not be empty").MaximumLength(10).WithMessage("Currency size can be maximum 10");
        }
    }
}
