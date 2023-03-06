using Microsoft.EntityFrameworkCore;
using ShopApp.Data.GenericRepository;
using ShopApp.Model.Entity;
using System.Linq;

namespace ShopApp.Business.Services
{
    public interface ICartService
    {
        void InitializeCart(int userId);
        Cart GetCartByUserId(int userId);
        void AddToCart(int userId, int productId, int quantity);
        void DeleteFromCart(int userId, int productId);
        void ClearCart(int cartId);
    }

    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitofwork;
        public CartService(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public void AddToCart(int userId, int productId, int quantity)
        {
            var cart = GetCartByUserId(userId);

            if (cart != null)
            {
                // eklenmek isteyen ürün sepette varmı (güncelleme)
                // eklenmek isteyen ürün sepette var ve yeni kayıt oluştur. (kayıt ekleme)

                var index = cart.CartItems.FindIndex(i => i.ProductId == productId);
                if (index < 0)
                {
                    cart.CartItems.Add(new CartItem()
                    {
                        ProductId = productId,
                        Quantity = quantity,
                        CartId = cart.Id
                    });
                }
                else
                {
                    cart.CartItems[index].Quantity += quantity;
                }

                _unitofwork.Repository<Cart>().Update(cart);
                _unitofwork.Save();
            }
        }

        public void ClearCart(int cartId)
        {
            var cartItems = _unitofwork.Repository<CartItem>()
                .Where(x => x.CartId == cartId).ToList();
            if (cartItems != null && cartItems.Any())
            {
                _unitofwork.Repository<CartItem>().DeleteRange(cartItems);
                _unitofwork.Save();
            }
        }

        public void DeleteFromCart(int userId, int productId)
        {
            var cart = GetCartByUserId(userId);
            if (cart != null)
            {
                var cartItems = _unitofwork.Repository<CartItem>()
                    .Where(x => x.CartId == cart.Id && x.ProductId == productId)
                    .ToList();
                if (cartItems != null && cartItems.Any())
                {
                    _unitofwork.Repository<CartItem>().DeleteRange(cartItems);
                    _unitofwork.Save();
                }
            }
        }

        public Cart GetCartByUserId(int userId)
        {
            return _unitofwork.Repository<Cart>()
                .FirstOrDefault(x => x.UserId == userId,
                 a => a.Include(o => o.CartItems)
                       .ThenInclude(o => o.Product));
        }

        public void InitializeCart(int userId)
        {
            var model = new Cart()
            {
                UserId = userId
            };
            _unitofwork.Repository<Cart>().Add(model);
            _unitofwork.Save();
        }
    }
}