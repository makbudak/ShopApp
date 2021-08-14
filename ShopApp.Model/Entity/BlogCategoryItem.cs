namespace ShopApp.Model.Entity
{
    public class BlogCategoryItem : BaseEntity
    {
        public int BlogCategoryId { get; set; }

        public BlogCategory BlogCategory { get; set; }

        public int BlogId { get; set; }

        public Blog Blog { get; set; }
    }
}
