using GymSystem.Domain.Enums;

namespace GymSystem.Domain.Entities
{
    public class Subscription : BaseEntity
    {
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }

        public SubscriptionPeriod SubscriptionType { get; set; }
        public int MemberId { get; set; }
        public Member? Member { get; set; }
        public ICollection<Payment>? Payments { get; set; }
    }
}
