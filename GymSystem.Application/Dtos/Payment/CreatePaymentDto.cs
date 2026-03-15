using GymSystem.Domain.Enums;

namespace GymSystem.Application.Dtos.Payment
{
    public class CreatePaymentDto
    {
        public int SubscriptionId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
