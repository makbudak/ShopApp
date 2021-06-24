using ShopApp.Data.Repositories;
using ShopApp.Model.Entity;
using System.Collections.Generic;
using System.Linq;

namespace ShopApp.Business.Services
{
    public interface IUserService
    {
        List<User> GetAll();
        User GetById(int id);
    }

    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitofwork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitofwork = unitOfWork;
        }

        public List<User> GetAll()
        {
            return _unitofwork.Users.GetAll().Where(x => !x.Deleted).ToList();
        }

        public User GetById(int id)
        {
            return _unitofwork.Users.GetById(id);
        }
    }
}
