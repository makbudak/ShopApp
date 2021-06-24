using System.ComponentModel.DataAnnotations;

namespace ShopApp.Model.Entity
{
    public class AccessRightCategory: BaseEntity
    {
        [StringLength(250)]
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public bool Deleted { get; set; }
    }
}
