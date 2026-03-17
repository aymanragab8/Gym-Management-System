using GymSystem.Application.Abstracts;
using GymSystem.Domain.Entities;
using GymSystem.Infrastructure.GenericImplementation;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models.Data;

namespace GymSystem.Infrastructure.Repositories
{
    public class WorkoutPlanRepository : GenericRepository<WorkoutPlan>, IWorkoutPlanRepository
    {
        public WorkoutPlanRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<WorkoutPlan>> GetAllAsync(int pageNumber, int pageSize)
        {
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            return await _dbContext.WorkoutPlans
                .OrderBy(m => m.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<List<WorkoutPlan>> GetAllWorkoutPlanforMemberAsync(int memberId)
        {

            return await _dbContext.WorkoutPlans.Include(wp => wp.Member)
                .Where(wp => wp.MemberId == memberId)
                .OrderBy(m => m.Id)
                .ToListAsync();
        }
    }
}
