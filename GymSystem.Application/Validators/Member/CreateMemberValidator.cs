using FluentValidation;
using GymSystem.Application.Dtos.MemberDto;

namespace GymSystem.Application.Validators.Member
{
    public class CreateMemberValidator : AbstractValidator<CreateMemberDto>
    {
        public CreateMemberValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("Member data cannot be null.");

            RuleFor(x => x.FullName)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Full name is required and must not exceed 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("A valid email is required.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Matches(@"^[0-9]+$")
                .WithMessage("Phone number must contain only numbers.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty()
                .WithMessage("A valid Birthdate is required.")
                .Must(d => d <= DateTime.UtcNow && d > DateTime.UtcNow.AddYears(-120))
                .WithMessage("Date of birth must be a realistic date.");
        }
    }
}
