using Microsoft.EntityFrameworkCore;
using ShopApp.Data.GenericRepository;
using ShopApp.Model.Entity;
using System.Collections.Generic;
using System.Linq;

namespace ShopApp.Business.Services
{
    public interface IOrderService
    {
        void Create(Order entity);
        List<Order> GetOrders(int customerId);
    }

    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitofwork;
        public OrderService(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        public void Create(Order entity)
        {
            _unitofwork.Repository<Order>().Add(entity);
            _unitofwork.Save();
        }

        public List<Order> GetOrders(int customerId)
        {
            return _unitofwork.Repository<Order>()
                .GetAll(i => i.CustomerId == customerId)
                .Include(i => i.OrderItems)
                .ThenInclude(i => i.Product)
                .ToList();
        }
    }
}