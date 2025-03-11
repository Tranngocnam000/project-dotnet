using FluentValidation;

namespace Phone.ViewModels.Validations;

public class MobileEditRequestValidation : AbstractValidator<MobileEditRequest>{
    public MobileEditRequestValidation()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Tên sản phẩm không được để trống");
        RuleFor(x => x.Name).MaximumLength(50).WithMessage("Tên sản phẩm không được quá dài");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Mô tả sản phẩm không được để trống");
        RuleFor(x => x.Description).MaximumLength(50).WithMessage("Mô tả sản phẩm không được quá dài");
        RuleFor(x => x.Price).NotEmpty().WithMessage("Giá sản phẩm không được để trống");
        RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Loại sản phẩm không được để trống");
    }
}