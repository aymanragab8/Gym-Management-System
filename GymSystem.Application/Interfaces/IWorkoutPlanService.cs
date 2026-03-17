using GymSystem.Application.Dtos.WorkoutPlan;

namespace GymSystem.Application.Interfaces
{
    public interface IWorkoutPlanService
    {
        Task<List<WorkoutPlanDataDto>> GetAllAsync(int pageNumber, int pageSize);
        Task<List<WorkoutPlanDataDto>> GetAllWorkoutPlanforMemberAsync(int memberId, string currentUserId, string role);
        Task<string> CreateWorkoutPlanAsync(int memberId, CreateWorkoutPlanDto dto);
        Task<string> UpdateWorkoutPlanAsync(int workplanId, UpdateWorkoutPlanDto dto);
        Task<string> DeleteWorkoutPlanAsync(int workplanId);

        Task<string> AddExerciseToWorkoutPlanAsync(int workplanId, int exerciseId, CreateExerciseToWorkoutPlanDto dto);
        Task<string> RemoveExerciseFromWorkoutPlanAsync(int workplanId, int exerciseId);
        Task<List<WorkoutPlanExerciseDataDto>> GetExercisesByWorkoutPlanIdAsync(int workplanId, string currentUserId, string role);
        Task<string> UpdateWorkoutPlanExerciseAsync(int workplanId, int exerciseId, UpdateWorkoutPlanExerciseDto dto);

    }
}
