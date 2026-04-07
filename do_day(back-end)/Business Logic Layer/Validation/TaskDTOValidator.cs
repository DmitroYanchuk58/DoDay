using FluentValidation;
using Business_Logic_Layer.DTO;

namespace Business_Logic_Layer.Validators
{
    public class TaskDTOValidator : AbstractValidator<TaskDTO>
    {
        public TaskDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Task ID is required");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Task name is required")
                .MinimumLength(1).WithMessage("Name cannot be empty")
                .MaximumLength(500).WithMessage("Task name cannot exceed 500 characters")
                .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Name cannot consist of whitespace only");

            RuleFor(x => x.DateCreated)
                .NotEmpty().WithMessage("Creation date is required")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Creation date cannot be in the future");

            RuleFor(x => x.Description)
                .MinimumLength(5).WithMessage("Task description must be at least 5 characters long")
                .MaximumLength(3000).WithMessage("Task description cannot exceed 3000 characters")
                .NotEmpty().WithMessage("Task description is required");

            RuleFor(x => x.Image)
                .Must(img => img.Length <= 2 * 1024 * 1024)
                .WithMessage("Image size cannot exceed 2 MB")
                .When(x => x.Image != null);
        }
    }
}