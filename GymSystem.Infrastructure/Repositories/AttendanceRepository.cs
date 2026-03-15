using GymSystem.Application.Abstracts;
using GymSystem.Domain.Entities;
using GymSystem.Infrastructure.GenericImplementation;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models.Data;

namespace GymSystem.Infrastructure.Repositories
{
    public class AttendanceRepository : GenericRepository<Attendance>, IAttendanceRepository
    {
        public AttendanceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> ExistsAsync(int memberId, DateTime date)
        {
            return await _dbContext.Attendances
                .AnyAsync(x => x.MemberId == memberId && x.CreatedAt.Date == date);
        }

        public async Task<List<Attendance>> GetByMemberIdAsync(int memberId)
        {
            return await _dbContext.Attendances
                .Where(x => x.MemberId == memberId && !x.IsDeleted)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Attendance>> GetByDateAsync(DateTime date)
        {
            return await _dbContext.Attendances
                .Where(x => x.CreatedAt.Date == date.Date && !x.IsDeleted)
                .Include(x => x.Member)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }
        public async Task DeleteMemberAttendanceAsync(int memberId)
        {
            var records = await _dbContext.Attendances
                .Where(x => x.MemberId == memberId)
                .ToListAsync();

            foreach (var record in records)
                record.IsDeleted = true;

            await _dbContext.SaveChangesAsync();
        }
    }
}