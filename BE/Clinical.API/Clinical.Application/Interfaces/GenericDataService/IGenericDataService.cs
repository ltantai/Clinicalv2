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

        Task<T> AddAsync<T>(T entity) where T : BaseEntity;
        Task<T> UpdateAsync<T>(T entity) where T : BaseEntity;
        Task DeleteAsync<T>(int id) where T : BaseEntity;

        Task SaveChangesAsync();
    }
}
