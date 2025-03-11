using FluentValidation;

namespace Phone.ViewModels.Validations;

public class CategoryCreateRequestValidation : AbstractValidator<CategoryCreateRequest>{
    public CategoryCreateRequestValidation()
    {
        RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Tên loại không được để trống");
        RuleFor(x => x.CategoryName).MaximumLength(50).WithMessage("Tên loại không được quá dài");
    }
}