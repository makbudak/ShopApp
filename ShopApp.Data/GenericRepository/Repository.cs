using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using ShopApp.Data;
using ShopApp.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Data.GenericRepository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ShopContext _context;
        private readonly DbSet<TEntity> _dbSet;
        private bool _isDisposed;

        public Repository(IUnitOfWork unitOfWork) : this(unitOfWork.Context)
        {
            _isDisposed = unitOfWork.IsDisposed;
        }

        public Repository(ShopContext context)
        {

            if (context != null && !_isDisposed)
                _context = context;


            _dbSet = _context.Set<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {

            if (entity == null)
                throw new ArgumentNullException("Entity is null");

            _dbSet.Add(entity);
        }

        public async virtual Task AddAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity is null");

            await _dbSet.AddAsync(entity);
        }

        public virtual void AddRange(List<TEntity> entity)
        {
            try
            {
                /* Bulk Insert işlemlerinde performans artışır sağlar. Ref: https://docs.microsoft.com/tr-tr/ef/ef6/saving/change-tracking/auto-detect-changes?redirectedfrom=MSDN */

                _context.ChangeTracker.AutoDetectChangesEnabled = false;

                _dbSet.AddRange(entity);
            }
            finally
            {
                _context.ChangeTracker.AutoDetectChangesEnabled = true;
            }

        }

        public async virtual Task AddRangeAsync(List<TEntity> entity)
        {
            await _dbSet.AddRangeAsync(entity);
        }

        /// <summary>
        /// Verilen Generic Entity' i siler.
        /// </summary>
        /// <param name="entity"></param>
        public async virtual Task DeleteAsync(TEntity entity)
        {
            entity = await FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (entity != null)
                _dbSet.Remove(entity);
        }


        public virtual void Delete(TEntity entity)
        {
            entity = FirstOrDefault(x => x.Id == entity.Id);

            if (entity != null)
                _dbSet.Remove(entity);
        }

        /// <summary>
        /// Verilen Id yi bulup Entity ' i siler.
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(int id)
        {
            var entity = FirstOrDefault(x => x.Id == id);

            if (entity != null)
                _dbSet.Remove(entity);
            else
                throw new Exception("Data bulunamadı");
        }


        public async virtual Task DeleteAsync(int id)
        {
            var entity = await FirstOrDefaultAsync(x => x.Id == id);

            if (entity != null)
                _dbSet.Remove(entity);

            else
                throw new Exception("Data bulunamadı");
        }

        public virtual IQueryable<TEntity> Where()
        {
            return _dbSet.AsQueryable();/*.Where(x=> !x.IsDeleted)*/
        }

        public virtual IQueryable<TEntity> GetAllNoTracking()
        {
            return _dbSet.AsQueryable().AsNoTracking();/*.Where(x=> !x.IsDeleted)*/
        }

        public IQueryable<TEntity> GetAllNoTracking(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (include != null)
                query = include(query);

            if (predicate != null)
                query = query.Where(predicate).AsNoTracking();

            return query;
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (include != null)
                query = include(query);

            if (predicate != null)
                return query.FirstOrDefault(predicate);

            return query.FirstOrDefault();
        }


        public async virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (include != null)
                query = include(query);

            if (predicate != null)
                return await query.FirstOrDefaultAsync(predicate);

            return await query.FirstOrDefaultAsync();
        }

        public virtual IIncludableQueryable<TEntity, object> Include(Expression<Func<TEntity, object>> expression)
        {
            IQueryable<TEntity> query = _dbSet;

            return query.Include(expression);
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (predicate != null)
                return query.FirstOrDefault(predicate);

            return query.FirstOrDefault();
        }


        public async virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null)
        {

            IQueryable<TEntity> query = _dbSet;

            if (predicate != null)
                return await query.FirstOrDefaultAsync(predicate);

            return await query.FirstOrDefaultAsync();
        }

        public virtual void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity is null");

            _dbSet.Update(entity);
        }

        public virtual void Update(List<TEntity> entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity is null");

            _dbSet.UpdateRange(entity);
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
            _isDisposed = true;

        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (include != null)
                query = include(query);

            if (predicate != null)
                query = query.Where(predicate);

            return query;
        }


        public virtual bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("entity is null");

            return _dbSet.Any(predicate);

        }

        public async virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("entity is null");

            return await _dbSet.AnyAsync(predicate);

        }



        public async Task<List<TEntity>> TakeAsync(int count)
        {
            return await _dbSet.Take(count).ToListAsync();

        }

        public void DeleteRange(List<TEntity> entity)
        {
            _dbSet.RemoveRange(entity);
        }
    }

}
