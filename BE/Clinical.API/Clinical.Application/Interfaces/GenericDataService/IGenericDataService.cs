using Clinical.Domain.Commons.CommonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Clinical.Application.Interfaces.GenericDataService
{
    public interface IGenericDataService
    {
        Task<T> GetByIdAsync<T>(int id) where T : BaseEntity;
        Task<T?> GetByIdAsync<T>(
            int id,
            IList<Expression<Func<T, object?>>> includeExpressions,
            bool withTracking = true
        ) where T : BaseEntity;
        Task<IReadOnlyList<T>> GetAllAsync<T>(bool withTracking = true) where T : BaseEntity;

        Task<IReadOnlyList<T>> GetAllAsync<T>(
            Expression<Func<T, bool>> condition,
            bool withTracking = true
        ) where T : BaseEntity;

        Task<IReadOnlyList<T>> GetAllAsync<T>(
            Expression<Func<T, bool>> condition,
            IList<Expression<Func<T, object?>>> includeExpressions,
            bool withTracking = true
        ) where T : BaseEntity;

        Task<(IReadOnlyList<T> Items, int TotalCount)> GetAllPaginatedAsync<T>(
            Expression<Func<T, bool>> condition,
            int pageNumber,
            int pageSize,
            bool withTracking = true
        ) where T : BaseEntity;

        Task<T> AddAsync<T>(T entity) where T : BaseEntity;
        Task<IEnumerable<T>> AddRangeAsync<T>(IEnumerable<T> entities) where T : BaseEntity;

        Task<T> UpdateAsync<T>(T entity) where T : BaseEntity;
        Task<IEnumerable<T>> UpdateRangeAsync<T>(IEnumerable<T> entities) where T : BaseEntity;

        Task DeleteAsync<T>(int id) where T : BaseEntity;
        Task DeleteRangeAsync<T>(IEnumerable<int> ids) where T : BaseEntity;

        Task SaveChangesAsync();
    }
}
