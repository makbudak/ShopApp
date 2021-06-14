using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShopApp.Model.Entity;

namespace ShopApp.Data.Repositories
{
    public interface ICartRepository : IRepository<Cart>
    {
        Cart GetByUserId(string userId);
        void DeleteFromCart(int cartId, int productId);
        void ClearCart(int cartId);
    }

    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(ShopContext context) : base(context)
        {

        }

        private ShopContext ShopContext
        {
            get { return context as ShopContext; }
        }
        public void ClearCart(int cartId)
        {
            var cmd = @"delete from CartItems where CartId=@p0";
            ShopContext.Database.ExecuteSqlRaw(cmd, cartId);
        }

        public void DeleteFromCart(int cartId, int productId)
        {
            var cmd = @"delete from CartItems where CartId=@p0 and ProductId=@p1";
            ShopContext.Database.ExecuteSqlRaw(cmd, cartId, productId);
        }

        public Cart GetByUserId(string userId)
        {
            return ShopContext.Carts
                        .Include(i => i.CartItems)
                        .ThenInclude(i => i.Product)
                        .FirstOrDefault(i => i.UserId == userId);
        }

        public override void Update(Cart entity)
        {
            ShopContext.Carts.Update(entity);
        }
    }
}