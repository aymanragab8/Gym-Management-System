using FluentValidation;
using GymSystem.Application.Dtos.WorkoutPlan;

namespace GymSystem.Application.Validators.WorkoutPlan
{
    public class UpdateWorkoutPlanExerciseValidator : AbstractValidator<UpdateWorkoutPlanExerciseDto>
    {
        public UpdateWorkoutPlanExerciseValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("WorkoutPlan data cannot be null.");

            RuleFor(x => x.Sets)
                .GreaterThan(0)
                .WithMessage("Sets must be greater than 0.");
            RuleFor(x => x.Reps)
                .GreaterThan(0)
                .WithMessage("Reps must be greater than 0.");
        }
    }
}
