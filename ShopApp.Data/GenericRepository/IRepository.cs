using Microsoft.EntityFrameworkCore.Query;
using ShopApp.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShopApp.Data.GenericRepository
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        void Add(TEntity entity);

        Task AddAsync(TEntity entity);

        void AddRange(List<TEntity> entity);

        Task AddRangeAsync(List<TEntity> entity);
        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        IQueryable<TEntity> GetAllNoTracking();

        IQueryable<TEntity> GetAllNoTracking(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        TEntity Get(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        TEntity Get(Expression<Func<TEntity, bool>> predicate = null);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate = null);

        IIncludableQueryable<TEntity, object> Include(Expression<Func<TEntity, object>> expression);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        Task DeleteAsync(TEntity entity);

        void Delete(int id);

        Task DeleteAsync(int id);

        void DeleteRange(List<TEntity> entity);

        /// <summary>
        /// Mevcut Dmo üzerinde parametre olarak geçilen predicate için tabloda varmı/yokmu kontrolü yapar. True/False döner.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool IsExist(Expression<Func<TEntity, bool>> predicate);

        Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> predicate);


        Task<List<TEntity>> TakeAsync(int count);
    }
}
