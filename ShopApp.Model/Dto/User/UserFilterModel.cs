namespace ShopApp.Model.Dto.User
{
    public class UserFilterModel : PageNumberModel
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }
    }
}
