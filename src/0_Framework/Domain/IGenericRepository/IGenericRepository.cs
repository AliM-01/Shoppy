using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace _0_Framework.Domain.IGenericRepository
{
    public interface IGenericRepository<TEntity> : IAsyncDisposable where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetQuery();

        Task InsertEntity(TEntity entity);

        Task InsertRangeEntity(List<TEntity> entities);

        Task<TEntity> GetEntityById(long entityId);

        void Update(TEntity entity);

        Task SoftDelete(long entityId);

        Task FullDelete(long entityId);

        bool Exists(Expression<Func<TEntity, bool>> expression);

        Task SaveChanges();
    }
}