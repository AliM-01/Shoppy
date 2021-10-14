using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using _0_Framework.Domain;
using _0_Framework.Domain.IGenericRepository;
using Microsoft.EntityFrameworkCore;

namespace _0_Framework.Infrastructure.GenericRepository
{
    public class GenericRepository<TContext, TEntity> : IGenericRepository<TEntity>
        where TContext : DbContext
        where TEntity : BaseEntity
    {
        #region Ctor
         
        private readonly TContext _context;

        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(TContext context)
        {
            this._context = context;
            this._dbSet = _context.Set<TEntity>();
        }

        #endregion
        
        #region CRUD

        #region Create

        public async Task InsertEntity(TEntity entity)
        {
            entity.CreationDate = DateTime.Now;
            entity.LastUpdateDate = DateTime.Now;
            await _dbSet.AddAsync(entity);
        }

        public async Task InsertRangeEntity(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                await InsertEntity(entity);
            }
        }

        #endregion 

        #region Read

        public async Task<TEntity> GetEntityById(long entityId)
        {
            return await _dbSet.AsNoTracking().SingleOrDefaultAsync(s => s.Id == entityId);
        }

        #endregion 

        #region Update

        public void Update(TEntity entity)
        {
            entity.LastUpdateDate = DateTime.Now;
            _dbSet.Update(entity);
        }

        #endregion

        #region Delete
         
        public async Task SoftDelete(long entityId)
        {
            TEntity entity = await GetEntityById(entityId);
            entity.IsDeleted = true;
            Update(entity);
        }

        public async Task FullDelete(long entityId)
        {
            TEntity entity = await GetEntityById(entityId);
            _context.Remove(entity);
        }

        #endregion

        #endregion

        #region Exists

        public bool Exists(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.Any(expression);
        }

        #endregion

        #region Save Changes

        public async Task SaveChanges()
        {
           await _context.SaveChangesAsync();
        }

        #endregion

        #region Get Query

        public IQueryable<TEntity> GetQuery()
        {
            return _dbSet.AsNoTracking().AsQueryable();
        }

        #endregion

        #region Dispose

        public async ValueTask DisposeAsync()
        {
            if (_context != null)
            {
                await _context.DisposeAsync();
            }
        }

        #endregion
    }
}