using AutoMapper;
using GymSystem.Application.Abstracts;
using GymSystem.Application.Dtos.WorkoutPlan;
using GymSystem.Application.Interfaces;
using GymSystem.Domain.Entities;
using GymSystem.Infrastructure.Interfaces;
using WorkoutPlan = GymSystem.Domain.Entities.WorkoutPlan;

namespace GymSystem.Application.Services
{
    public class WorkoutPlanService : IWorkoutPlanService
    {
        private readonly IWorkoutPlanRepository _workoutPlanRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IWorkoutPlanExerciseRepository _workoutPlanExerciseRepository;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IMapper _mapper;

        public WorkoutPlanService(IWorkoutPlanRepository workoutPlanRepository,
            IMemberRepository memberRepository,
            IWorkoutPlanExerciseRepository workoutPlanExerciseRepository,
            IExerciseRepository exerciseRepository,
            IMapper mapper)
        {
            _workoutPlanRepository = workoutPlanRepository;
            _memberRepository = memberRepository;
            _workoutPlanExerciseRepository = workoutPlanExerciseRepository;
            _exerciseRepository = exerciseRepository;
            _mapper = mapper;
        }

        public async Task<string> AddExerciseToWorkoutPlanAsync(int workplanId, int exerciseId, CreateExerciseToWorkoutPlanDto dto)
        {
            if (workplanId <= 0 || exerciseId <= 0)
                throw new ArgumentException("Enter a positive integer value.");
            var workplan = await _workoutPlanRepository.GetByIdAsync(workplanId);
            if (workplan == null)
                throw new KeyNotFoundException("WorkoutPlan not found");
            var exercise = await _exerciseRepository.GetByIdAsync(exerciseId);
            if (exercise == null)
                throw new KeyNotFoundException("Exercise not found");

            var newWorkoutPlanExercise = _mapper.Map<WorkoutPlanExercise>(dto);
            newWorkoutPlanExercise.WorkoutPlanId = workplanId;
            newWorkoutPlanExercise.ExerciseId = exerciseId;

            await _workoutPlanExerciseRepository.AddAsync(newWorkoutPlanExercise);
            return $"Exercise {exercise.Name} added successfully to WorkoutPlan with Id: {workplanId}.";
        }

        public async Task<string> CreateWorkoutPlanAsync(int memberId, CreateWorkoutPlanDto dto)
        {
            if (memberId <= 0)
                throw new ArgumentException("MemberId must be greater than 0.");

            var member = await _memberRepository.GetByIdAsync(memberId);

            if (member == null)
                throw new KeyNotFoundException($"Member with id {memberId} not found.");

            var newWoP = _mapper.Map<WorkoutPlan>(dto);
            newWoP.MemberId = memberId;

            await _workoutPlanRepository.AddAsync(newWoP);

            return $"WorkoutPlan {newWoP.Name} added successfully with Id: {newWoP.Id}.";


        }

        public async Task<string> DeleteWorkoutPlanAsync(int workplanId)
        {
            if (workplanId <= 0)
                throw new ArgumentException("Enter a positive integer value.");
            var workplan = await _workoutPlanRepository.GetByIdAsync(workplanId);
            if (workplan == null)
                throw new KeyNotFoundException("WorkoutPlan not found");
            await _workoutPlanRepository.DeleteAsync(workplan);
            return $"WorkoutPlan with Id: {workplanId} deleted successfully.";
        }

        public async Task<List<WorkoutPlanDataDto>> GetAllAsync(int pageNumber, int pageSize)
        {
            var workoutPlans = await _workoutPlanRepository.GetAllAsync(pageNumber, pageSize);
            if (workoutPlans == null)
                return new List<WorkoutPlanDataDto>();
            return _mapper.Map<List<WorkoutPlanDataDto>>(workoutPlans);
        }

        public async Task<List<WorkoutPlanDataDto>> GetAllWorkoutPlanforMemberAsync(int memberId, string currentUserId, string role)
        {
            if (memberId <= 0)
                throw new ArgumentException("Enter a positive integer value.");

            var member = await _memberRepository.GetByIdAsync(memberId);
            if (member == null)
                throw new KeyNotFoundException("Member not found.");

            switch (role)
            {
                case "Member":
                    if (member.ApplicationUserId != currentUserId)
                        throw new UnauthorizedAccessException("You can only view your own workout plans.");
                    break;

                case "Coach":
                    var coach = await _memberRepository.GetByApplicationUserIdAsync(currentUserId);
                    if (coach == null)
                        throw new KeyNotFoundException("Coach not found.");

                    if (member.CoachId != coach.Id)
                        throw new UnauthorizedAccessException("You can only view workout plans of your assigned members.");
                    break;

                default:
                    throw new UnauthorizedAccessException("Unknown role.");
            }

            var workoutPlans = await _workoutPlanRepository.GetAllWorkoutPlanforMemberAsync(memberId);
            if (!workoutPlans.Any())
                return new List<WorkoutPlanDataDto>();

            return _mapper.Map<List<WorkoutPlanDataDto>>(workoutPlans);
        }

