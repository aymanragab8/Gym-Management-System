using GymSystem.Application.Genaric;
using GymSystem.Domain.Entities;

namespace GymSystem.Application.Abstracts
{
    public interface IAttendanceRepository : IGenericRepository<Attendance>
    {
        Task<bool> ExistsAsync(int memberId, DateTime date);
        Task<List<Attendance>> GetByMemberIdAsync(int memberId);
        Task<List<Attendance>> GetByDateAsync(DateTime date);
        Task DeleteMemberAttendanceAsync(int memberId);
    }
}