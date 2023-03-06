using System.ComponentModel.DataAnnotations;

namespace ShopApp.Model.Entity
{
    public class UserAddress : BaseEntity
    {
        public int UserId { get; set; }

        public User User { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(500)]

        public string NameSurname { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

        public int DistrictId { get; set; }
        public District District { get; set; }

        public int NeighborhoodId { get; set; }
        public Neighborhood Neighborhood { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        [StringLength(100)]
        public string PostCode { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        public bool Deleted{ get; set; }
    }
}
