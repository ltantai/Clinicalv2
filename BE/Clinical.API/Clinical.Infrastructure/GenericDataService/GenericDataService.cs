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
            return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id && !x.IsDelete);
        }

        public async Task<T?> GetByIdAsync<T>(
            int id,
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

            return await query.FirstOrDefaultAsync(x => x.Id == id && !x.IsDelete);
        }

        public async Task<IReadOnlyList<T>> GetAsync<T>(Expression<Func<T, bool>> predicate, bool withTracking = true) where T : BaseEntity
        {
            var query = _context.Set<T>().Where(x => !x.IsDelete).Where(predicate);

            if (!withTracking)
                query = query.AsNoTracking();

            return await query.ToListAsync();
        }


        public async Task<IReadOnlyList<T>> GetAllAsync<T>(bool withTracking = true) where T : BaseEntity
        {
            var query = _context.Set<T>().Where(x => !x.IsDelete).AsQueryable();
            if (!withTracking) query = query.AsNoTracking();
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync<T>(
            Expression<Func<T, bool>> condition,
            bool withTracking = true
        ) where T : BaseEntity
        {
            var query = _context.Set<T>().Where(condition).Where(x => !x.IsDelete);
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
            return await query.Where(condition).Where(x => !x.IsDelete).ToListAsync();
        }

        //Get pagination
        public async Task<(IReadOnlyList<T> Items, int TotalCount)> GetAllPaginatedAsync<T>(
            Expression<Func<T, bool>> condition,
            int pageNumber,
            int pageSize,
            IList<Expression<Func<T, object?>>>? includeExpressions = null,
            bool withTracking = true
        ) where T : BaseEntity
        {
            IQueryable<T> query = _context.Set<T>();

            // Apply includes if provided
            if (includeExpressions != null)
            {
                foreach (var include in includeExpressions)
                {
                    query = query.Include(include);
                }
            }

            // Apply tracking setting
            if (!withTracking)
            {
                query = query.AsNoTracking();
            }

            // Apply filters
            query = query.Where(condition).Where(x => !x.IsDelete);

            // Total count before pagination
            var totalCount = await query.CountAsync();

            // Apply pagination
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }


        public async Task<T> AddAsync<T>(T entity) where T : BaseEntity
        {
            entity.CreateTime = DateTime.Now;
            entity.IsDelete = false;

            var entry = await _context.Set<T>().AddAsync(entity);
            await SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync<T>(IEnumerable<T> entities) where T : BaseEntity
        {
            if (entities.Any())
            {
                foreach (var entity in entities)
                {
                    entity.CreateTime = DateTime.Now;
                    entity.IsDelete = false;
                }
            }
            await _context.Set<T>().AddRangeAsync(entities);
            await SaveChangesAsync();
            return entities;
        }

        public async Task<T> UpdateAsync<T>(T entity) where T : BaseEntity
        {
            entity.ModifyTime = DateTime.Now;

            _context.Set<T>().Update(entity);

            await SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> UpdateRangeAsync<T>(IEnumerable<T> entities) where T : BaseEntity
        {
            if (entities.Any())
            {
                foreach (var entity in entities)
                {
                    entity.ModifyTime = DateTime.Now;
                }
            }
            _context.Set<T>().UpdateRange(entities);
            await SaveChangesAsync();
            return entities;
        }

        public async Task DeleteAsync<T>(int id) where T : BaseEntity
        {
            var entity = await GetByIdAsync<T>(id);
            if (entity != null)
            {
                entity.IsDelete = true;
                await UpdateAsync(entity);
                //_context.Set<T>().Remove(entity);
            }
            await SaveChangesAsync();
        }

        public async Task DeleteRangeAsync<T>(IEnumerable<int> ids) where T : BaseEntity
        {
            var entities = await _context.Set<T>().Where(e => ids.Contains(e.Id)).ToListAsync();

            if (entities.Any())
            {
                foreach (var entity in entities)
                {
                    entity.IsDelete = false;
                }
                await UpdateRangeAsync(entities);
                //_context.Set<T>().RemoveRange(entities);
            }
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        private async Task AddAuditTable()
        {

        }
    }
}
