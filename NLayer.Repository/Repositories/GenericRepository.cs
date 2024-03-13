using Microsoft.EntityFrameworkCore;
using NLayer.Core.Entities;
using NLayer.Core.Repositories;
using NLayer.Service.Exceptions;
using System.Linq.Expressions;

namespace NLayer.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly IUserTokenRepository _userTokenRepository;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public Task AddRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<ICollection<T>> GetAllAsync(string token)
        {
            
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id, string token)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity != null)
            {
                
                return entity;
            }
            else
            {
                throw new NotFoundException($"{typeof(T).Name} not found with ID: {id}");
            }
        }

        public virtual  void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (entity is ISoftDeletable softDeletableEntity)
            {
                softDeletableEntity.IsDeleted = true;
            }
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            
            _dbSet.Update(entity);
        }

        public IQueryable<T> WhereAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
