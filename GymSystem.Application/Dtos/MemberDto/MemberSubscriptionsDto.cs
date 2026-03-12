using GymSystem.Application.Dtos.SubscriptionDto;

namespace GymSystem.Application.Dtos.MemberDto
{
    public class MemberSubscriptionsDto
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; } = string.Empty;
        public List<SubscriptionsDataDto> Subscriptions { get; set; } = new();
    }
}
