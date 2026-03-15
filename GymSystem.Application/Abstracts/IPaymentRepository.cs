using GymSystem.Application.Genaric;
using GymSystem.Domain.Entities;

namespace GymSystem.Application.Abstracts
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        Task<List<Payment>> GetByMemberIdAsync(int memberId);
        Task<List<Payment>> GetAllPaymentsAsync(int pageNumber, int pageSize);
        Task<bool> ExistsAsync(int memberId, int subscriptionId);
    }
}
