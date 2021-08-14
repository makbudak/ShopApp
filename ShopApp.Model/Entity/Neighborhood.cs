namespace ShopApp.Model.Entity
{
    public class Neighborhood : BaseEntity
    {
        public int DistrictId { get; set; }
        public District District { get; set; }
        public string PostCode { get; set; }
        public string Name { get; set; }
    }
}
