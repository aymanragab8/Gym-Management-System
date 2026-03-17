using FluentValidation;
using GymSystem.Application.Dtos.ExerciseDto;

namespace GymSystem.Application.Validators.Exercise
{
    public class CreateExerciseValidator : AbstractValidator<CreateExerciseDto>
    {
        public CreateExerciseValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("Exercise data cannot be null.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Name is required and must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(500)
                .WithMessage("Description is required.");
        }
    }
}
