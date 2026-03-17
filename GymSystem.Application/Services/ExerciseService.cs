using AutoMapper;
using GymSystem.Application.Abstracts;
using GymSystem.Application.Dtos.ExerciseDto;
using GymSystem.Application.Interfaces;
using GymSystem.Domain.Entities;

namespace GymSystem.Application.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IMapper _mapper;

        public ExerciseService(IExerciseRepository exerciseRepository, IMapper mapper)
        {
            _exerciseRepository = exerciseRepository;
            _mapper = mapper;
        }

        public async Task<string> AddExerciseAsync(CreateExerciseDto dto)
        {
            var exer = _mapper.Map<Exercise>(dto);
            await _exerciseRepository.AddAsync(exer);
            return $"Exercise {exer.Name} created successfully with Id: {exer.Id}.";
        }

        public async Task<string> DeleteExerciseAsync(int exId)
        {
            if (exId <= 0)
                throw new ArgumentException("ExerciseId must be greater than 0.");

            var exer = await _exerciseRepository.GetByIdAsync(exId);

            if (exer == null)
                throw new KeyNotFoundException($"Exercise with id {exId} not found.");

            await _exerciseRepository.DeleteAsync(exer);

            return $"Exercise {exer.Name} deleted successfully with Id: {exer.Id}.";

        }

        public async Task<List<ExersicesDataDto>> GetAllExercisesAsync(int pageNumber, int pageSize)
        {
            var exercises = await _exerciseRepository.GetAllAsync(pageNumber, pageSize);
            if (!exercises.Any())
                return new List<ExersicesDataDto>();
            return _mapper.Map<List<ExersicesDataDto>>(exercises);
        }

    }
}
