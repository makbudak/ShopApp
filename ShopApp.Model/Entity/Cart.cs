using System.Collections.Generic;

namespace ShopApp.Model.Entity
{
    public class Cart: BaseEntity
    {        
        public string UserId { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}