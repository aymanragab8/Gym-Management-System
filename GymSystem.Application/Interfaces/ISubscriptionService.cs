using GymSystem.Application.Dtos.SubscriptionDto;

namespace GymSystem.Application.Interfaces
{
    public interface ISubscriptionService
    {
        Task<List<SubscriptionsDataDto>> GetAllSubscriptionsAsync(int pageNumber, int pageSize);
        Task<SubscriptionsDataDto> GetSubscriptionByIdAsync(int id);
        Task<string> AddSubscriptionAsync(int memberId, CreateSubscriptionDto dto);
        Task<string> UpdateSubscriptionAsync(int subId, UpdateSubscriptionDto dto);
        Task<string> DeleteSubscriptionAsync(int subId);
    }
}
