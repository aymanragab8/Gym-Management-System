using GymSystem.Application.Abstracts;
using GymSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models.Data;

namespace GymSystem.Infrastructure.Repositories
{
    public class WorkoutPlanExerciseRepository : IWorkoutPlanExerciseRepository
    {
        private readonly ApplicationDbContext _context;

        public WorkoutPlanExerciseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(WorkoutPlanExercise workoutPlanExercise)
        {
            await _context.WorkoutPlanExercises.AddAsync(workoutPlanExercise);
            await _context.SaveChangesAsync();
        }

        public async Task<WorkoutPlanExercise> GetAsync(int planId, int exerciseId)
        {
            return await _context.WorkoutPlanExercises
                .AsNoTracking()
                .Include(x => x.Exercise)
                .FirstOrDefaultAsync(x => x.WorkoutPlanId == planId && x.ExerciseId == exerciseId);
        }

        public async Task RemoveAsync(int planId, int exerciseId)
        {
            var entity = await _context.WorkoutPlanExercises
                .FirstOrDefaultAsync(x => x.WorkoutPlanId == planId && x.ExerciseId == exerciseId);

            _context.WorkoutPlanExercises.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<WorkoutPlanExercise>> GetExercisesByPlanIdAsync(int planId)
        {
            return await _context.WorkoutPlanExercises
                .Where(x => x.WorkoutPlanId == planId)
                .Include(x => x.Exercise)
                .ToListAsync();
        }

        public async Task UpdateAsync(WorkoutPlanExercise updatedEntity)
        {
            var existing = await _context.WorkoutPlanExercises
                .FirstOrDefaultAsync(x => x.WorkoutPlanId == updatedEntity.WorkoutPlanId
                                        && x.ExerciseId == updatedEntity.ExerciseId);

            if (existing is null)
                throw new KeyNotFoundException("WorkoutPlanExercise not found.");

            existing.Sets = updatedEntity.Sets;
            existing.Reps = updatedEntity.Reps;

            await _context.SaveChangesAsync();
        }
    }
}