using GymSystem.Domain.Entities;

namespace GymSystem.Application.Genaric
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task DeleteRangeAsync(ICollection<T> entities);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task SaveChangesAsync();
        IQueryable<T> GetTableNoTracking();
        IQueryable<T> GetTableAsTracking();
        Task AddAsync(T entity);
        Task AddRangeAsync(ICollection<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(ICollection<T> entities);
        Task DeleteAsync(T entity);
    }
}
