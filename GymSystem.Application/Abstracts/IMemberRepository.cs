using GymSystem.Application.Genaric;
using GymSystem.Domain.Entities;

namespace GymSystem.Infrastructure.Interfaces
{
    public interface IMemberRepository : IGenericRepository<Member>
    {
        Task<List<Member>> GetAllAsync(int pageNumber, int pageSize);
        Task<Member?> GetByApplicationUserIdAsync(string applicationUserId);
    }
}
