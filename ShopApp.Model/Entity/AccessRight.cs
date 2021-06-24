using System.ComponentModel.DataAnnotations;

namespace ShopApp.Model.Entity
{
    public class AccessRight : BaseEntity
    {
        public int AccessRightCategoryId { get; set; }

        public AccessRightCategory AccessRightCategory { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Title { get; set; }

        [StringLength(250)]
        public string EndPoint { get; set; }

        public bool IsActive { get; set; }

        public bool Deleted { get; set; }
    }
}
