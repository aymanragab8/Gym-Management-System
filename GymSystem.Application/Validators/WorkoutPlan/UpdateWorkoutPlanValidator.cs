using FluentValidation;
using GymSystem.Application.Dtos.WorkoutPlan;

namespace GymSystem.Application.Validators.WorkoutPlan
{
    public class UpdateWorkoutPlanValidator : AbstractValidator<UpdateWorkoutPlanDto>
    {
        public UpdateWorkoutPlanValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("WorkoutPlan data cannot be null.");

            RuleFor(x => x.Name)
                .MaximumLength(100)
                .WithMessage("Name is required and must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(300)
                .WithMessage("Description is required.");
        }
    }
}
