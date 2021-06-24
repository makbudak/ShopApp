using ShopApp.Data.Repositories;
using ShopApp.Extensions;
using ShopApp.Model.Entity;
using System.Linq;

namespace ShopApp.Business.Services
{
    public interface ICustomerService
    {
        Customer GetById(int id);

        Customer GetByEmail(string email);
        
        bool IsEmailConfirmed(string email);

        bool LoginControl(string email, string password);
    }

    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitofwork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitofwork = unitOfWork;
        }

        public Customer GetByEmail(string email)
        {
            return _unitofwork.Customers.GetAll()
                .FirstOrDefault(x => x.Email == email && !x.Deleted);
        }

        public Customer GetById(int id)
        {
            return _unitofwork.Customers.GetById(id);
        }

        public bool IsEmailConfirmed(string email)
        {
            var customer = GetByEmail(email);
            if (customer == null)
                return false;
            else
                return customer.EmailConfirmed;
        }

        public bool LoginControl(string email, string password)
        {
            string hashedPassword = HashExtension.Sha256(password);
            return _unitofwork.Customers.GetAll().Any(x => !x.Deleted && x.EmailConfirmed && x.IsActive && x.Email == email && x.Password == hashedPassword);
        }
    }
}
