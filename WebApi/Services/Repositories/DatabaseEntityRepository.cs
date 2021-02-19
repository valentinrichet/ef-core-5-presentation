using System.Threading;
using System.Threading.Tasks;
using WebApi.Data.Database;
using WebApi.Data.Database.Models;

namespace WebApi.Services.Repositories
{
    public class DatabaseEntityRepository<T> : BaseRepository<T>, IDatabaseEntityRepository<T> where T : DatabaseEntity, new()
    {
        public DatabaseEntityRepository(EfCorePresentationContext dbContext) : base(dbContext)
        {
        }

        public async Task RemoveAsync(int entityId, CancellationToken cancellationToken)
        {
            T entity = new T
            {
                Id = entityId,
            };
            dbSet.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
