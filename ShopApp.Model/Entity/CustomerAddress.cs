using System.ComponentModel.DataAnnotations;

namespace ShopApp.Model.Entity
{
    public class CustomerAddress : BaseEntity
    {
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(500)]

        public string NameSurname { get; set; }

        public int CityId { get; set; }

        public int ProvinceId { get; set; }

        public int DistrictId { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        [StringLength(100)]
        public string PostCode { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }
    }
}
