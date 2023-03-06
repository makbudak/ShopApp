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
        List<Order> GetOrders(int userId);
    }

    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Create(Order entity)
        {
            _unitOfWork.Repository<Order>().Add(entity);
            _unitOfWork.Save();
        }

        public List<Order> GetOrders(int userId)
        {
            return _unitOfWork.Repository<Order>()
                .Where(i => i.UserId == userId)
                .Include(i => i.OrderItems)
                .ThenInclude(i => i.Product)
                .OrderByDescending(x => x.Id).ToList();
        }
    }
}