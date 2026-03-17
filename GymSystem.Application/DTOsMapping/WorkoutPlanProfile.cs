using AutoMapper;
using GymSystem.Application.Dtos.WorkoutPlan;
using GymSystem.Domain.Entities;

namespace GymSystem.Application.DTOsMapping
{
    public class WorkoutPlanProfile : Profile
    {
        public WorkoutPlanProfile()
        {
            CreateMap<WorkoutPlan, WorkoutPlanDataDto>()
                .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member.FullName));
            CreateMap<CreateWorkoutPlanDto, WorkoutPlan>();
            CreateMap<UpdateWorkoutPlanDto, WorkoutPlan>();

            CreateMap<CreateExerciseToWorkoutPlanDto, WorkoutPlanExercise>();
            CreateMap<UpdateWorkoutPlanExerciseDto, WorkoutPlanExercise>();
            CreateMap<WorkoutPlanExercise, WorkoutPlanExerciseDataDto>();




        }
    }
}
