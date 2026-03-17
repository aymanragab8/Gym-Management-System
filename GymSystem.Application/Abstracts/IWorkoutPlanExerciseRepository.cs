using GymSystem.Domain.Entities;

namespace GymSystem.Application.Abstracts
{
    public interface IWorkoutPlanExerciseRepository
    {
        Task AddAsync(WorkoutPlanExercise entity);

        Task<WorkoutPlanExercise?> GetAsync(int planId, int exerciseId);

        Task RemoveAsync(int planId, int exerciseId);

        Task<List<WorkoutPlanExercise>> GetExercisesByPlanIdAsync(int planId);

        Task UpdateAsync(WorkoutPlanExercise updatedEntity);
    }
}