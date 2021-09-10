using ShopApp.Data.GenericRepository;
using ShopApp.Extensions;
using ShopApp.Model.Dto;
using ShopApp.Model.Dto.User;
using ShopApp.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ShopApp.Business.Services
{
    public interface IUserService
    {
        Pagination<UserModel> Get(UserFilterModel model);
        User GetById(int id);
        ServiceResult Login(LoginModel model);
        ServiceResult Put(UserModel model);
        ServiceResult Post(UserModel model);
        ServiceResult Delete(int id);
    }

    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Pagination<UserModel> Get(UserFilterModel model)
        {
            var entity = _unitOfWork.Repository<User>()
                .GetAll(x => !x.Deleted);

            if (!string.IsNullOrEmpty(model.Name))
                entity = entity.Where(x => x.Name.Contains(model.Name));

            if (!string.IsNullOrEmpty(model.Surname))
                entity = entity.Where(x => x.Surname.Contains(model.Surname));

            if (!string.IsNullOrEmpty(model.Email))
                entity = entity.Where(x => x.Email.Contains(model.Email));

            var list = entity.Skip((model.PageNumber - 1) * model.PageSize)
                 .Take(model.PageSize)
                 .Select(x => new UserModel
                 {
                     Id = x.Id,
                     UserType = x.UserType,
                     Name = x.Name,
                     Surname = x.Surname,
                     Email = x.Email,
                     Phone = x.Phone,
                     EmailConfirmed = x.EmailConfirmed,
                     IsActive = x.IsActive,
                 }).ToList();

            var result = new Pagination<UserModel>
            {
                List = list,
                Total = entity.Count()
            };

            return result;
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

        public ServiceResult Put(UserModel model)
        {
            var result = new ServiceResult { StatusCode = HttpStatusCode.OK };

            var user = GetById(model.Id);
            if (user != null)
            {
                var checkEmail = _unitOfWork.Repository<User>()
                    .GetAll(x => x.Id != model.Id && x.Email == model.Email)
                    .Any();

                if (!checkEmail)
                {
                    if (!user.EmailConfirmed)
                    {
                        user.Email = model.Email;
                    }
                    user.IsActive = model.IsActive;
                    user.Name = model.Name;
                    user.Phone = model.Phone;
                    user.Surname = model.Surname;
                    user.UpdatedDate = DateTime.Now;
                    user.UserType = model.UserType;
                    _unitOfWork.Save();
                }
                else
                {
                    result.StatusCode = HttpStatusCode.BadRequest;
                    result.Message = "Email adresi daha önce eklenmiş.";
                }
            }
            else
            {
                result.StatusCode = HttpStatusCode.NotFound;
                result.Message = "Kullanıcı bulunamadı.";
            }
            return result;
        }

        public ServiceResult Post(UserModel model)
        {
            var result = new ServiceResult { StatusCode = HttpStatusCode.OK };

            var checkEmail = _unitOfWork.Repository<User>()
                .GetAll(x => x.Email == model.Email)
                .Any();

            if (!checkEmail)
            {
                var user = new User()
                {
                    Deleted = false,
                    Email = model.Email,
                    EmailConfirmed = false,
                    InsertedDate = DateTime.Now,
                    IsActive = model.IsActive,
                    Name = model.Name,
                    Phone = model.Phone,
                    Surname = model.Surname,
                    UserType = model.UserType
                };
                _unitOfWork.Repository<User>().Add(user);
                _unitOfWork.Save();
            }
            else
            {
                result.StatusCode = HttpStatusCode.NotFound;
                result.Message = "Kullanıcı bulunamadı.";
            }
            return result;
        }

        public ServiceResult Delete(int id)
        {
            var result = new ServiceResult { StatusCode = HttpStatusCode.OK };

            var user = GetById(id);
            if (user != null)
            {
                user.Deleted = true;
                _unitOfWork.Save();
            }
            return result;
        }
    }
}
