using LibraryManagementSystem.Models;
using System.Linq.Expressions;

namespace LibraryManagementSystem.Services.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseModel
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity?> GetByIdAsync(int id);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity?> UpdateAsync(int id, Action<TEntity> updateAction);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<int> CountAsync();
    }
}
