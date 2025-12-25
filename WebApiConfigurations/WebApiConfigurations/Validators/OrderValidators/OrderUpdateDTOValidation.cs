using FluentValidation;
using WebApiConfigurations.DTOs.OrderDTOs;

namespace WebApiConfigurations.Validators.OrderValidators
{
    public class OrderUpdateDTOValidation: AbstractValidator<UpdateOrderDTO>
    {
        public OrderUpdateDTOValidation()
        {
            RuleFor(x => x.OrderDate).NotEmpty().WithMessage("OrderDate is required").LessThanOrEqualTo(DateTime.UtcNow);
            RuleFor(x => x.TotalAmount).NotEmpty().WithMessage("TotalAmount is required").NotNull().WithMessage("Can not be empty");
        }
    }
}
