using Microsoft.EntityFrameworkCore;
using ShopApp.Model.Entity;
using System;

namespace ShopApp.Data.GenericRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ShopContext Context { get; }
        IRepository<T> Repository<T>() where T : BaseEntity;
        void Save();
        bool IsDisposed { get; }
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShopContext _context;
        private bool _disposed = false;

        public ShopContext Context
        {
            get { return _context; }
        }

        public bool IsDisposed
        {
            get { return _disposed; }
        }

        public UnitOfWork(ShopContext context)
        {
            _context = context;
            //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IRepository<T> Repository<T>() where T : BaseEntity
        {
            return new Repository<T>(_context);
        }
    }
}