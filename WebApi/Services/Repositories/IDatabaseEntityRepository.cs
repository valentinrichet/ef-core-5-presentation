using System.Threading;
using System.Threading.Tasks;
using WebApi.Data.Database.Models;

namespace WebApi.Services.Repositories
{
    public interface IDatabaseEntityRepository<T> : IBaseRepository<T> where T : DatabaseEntity
    {
        public Task RemoveAsync(int entityId, CancellationToken cancellationToken);
    }
}
