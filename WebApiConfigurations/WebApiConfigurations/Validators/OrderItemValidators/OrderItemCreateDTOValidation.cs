using FluentValidation;
using WebApiConfigurations.DTOs.OrderItemDTOs;

namespace WebApiConfigurations.Validators.OrderItemValidators
{
    public class OrderItemCreateDTOValidation: AbstractValidator<CreateOrderItemDTO>
    {
        public OrderItemCreateDTOValidation()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("Descrption is required")
               .NotNull().WithMessage("Can not be empty").MaximumLength(500).WithMessage("Description size can be maximum 500");
            RuleFor(x => x.Quantity).NotEmpty().WithMessage("Quantity is required").NotNull().WithMessage("Can not be empty");
            RuleFor(x => x.UnitPrice).NotEmpty().WithMessage("UnitPrice is required").NotNull().WithMessage("Can not be empty");
        }
    }
}
