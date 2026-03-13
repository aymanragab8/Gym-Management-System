using GymSystem.Application.Genaric;
using GymSystem.Domain.Entities;

namespace GymSystem.Application.Abstracts
{
    public interface ISubscriptionRepository : IGenericRepository<Subscription>
    {
        Task<List<Subscription>> GetSubscriptionsAsync(int pageNumber, int pageSize);
        Task<bool> HasActiveSubscriptionAsync(int memberId);
    }
}
