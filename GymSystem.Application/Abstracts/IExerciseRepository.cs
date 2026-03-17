using GymSystem.Application.Genaric;
using GymSystem.Domain.Entities;

namespace GymSystem.Application.Abstracts
{
    public interface IExerciseRepository : IGenericRepository<Exercise>
    {
        Task<List<Exercise>> GetAllAsync(int pageNumber, int pageSize);
    }
}
