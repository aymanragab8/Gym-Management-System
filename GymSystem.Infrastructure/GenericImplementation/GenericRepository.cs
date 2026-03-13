using GymSystem.Application.Genaric;
using GymSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models.Data;

namespace GymSystem.Infrastructure.GenericImplementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<T> GetTableNoTracking()
        {
            return _dbContext.Set<T>()
                .AsNoTracking()
                .AsQueryable();
        }

        public IQueryable<T> GetTableAsTracking()
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task AddAsync(T entity)
        {
            entity.CreatedAt = DateTime.UtcNow;

            await _dbContext.Set<T>().AddAsync(entity);

            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task AddRangeAsync(ICollection<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreatedAt = DateTime.UtcNow;
            }

            await _dbContext.Set<T>().AddRangeAsync(entities);

            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;

            _dbContext.Set<T>().Update(entity);

            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task UpdateRangeAsync(ICollection<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.UpdatedAt = DateTime.UtcNow;
            }

            _dbContext.Set<T>().UpdateRange(entities);

            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            entity.IsDeleted = true;

            _dbContext.Set<T>().Update(entity);

            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteRangeAsync(ICollection<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
            }

            _dbContext.Set<T>().UpdateRange(entities);

            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}