
using GymSystem.Domain.Enums;

namespace GymSystem.Domain.Entities
{
    public class Payment :BaseEntity
    {
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        public PaymentMethod PaymentMethod { get; set; } 
        public bool IsSuccessful { get; set; }

        public int MemberId { get; set; }
        public Member? Member { get; set; }
        public int SubscriptionId { get; set; }
        public Subscription? Subscription { get; set; }
    }
}
