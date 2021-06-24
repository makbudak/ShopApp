using System.Collections.Generic;
using ShopApp.Data.Repositories;
using ShopApp.Model.Entity;

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
            _unitofwork.Orders.Create(entity);
            _unitofwork.Save();
        }

        public List<Order> GetOrders(int customerId)
        {
            return  _unitofwork.Orders.GetOrders(customerId);
        }
    }
}