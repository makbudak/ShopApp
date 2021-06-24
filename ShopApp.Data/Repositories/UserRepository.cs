using ShopApp.Model.Entity;

namespace ShopApp.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {

    }

    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ShopContext context) : base(context)
        {

        }

    }
}
