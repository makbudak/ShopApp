using System.Collections.Generic;

namespace ShopApp.Model.Entity
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public int ReadCount { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }
        public bool Published { get; set; }
        public bool Deleted { get; set; }
        public List<ProductCategoryItem> ProductCategoryItems { get; set; }

    }
}
