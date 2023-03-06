using System.Collections.Generic;

namespace ShopApp.Model.Entity
{
    public class BlogCategory : BaseEntity
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public bool IsActive { get; set; }

        public bool Deleted { get; set; }
    }
}
