using FluentValidation;
using GymSystem.Application.Dtos.WorkoutPlan;

namespace GymSystem.Application.Validators.WorkoutPlan
{
    public class CreateWorkoutPlanValidator : AbstractValidator<CreateWorkoutPlanDto>
    {
        public CreateWorkoutPlanValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("WorkoutPlan data cannot be null.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Name is required and must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(300)
                .WithMessage("Description is required.");

        }
    }
}
