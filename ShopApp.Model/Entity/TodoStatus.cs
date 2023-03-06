namespace ShopApp.Model.Entity
{
    public class TodoStatus : BaseEntity
    {
        public int TodoCategoryId { get; set; }

        public TodoCategory TodoCategory { get; set; }

        public string Name { get; set; }

        public int DisplayOrder { get; set; }

        public bool Deleted { get; set; }
    }
}
