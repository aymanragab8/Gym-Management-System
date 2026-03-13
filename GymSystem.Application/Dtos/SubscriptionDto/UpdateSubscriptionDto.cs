using GymSystem.Domain.Enums;

namespace GymSystem.Application.Dtos.SubscriptionDto
{
    public class UpdateSubscriptionDto
    {
        public decimal? Price { get; set; }
        public SubscriptionPeriod? SubscriptionType { get; set; }

    }
}
