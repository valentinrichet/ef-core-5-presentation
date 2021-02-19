using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Data.Database;

namespace WebApi.Services.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly DbContext dbContext;
        protected readonly DbSet<T> dbSet;

        public BaseRepository(EfCorePresentationContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<T>();
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            var entityEntry = await dbSet.AddAsync(entity, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            entityEntry.State = EntityState.Detached;
            return entityEntry.Entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            ICollection<EntityEntry<T>> entityEntries = new List<EntityEntry<T>>();
            foreach (var entity in entities)
            {
                var entityEntry = await dbSet.AddAsync(entity, cancellationToken);
                entityEntries.Add(entityEntry);
            }
            await dbContext.SaveChangesAsync(cancellationToken);
            ICollection<T> addedEntities = new List<T>();
            foreach (var addedEntityEntry in entityEntries)
            {
                addedEntityEntry.State = EntityState.Detached;
                addedEntities.Add(addedEntityEntry.Entity);
            }
            return addedEntities;
        }

        public IQueryable<T> GetAllQuery()
        {
            var query = dbSet.AsNoTracking().AsSingleQuery();
            return query;
        }

        public IQueryable<T> GetAllQueryWithTracking()
        {
            var query = dbSet.AsSingleQuery();
            return query;
        }

        public async Task RemoveAsync(T entity, CancellationToken cancellationToken)
        {
            dbSet.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            dbSet.Update(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
