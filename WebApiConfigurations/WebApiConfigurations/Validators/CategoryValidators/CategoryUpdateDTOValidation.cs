using FluentValidation;
using WebApiConfigurations.DTOs.CategoryDTOs;

namespace WebApiConfigurations.Validators.CategoryValidators
{
    public class CategoryUpdateDTOValidation: AbstractValidator<UpdateCategoryDTO>
    {
        public CategoryUpdateDTOValidation()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Name is required")
                .NotNull().WithMessage("Can not be empty").MaximumLength(100).WithMessage("Name size can be maximum 100");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Descrption is required")
                .NotNull().WithMessage("Can not be empty").MaximumLength(500).WithMessage("Description size can be maximum 500");
        }
    }
}
