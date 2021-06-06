using System;

namespace ShopApp.Data.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ICartRepository Carts { get; }
        ICategoryRepository Categories { get; }
        IOrderRepository Orders { get; }
        IProductRepository Products { get; }
        void Save();

    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShopContext _context;
        public UnitOfWork(ShopContext context)
        {
            _context = context;
        }

        private CartRepository _cartRepository;
        private CategoryRepository _categoryRepository;
        private OrderRepository _orderRepository;
        private ProductRepository _productRepository;

        public ICartRepository Carts =>
            _cartRepository = _cartRepository ?? new CartRepository(_context);

        public ICategoryRepository Categories =>
            _categoryRepository = _categoryRepository ?? new CategoryRepository(_context);

        public IOrderRepository Orders =>
            _orderRepository = _orderRepository ?? new OrderRepository(_context);

        public IProductRepository Products =>
            _productRepository = _productRepository ?? new ProductRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}