using ShopApp.Model.Entity;

namespace ShopApp.Data.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {

    }

    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ShopContext context) : base(context)
        {

        }

        private ShopContext ShopContext
        {
            get { return context as ShopContext; }
        }
    }
}
