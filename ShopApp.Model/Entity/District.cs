namespace ShopApp.Model.Entity
{
    public class District : BaseEntity
    {
        public int CityId { get; set; }
        public City City { get; set; }
        public string Name { get; set; }
    }
}
