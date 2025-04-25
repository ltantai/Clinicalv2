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

        //Get pagination
        public async Task<(IReadOnlyList<T> Items, int TotalCount)> GetAllPaginatedAsync<T>(
            Expression<Func<T, bool>> condition,
            int pageNumber,
            int pageSize,
            bool withTracking = true
        ) where T : BaseEntity
        {
            var query = _context.Set<T>().Where(condition);

            if (!withTracking)
                query = query.AsNoTracking();

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }


        public async Task<T> AddAsync<T>(T entity) where T : BaseEntity
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync<T>(IEnumerable<T> entities) where T : BaseEntity
        {
            await _context.Set<T>().AddRangeAsync(entities);
            return entities;
        }

        public Task<T> UpdateAsync<T>(T entity) where T : BaseEntity
        {
            _context.Set<T>().Update(entity);
            return Task.FromResult(entity);
        }

        public Task<IEnumerable<T>> UpdateRangeAsync<T>(IEnumerable<T> entities) where T : BaseEntity
        {
            _context.Set<T>().UpdateRange(entities);
            return Task.FromResult(entities);
        }

        public async Task DeleteAsync<T>(int id) where T : BaseEntity
        {
            var entity = await GetByIdAsync<T>(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
            }
        }

        public async Task DeleteRangeAsync<T>(IEnumerable<int> ids) where T : BaseEntity
        {
            var entities = await _context.Set<T>().Where(e => ids.Contains(e.Id)).ToListAsync();

            if (entities.Any())
            {
                _context.Set<T>().RemoveRange(entities);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
