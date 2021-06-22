namespace ShopApp.Model.Entity
{
    public class CartItem: BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }

        public int Quantity { get; set; }
    }
}