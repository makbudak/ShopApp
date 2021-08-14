namespace ShopApp.Model.Entity
{
    public class ProductCategoryItem: BaseEntity
    {
        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}