using Clinical.Application.Interfaces.GenericDataService;
using Clinical.Domain.Commons.CommonEntities;
using Clinical.Persistence.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Clinical.Infrastructure.GenericDataService
{
    public class GenericDataService : IGenericDataService
    {
        private readonly ClinicalDbContext _context;

        public GenericDataService(ClinicalDbContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync<T>(int id) where T : BaseEntity
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync<T>(bool withTracking = true) where T : BaseEntity
        {
            var query = _context.Set<T>().AsQueryable();
            if (!withTracking) query = query.AsNoTracking();
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync<T>(
            Expression<Func<T, bool>> condition,
            bool withTracking = true
        ) where T : BaseEntity
        {
            var query = _context.Set<T>().Where(condition);
            if (!withTracking) query = query.AsNoTracking();
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync<T>(
            Expression<Func<T, bool>> condition,
            IList<Expression<Func<T, object?>>> includeExpressions,
            bool withTracking = true
        ) where T : BaseEntity
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var include in includeExpressions)
            {
                query = query.Include(include);
            }

            if (!withTracking)
                query = query.AsNoTracking();

            return await query.Where(condition).ToListAsync();
        }

        public async Task<T> AddAsync<T>(T entity) where T : BaseEntity
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public Task<T> UpdateAsync<T>(T entity) where T : BaseEntity
        {
            _context.Set<T>().Update(entity);
            return Task.FromResult(entity);
        }

        public async Task DeleteAsync<T>(int id) where T : BaseEntity
        {
            var entity = await GetByIdAsync<T>(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
