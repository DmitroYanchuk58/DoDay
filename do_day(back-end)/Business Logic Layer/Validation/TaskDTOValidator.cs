using FluentValidation;
using Business_Logic_Layer.DTO;

namespace Business_Logic_Layer.Validators
{
    public class TaskDTOValidator : AbstractValidator<TaskDTO>
    {
        public TaskDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("ID завдання обов'язкове");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Назва завдання обов'язкова")
                .MinimumLength(1).WithMessage("Назва не може бути порожньою")
                .MaximumLength(500).WithMessage("Назва завдання не може перевищувати 500 символів")
                .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Назва не може складатися лише з пробілів");

            RuleFor(x => x.DateCreated)
                .NotEmpty().WithMessage("Дата створення обов'язкова")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Дата створення не може бути в майбутньому");

            RuleFor(x => x.Description)
                .MinimumLength(5).WithMessage("Опис завдання не може бути меншим за 5 символів")
                .MaximumLength(3000).WithMessage("Опис завдання не може перевищувати 3000 символів")
                .When(x => x.Description != null);

            RuleFor(x => x.Image)
                .Must(img => img.Length <= 2 * 1024 * 1024)
                .WithMessage("Розмір зображення не може перевищувати 2 МБ")
                .When(x => x.Image != null);
        }
    }
}