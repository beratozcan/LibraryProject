using System.Linq.Expressions;

namespace NLayer.Core.Services
{
    public interface IService<T> where T : class
    {
        Task<ICollection<T>> GetAllAsync(string token);
        Task<T> GetByIdAsync(int id, string token);
        IQueryable<T> WhereAsync(Expression<Func<T, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);

    }
}
