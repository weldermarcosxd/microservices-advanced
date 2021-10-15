using Ordering.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : EntityBase
    {
        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken);

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);

        Task<IReadOnlyList<T>> GetAsync(
            CancellationToken cancellationToken,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeString = null,
            bool disableTracking = false
        );

        Task<IReadOnlyList<T>> GetAsync(
            CancellationToken cancellationToken,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includes = null,
            bool disableTracking = false
        );

        Task<T> GetByIdAsync(long id, CancellationToken cancellationToken);

        Task<T> CreateAsync(T entity, CancellationToken cancellationToken);

        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);

        Task<T> DeleteAsync(T entity, CancellationToken cancellationToken);
    }
}
