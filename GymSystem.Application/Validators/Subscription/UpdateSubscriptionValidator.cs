using FluentValidation;
using GymSystem.Application.Dtos.SubscriptionDto;

namespace GymSystem.Application.Validators.Subscription
{
    public class UpdateSubscriptionValidator : AbstractValidator<UpdateSubscriptionDto>
    {
        public UpdateSubscriptionValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("Subscription data cannot be null.");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .When(x => x.Price.HasValue)
                .WithMessage("Price must be greater than zero.");

            RuleFor(x => x.SubscriptionType)
                .IsInEnum()
                .When(x => x.SubscriptionType.HasValue)
                .WithMessage("SubscriptionType is invalid.");
        }
    }
}
