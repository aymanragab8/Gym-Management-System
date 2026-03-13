using GymSystem.Application.Abstracts;
using GymSystem.Domain.Entities;
using GymSystem.Infrastructure.GenericImplementation;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models.Data;

namespace GymSystem.Infrastructure.Repositories
{
    public class SubscriptionRepository : GenericRepository<Subscription>, ISubscriptionRepository
    {
        public SubscriptionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Subscription>> GetSubscriptionsAsync(int pageNumber, int pageSize)
        {
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            return await _dbContext.Subscriptions
                .Include(s => s.Member)
                .OrderBy(s => s.Price)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public override async Task<Subscription?> GetByIdAsync(int id)
        {
            return await _dbContext.Subscriptions
                .Include(s => s.Member)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> HasActiveSubscriptionAsync(int memberId)
        {
            var now = DateTime.UtcNow;
            return await _dbContext.Subscriptions
                .AnyAsync(x => x.MemberId == memberId && !x.IsDeleted);
        }
    }
}