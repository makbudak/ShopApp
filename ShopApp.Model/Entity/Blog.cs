using System.Collections.Generic;

namespace ShopApp.Model.Entity
{
    public class Blog : BaseEntity
    {
        public int BlogCategoryId { get; set; }

        public BlogCategory BlogCategory { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public string ImageUrl { get; set; }

        public int ReadCount { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }

        public bool Published { get; set; }

        public bool Deleted { get; set; }
    }
}
