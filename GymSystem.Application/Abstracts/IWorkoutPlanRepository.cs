using GymSystem.Application.Genaric;
using GymSystem.Domain.Entities;

namespace GymSystem.Application.Abstracts
{
    public interface IWorkoutPlanRepository : IGenericRepository<WorkoutPlan>
    {
        Task<List<WorkoutPlan>> GetAllAsync(int pageNumber, int pageSize);
        Task<List<WorkoutPlan>> GetAllWorkoutPlanforMemberAsync(int memberId);
    }
}
