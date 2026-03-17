using AutoMapper;
using GymSystem.Application.Dtos.ExerciseDto;
using GymSystem.Domain.Entities;

namespace GymSystem.Application.DTOsMapping
{
    public class ExerciseProfile : Profile
    {
        public ExerciseProfile()
        {
            CreateMap<Exercise, ExersicesDataDto>();
            CreateMap<CreateExerciseDto, Exercise>();
        }
    }
}
