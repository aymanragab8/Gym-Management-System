using FluentValidation;
using GymSystem.Application.Dtos.SubscriptionDto;

public class CtreateSubscriptionValidator : AbstractValidator<CreateSubscriptionDto>
{
    public CtreateSubscriptionValidator()
    {
        RuleFor(x => x)
             .NotNull()
             .WithMessage("Subscription data cannot be null.");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than zero.");

        RuleFor(x => x.DurationInDays)
            .GreaterThan(0)
            .WithMessage("Duration must be greater than zero days.");

        RuleFor(x => x.StartDate)
            .GreaterThanOrEqualTo(DateTime.Today)
            .WithMessage("StartDate cannot be in the past.");

        RuleFor(x => x.SubscriptionType)
            .IsInEnum()
            .WithMessage("SubscriptionType is invalid.");
    }
}