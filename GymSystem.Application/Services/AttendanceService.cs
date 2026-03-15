using AutoMapper;
using GymSystem.Application.Abstracts;
using GymSystem.Application.Dtos.Attendance;
using GymSystem.Application.Interfaces;
using GymSystem.Domain.Entities;
using GymSystem.Infrastructure.Interfaces;

namespace GymSystem.Application.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendenceRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public AttendanceService(IAttendanceRepository attendenceRepository, IMemberRepository memberRepository, IMapper mapper)
        {
            _attendenceRepository = attendenceRepository;
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task<string> CheckIn(string currentUserId)
        {
            var member = await _memberRepository.GetByApplicationUserIdAsync(currentUserId);
            if (member == null)
                throw new KeyNotFoundException($"Member not found.");

            var alreadyCheckedIn = await _attendenceRepository
                .ExistsAsync(member.Id, DateTime.UtcNow.Date);

            if (alreadyCheckedIn)
                throw new InvalidOperationException("Member already checked in today.");

            var attendance = new Attendance
            {
                MemberId = member.Id,
            };

            await _attendenceRepository.AddAsync(attendance);
            return $"Member {member.FullName} checked in successfully.";
        }

        public async Task<string> DeleteMmberAttendance(int memberId)
        {
            if (memberId <= 0)
                throw new ArgumentException("Member Id must be greater than zero.");
            var att = await _memberRepository.GetByIdAsync(memberId);
            if (att == null)
                throw new KeyNotFoundException($"Member with Id {memberId} not found.");
            await _attendenceRepository.DeleteMemberAttendanceAsync(memberId);
            return $"Attendance record for Member Id {memberId} deleted successfully.";
        }

        public async Task<List<AttendanceDataDto>> MemberAttendance(int memberId)
        {
            if (memberId <= 0)
                throw new ArgumentException("Member Id must be greater than zero.");
            var member = await _memberRepository.GetByIdAsync(memberId);
            if (member == null)
                throw new KeyNotFoundException($"Member with Id {memberId} not found.");
            var memAtt = await _attendenceRepository.GetByMemberIdAsync(memberId);
            if (!memAtt.Any())
                return new List<AttendanceDataDto>();
            return _mapper.Map<List<AttendanceDataDto>>(memAtt);
        }
        public async Task<List<AttendanceDataDto>> MemberAttendanceByDate(DateTime date)
        {
            if (date == null)
                throw new ArgumentException("Date cannot be null.");
            var memAtt = await _attendenceRepository.GetByDateAsync(date);
            if (!memAtt.Any())
                return new List<AttendanceDataDto>();
            return _mapper.Map<List<AttendanceDataDto>>(memAtt);
        }
    }
}