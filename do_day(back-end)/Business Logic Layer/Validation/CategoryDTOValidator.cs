using FluentValidation;
using Business_Logic_Layer.DTO;

namespace Business_Logic_Layer.Validators
{
    public class CategoryDTOValidator : AbstractValidator<CategoryDTO>
    {
        public CategoryDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("ID категорії не може бути порожнім");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Назва категорії обов'язкова")
                .MinimumLength(1).WithMessage("Назва має містити хоча б 1 символ")
                .MaximumLength(200).WithMessage("Назва не може бути довшою за 200 символів")
                .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Назва не може складатися лише з пробілів");
        }
    }
}