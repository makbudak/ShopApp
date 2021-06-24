using System.Collections.Generic;

namespace ShopApp.Model.Entity
{
    public class Cart : BaseEntity
    {
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public List<CartItem> CartItems { get; set; }
    }
}