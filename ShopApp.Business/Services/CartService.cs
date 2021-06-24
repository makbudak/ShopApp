using ShopApp.Data.Repositories;
using ShopApp.Model.Entity;

namespace ShopApp.Business.Services
{
    public interface ICartService
    {
        void InitializeCart(int customerId);
        Cart GetCartByCustomerId(int customerId);
        void AddToCart(int customerId, int productId, int quantity);
        void DeleteFromCart(int customerId, int productId);
        void ClearCart(int cartId);
    }

    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitofwork;
        public CartService(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public void AddToCart(int customerId, int productId, int quantity)
        {
            var cart = GetCartByCustomerId(customerId);

            if(cart!=null)
            {
                // eklenmek isteyen ürün sepette varmı (güncelleme)
                // eklenmek isteyen ürün sepette var ve yeni kayıt oluştur. (kayıt ekleme)

                var index = cart.CartItems.FindIndex(i=>i.ProductId==productId);                
                if(index<0)
                {
                    cart.CartItems.Add(new CartItem(){
                        ProductId = productId,
                        Quantity = quantity,
                        CartId = cart.Id
                    });
                }
                else{
                    cart.CartItems[index].Quantity += quantity;
                }

                _unitofwork.Carts.Update(cart);
                _unitofwork.Save();

            }
        }

        public void ClearCart(int cartId)
        {
            _unitofwork.Carts.ClearCart(cartId);
        }

        public void DeleteFromCart(int customerId, int productId)
        {
            var cart = GetCartByCustomerId(customerId);
            if(cart!=null)
            {
                _unitofwork.Carts.DeleteFromCart(cart.Id,productId);
            }   
        }

        public Cart GetCartByCustomerId(int customerId)
        {
            return _unitofwork.Carts.GetByUserId(customerId);
        }

        public void InitializeCart(int customerId)
        {
            _unitofwork.Carts.Create(new Cart(){CustomerId = customerId});
            _unitofwork.Save();
        }
    }
}