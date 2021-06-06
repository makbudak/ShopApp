using System.Collections.Generic;
using ShopApp.Business.Abstract;
using ShopApp.Data.Repositories;
using ShopApp.Model.Entity;

namespace ShopApp.Business.Concrete
{
    public interface IOrderService
    {
        void Create(Order entity);
        List<Order> GetOrders(string userId);
    }

    public class OrderManager : IOrderService
    {
         private readonly IUnitOfWork _unitofwork;
        public OrderManager(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        public void Create(Order entity)
        {
            _unitofwork.Orders.Create(entity);
            _unitofwork.Save();
        }

        public List<Order> GetOrders(string userId)
        {
            return  _unitofwork.Orders.GetOrders(userId);
        }
    }
}