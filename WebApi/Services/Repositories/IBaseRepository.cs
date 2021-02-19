using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Services.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        public Task<T> AddAsync(T entity, CancellationToken cancellationToken);
        public Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);
        public IQueryable<T> GetAllQuery();
        public IQueryable<T> GetAllQueryWithTracking();
        public Task RemoveAsync(T entity, CancellationToken cancellationToken);
        public Task UpdateAsync(T entity, CancellationToken cancellationToken);
    }
}
