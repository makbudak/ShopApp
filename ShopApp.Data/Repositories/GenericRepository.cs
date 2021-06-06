using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ShopApp.Data.Repositories
{
    public interface IRepository<T>
    {
        T GetById(int id);

        List<T> GetAll();

        void Create(T entity);

        void Update(T entity);
        void Delete(T entity);
    }

    public class GenericRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        protected readonly DbContext context;
        public GenericRepository(DbContext ctx)
        {
            context = ctx;
        }
        public void Create(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
        }

        public List<TEntity> GetAll()
        {
            return context.Set<TEntity>().ToList();
        }

        public TEntity GetById(int id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public virtual void Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        } 
    }
}