using FluentValidation;
using GymSystem.Application.Dtos.MemberDto;

public class UpdateMemberValidator : AbstractValidator<UpdateMemberDto>
{
    public UpdateMemberValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Member data cannot be null.");

        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrEmpty(x.Email))
            .WithMessage("Email must be a valid email address.");

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\+?\d{7,15}$")
            .When(x => !string.IsNullOrEmpty(x.PhoneNumber))
            .WithMessage("Phone number is invalid.");

        RuleFor(x => x.Specialization)
            .MaximumLength(100)
            .When(x => !string.IsNullOrEmpty(x.Specialization))
            .WithMessage("Specialization can be at most 100 characters.");

        RuleFor(x => x.HourlyRate)
            .GreaterThanOrEqualTo(0)
            .When(x => x.HourlyRate.HasValue)
            .WithMessage("HourlyRate must be non-negative.");

        RuleFor(x => x.Status)
            .IsInEnum()
            .When(x => x.Status.HasValue)
            .WithMessage("Status is invalid.");

        RuleFor(x => x.MemberType)
            .IsInEnum()
            .When(x => x.MemberType.HasValue)
            .WithMessage("MemberType is invalid.");
    }
}