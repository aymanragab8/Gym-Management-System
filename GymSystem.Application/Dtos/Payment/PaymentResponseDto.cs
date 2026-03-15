using GymSystem.Domain.Enums;
using System.Text.Json.Serialization;

namespace GymSystem.Application.Dtos.Payment
{
    public class PaymentResponseDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool IsSuccessful { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentMethod PaymentMethod { get; set; }
        public string MemberName { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SubscriptionPeriod SubscriptionType { get; set; }
    }
}
