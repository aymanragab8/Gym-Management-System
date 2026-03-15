using AutoMapper;
using GymSystem.Application.Dtos.Attendance;
using GymSystem.Domain.Entities;

namespace GymSystem.Application.DTOsMapping
{
    public class AttendanceProfile : Profile
    {
        public AttendanceProfile()
        {
            CreateMap<Attendance, AttendanceDataDto>();
        }
    }
}
