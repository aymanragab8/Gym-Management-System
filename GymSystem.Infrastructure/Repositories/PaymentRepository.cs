using GymSystem.Application.Abstracts;
using GymSystem.Domain.Entities;
using GymSystem.Infrastructure.GenericImplementation;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models.Data;

namespace GymSystem.Infrastructure.Repositories
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Payment>> GetAllPaymentsAsync(int pageNumber, int pageSize)
        {
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            return await _dbContext.Payments.Include(m => m.Member).ThenInclude(m => m.Subscriptions)
                .OrderBy(m => m.Id).Include(m => m.Member)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<Payment>> GetByMemberIdAsync(int memberId)
        {
            return await _dbContext.Payments.Include(m => m.Member).ThenInclude(m => m.Subscriptions)
                    .Where(m => m.MemberId == memberId)
                    .ToListAsync();
        }
        public async Task<bool> ExistsAsync(int memberId, int subscriptionId)
        {
            return await _dbContext.Payments
                .AnyAsync(x => x.MemberId == memberId
                            && x.SubscriptionId == subscriptionId
                            && x.IsSuccessful);
        }
    }
}