        public async Task<List<WorkoutPlanExerciseDataDto>> GetExercisesByWorkoutPlanIdAsync(int workplanId, string currentUserId, string role)
        {
            if (workplanId <= 0)
                throw new ArgumentException("Enter a positive integer value.");

            var workoutPlan = await _workoutPlanRepository.GetByIdAsync(workplanId);
            if (workoutPlan == null)
                throw new KeyNotFoundException($"WorkoutPlan with Id {workplanId} not found.");

            switch (role)
            {
                case "Member":
                    var member = await _memberRepository.GetByApplicationUserIdAsync(currentUserId);
                    if (member == null)
                        throw new KeyNotFoundException("Member not found.");

                    if (workoutPlan.MemberId != member.Id)
                        throw new UnauthorizedAccessException("You can only view your own workout plans.");
                    break;

                case "Coach":
                    var coach = await _memberRepository.GetByApplicationUserIdAsync(currentUserId);
                    if (coach == null)
                        throw new KeyNotFoundException("Coach not found.");

                    var planMember = await _memberRepository.GetByIdAsync(workoutPlan.MemberId);
                    if (planMember == null || planMember.CoachId != coach.Id)
                        throw new UnauthorizedAccessException("You can only view workout plans of your assigned members.");
                    break;

                default:
                    throw new UnauthorizedAccessException("Unknown role.");
            }

            var exercises = await _workoutPlanExerciseRepository.GetExercisesByPlanIdAsync(workplanId);
            if (!exercises.Any())
                return new List<WorkoutPlanExerciseDataDto>();

            return _mapper.Map<List<WorkoutPlanExerciseDataDto>>(exercises);
        }

        public async Task<string> RemoveExerciseFromWorkoutPlanAsync(int workplanId, int exerciseId)
        {
            if (workplanId <= 0 || exerciseId <= 0)
                throw new ArgumentException("Enter a positive integer value.");
            var workoutPlan = await _workoutPlanRepository.GetByIdAsync(workplanId);
            if (workoutPlan == null)
                throw new KeyNotFoundException($"WorkoutPlan with Id {workplanId} not found.");
            var exercise = await _exerciseRepository.GetByIdAsync(exerciseId);
            if (exercise == null)
                throw new KeyNotFoundException($"Exercise with Id {exerciseId} not found.");
            await _workoutPlanExerciseRepository.RemoveAsync(workplanId, exerciseId);
            return $"Exercise with Id: {exerciseId} removed successfully from WorkoutPlan with Id: {workplanId}.";
        }

        public async Task<string> UpdateWorkoutPlanAsync(int workplanId, UpdateWorkoutPlanDto dto)
        {
            if (workplanId <= 0)
                throw new ArgumentException("Enter a positive integer value.");
            var workplan = await _workoutPlanRepository.GetByIdAsync(workplanId);
            if (workplan == null)
                throw new KeyNotFoundException("WorkoutPlan not found");
            _mapper.Map<WorkoutPlan>(workplan);
            await _workoutPlanRepository.UpdateAsync(workplan);
            return $"WorkoutPlan {workplan.Name} updated successfully with Id: {workplan.Id}.";
        }

        public async Task<string> UpdateWorkoutPlanExerciseAsync(int workoutPlanId, int exerciseId, UpdateWorkoutPlanExerciseDto dto)
        {
            if (workoutPlanId <= 0 || exerciseId <= 0)
                throw new ArgumentException("Ids must be positive integers.");

            var workoutPlan = await _workoutPlanRepository.GetByIdAsync(workoutPlanId);
            if (workoutPlan == null)
                throw new KeyNotFoundException($"WorkoutPlan with Id {workoutPlanId} not found.");

            var exercise = await _exerciseRepository.GetByIdAsync(exerciseId);
            if (exercise == null)
                throw new KeyNotFoundException($"Exercise with Id {exerciseId} not found.");

            var existingEntity = await _workoutPlanExerciseRepository.GetAsync(workoutPlanId, exerciseId);
            if (existingEntity == null)
                throw new KeyNotFoundException($"Exercise {exerciseId} not found in WorkoutPlan {workoutPlanId}.");

            existingEntity.Sets = dto.Sets ?? existingEntity.Sets;
            existingEntity.Reps = dto.Reps ?? existingEntity.Reps;

            await _workoutPlanExerciseRepository.UpdateAsync(existingEntity);

            return $"Exercise {exercise.Name} updated successfully in WorkoutPlan with Id: {workoutPlanId}.";
        }
    }
}
