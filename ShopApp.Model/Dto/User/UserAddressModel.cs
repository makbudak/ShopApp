namespace ShopApp.Model.Dto.User
{
    public class UserAddressModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string NameSurname { get; set; }

        public int CityId { get; set; }

        public string CityName { get; set; }

        public int DistrictId { get; set; }

        public string DistrictName { get; set; }

        public int NeighborhoodId { get; set; }

        public string NeighborhoodName { get; set; }

        public string Address { get; set; }

        public string PostCode { get; set; }

        public string Phone { get; set; }
    }
}
