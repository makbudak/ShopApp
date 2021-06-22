using System.Collections.Generic;

namespace ShopApp.Model.Entity
{
    public class Category : BaseEntity
    {
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }

    }
}