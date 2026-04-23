using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryManagementSystem.Services.Implementations
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : BaseModel
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet   = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
            => await _dbSet.Where(e => e.IsActive).AsNoTracking().ToListAsync();

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
            => await _dbSet.Where(e => e.IsActive).Where(predicate).AsNoTracking().ToListAsync();

        public async Task<TEntity?> GetByIdAsync(int id)
            => await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id && e.IsActive);

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            entity.IsActive   = true;
            entity.CreatedAt  = DateTime.UtcNow;
            entity.CreatedBy  = "system";
            entity.UpdatedAt  = DateTime.UtcNow;
            entity.UpdatedBy  = "system";
            entity.DeletedAt  = default;
            entity.DeletedBy  = string.Empty;

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity?> UpdateAsync(int id, Action<TEntity> updateAction)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(e => e.Id == id && e.IsActive);
            if (entity is null) return null;

            updateAction(entity);
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = "system";

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(e => e.Id == id && e.IsActive);
            if (entity is null) return false;

            entity.IsActive  = false;
            entity.DeletedAt = DateTime.UtcNow;
            entity.DeletedBy = "system";
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
            => await _dbSet.AnyAsync(e => e.Id == id && e.IsActive);

        public async Task<int> CountAsync()
            => await _dbSet.CountAsync(e => e.IsActive);
    }
}
