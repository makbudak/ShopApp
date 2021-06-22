using System.ComponentModel.DataAnnotations;

namespace ShopApp.Model.Entity
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
