using GymSystem.Domain.Enums;

namespace GymSystem.Application.Dtos.SubscriptionDto
{
    public class CreateSubscriptionDto
    {
        public decimal Price { get; set; }
        public int DurationInDays { get; set; }
        public DateTime StartDate { get; set; }
        public SubscriptionPeriod SubscriptionType { get; set; }
    }
}
