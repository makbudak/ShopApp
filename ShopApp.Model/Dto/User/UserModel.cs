using ShopApp.Model.Enum;

namespace ShopApp.Model.Dto.User
{
    public class UserModel : BaseModel
    {
        public UserType UserType { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string Phone { get; set; }

        public bool IsActive { get; set; }
    }
}