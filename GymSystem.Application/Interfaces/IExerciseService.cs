using GymSystem.Application.Dtos.ExerciseDto;

namespace GymSystem.Application.Interfaces
{
    public interface IExerciseService
    {
        Task<List<ExersicesDataDto>> GetAllExercisesAsync(int pageNumber, int pageSize);
        Task<string> AddExerciseAsync(CreateExerciseDto dto);
        Task<string> DeleteExerciseAsync(int exId);
    }
}
