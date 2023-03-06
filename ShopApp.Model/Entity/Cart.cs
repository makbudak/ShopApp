using System.Collections.Generic;

namespace ShopApp.Model.Entity
{
    public class Cart : BaseEntity
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public List<CartItem> CartItems { get; set; }
    }
}