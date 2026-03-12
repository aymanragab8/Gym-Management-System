using GymSystem.Domain.Entities;
using GymSystem.Infrastructure.GenericImplementation;
using GymSystem.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models.Data;

namespace GymSystem.Infrastructure.Repositories
{
    public class MemberRepository : GenericRepository<Member>, IMemberRepository
    {
        public MemberRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Member>> GetAllAsync(int pageNumber, int pageSize)
        {
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            return await _dbContext.Members
                .OrderBy(m => m.FullName).Include(m => m.Subscriptions)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public override async Task<Member?> GetByIdAsync(int id)
        {
            return await _dbContext.Members
                .AsNoTracking()
                .Include(m => m.Subscriptions)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<Member?> GetByApplicationUserIdAsync(string applicationUserId)
        {
            return await _dbContext.Members
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ApplicationUserId == applicationUserId);
        }

    }
}