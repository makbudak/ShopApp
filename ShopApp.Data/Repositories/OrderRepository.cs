using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShopApp.Model.Entity;

namespace ShopApp.Data.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        List<Order> GetOrders(string userId);
    }

    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {

        public OrderRepository(ShopContext context) : base(context)
        {

        }

        private ShopContext ShopContext
        {
            get { return context as ShopContext; }
        }

        public List<Order> GetOrders(string userId)
        {

            var orders = ShopContext.Orders
                                .Include(i => i.OrderItems)
                                .ThenInclude(i => i.Product)
                                .AsQueryable();

            if (!string.IsNullOrEmpty(userId))
            {
                orders = orders.Where(i => i.UserId == userId);
            }

            return orders.ToList();
        }
    }
}