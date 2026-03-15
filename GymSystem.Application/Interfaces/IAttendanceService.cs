using GymSystem.Application.Dtos.Attendance;

namespace GymSystem.Application.Interfaces
{
    public interface IAttendanceService
    {
        Task<string> CheckIn(string currentUserId);
        Task<List<AttendanceDataDto>> MemberAttendance(int memberId);
        Task<List<AttendanceDataDto>> MemberAttendanceByDate(DateTime date);
        Task<string> DeleteMmberAttendance(int memberId);


    }
}
