using ShopApp.Data.GenericRepository;
using ShopApp.Extensions;
using ShopApp.Model.Dto;
using ShopApp.Model.Dto.User;
using ShopApp.Model.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ShopApp.Business.Services
{
    public interface IUserService
    {
        List<UserModel> Get();
        User GetById(int id);
        ServiceResult Login(LoginModel model);
    }

    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<UserModel> Get()
        {
            return _unitOfWork.Repository<User>()
                .GetAll(x => !x.Deleted)
                .Select(x => new UserModel
                {
                    Id = x.Id,
                    FirstName = x.Name,
                    LastName = x.Surname,
                    Email = x.Email,
                    Phone = x.Phone,
                    EmailConfirmed = x.EmailConfirmed
                }).ToList();

        }

        public User GetById(int id)
        {
            return _unitOfWork.Repository<User>().Get(x => x.Id == id);
        }

        public ServiceResult Login(LoginModel model)
        {
            var result = new ServiceResult { StatusCode = HttpStatusCode.OK };
            string hashedPassword = HashExtension.Sha256(model.Password);

            var user = _unitOfWork.Repository<User>()
                .Get(x => !x.Deleted && x.EmailConfirmed && x.IsActive && x.Email == model.Email && x.Password == hashedPassword);
            if (user == null)
            {
                result.StatusCode = HttpStatusCode.NotFound;
                result.Message = "Email adresi veya şifre hatalıdır. Lütfen tekrar deneyiniz.";
                return result;
            }
            else
            {
                if (!user.EmailConfirmed)
                {
                    result.StatusCode = HttpStatusCode.NotAcceptable;
                    result.Message = "Email adresi onaylanmamış. Lütfen girilen email adresine gelen link ile aktivasyon işlemini gerçekleştiriniz.";
                    return result;
                }
                result.Data = user;
            }
            return result;
        }
    }
}
