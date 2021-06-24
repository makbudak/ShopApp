using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShopApp.Model.Entity;

namespace ShopApp.Data.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        List<Order> GetOrders(int customerId);
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

        public List<Order> GetOrders(int customerId)
        {
            var orders = ShopContext.Orders
                                .Include(i => i.OrderItems)
                                .ThenInclude(i => i.Product)
                                .Where(i => i.CustomerId == customerId).ToList();
            return orders;
        }
    }
}