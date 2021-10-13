using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Domain;

namespace _0_Framework.Infrastructure.GenericRepository
{
    public interface IGenericRepository<TEntity> : IAsyncDisposable where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetQuery();

        Task InsertEntity(TEntity entity);

        Task InsertRangeEntity(List<TEntity> entities);

        Task<TEntity> GetEntityById(long entityId);

        void UpdateEntity(TEntity entity);

        Task SoftDeleteEntity(long entityId);

        Task FullyDeleteEntity(long entityId);

        Task SaveChanges();
    }
}