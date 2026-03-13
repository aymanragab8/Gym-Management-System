using GymSystem.Domain.Enums;
using System.Text.Json.Serialization;

namespace GymSystem.Application.Dtos.SubscriptionDto
{
    public class SubscriptionsDataDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SubscriptionPeriod SubscriptionType { get; set; }
        public string MemberName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
