using ShopApp.Data.GenericRepository;
using ShopApp.Extensions;
using ShopApp.Model.Dto;
using ShopApp.Model.Dto.User;
using ShopApp.Model.Entity;
using ShopApp.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ShopApp.Business.Services
{
    public interface IUserService
    {
        List<UserModel> Get();
        User GetById(int id);
        User GetByEmail(string email);
        bool IsEmailConfirmed(string email);
        UserProfileModel GetProfile();
        ServiceResult ChangePassword(PasswordModel model);
        ServiceResult Login(LoginModel model);
        ServiceResult Put(UserModel model);
        ServiceResult UpdateProfile(UserProfileModel model);
        ServiceResult Post(UserModel model);
        ServiceResult Register(RegisterModel model);
        ServiceResult Delete(int id);
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
            var list = _unitOfWork.Repository<User>()
                .Where(x => !x.Deleted)
                .OrderByDescending(x => x.Id)
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

            return list;
        }

        public User GetById(int id)
        {
            return _unitOfWork.Repository<User>().FirstOrDefault(x => x.Id == id);
        }

        public User GetByEmail(string email)
        {
            return _unitOfWork.Repository<User>()
                .FirstOrDefault(x => x.Email == email && !x.Deleted);
        }

        public bool IsEmailConfirmed(string email)
        {
            var user = GetByEmail(email);
            if (user == null)
                return false;
            else
                return user.EmailConfirmed;
        }

        public ServiceResult Login(LoginModel model)
        {
            var result = new ServiceResult { StatusCode = HttpStatusCode.OK };
            string hashedPassword = HashExtension.Sha256(model.Password);

            var user = _unitOfWork.Repository<User>()
                .FirstOrDefault(x => !x.Deleted && x.EmailConfirmed && x.IsActive && x.Email == model.Email && x.Password == hashedPassword);
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
                    .Where(x => x.Id != model.Id && x.Email == model.Email)
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
                .Where(x => x.Email == model.Email)
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

        public ServiceResult Register(RegisterModel model)
        {
            var result = new ServiceResult { StatusCode = HttpStatusCode.OK };
            try
            {
                if (model.Password != model.RePassword)
                {
                    result.StatusCode = HttpStatusCode.BadRequest;
                    result.Message = "Şifre alanları uyuşmamaktadır.";
                    return result;
                }

                var emailCheck = _unitOfWork.Repository<User>()
                    .FirstOrDefault(x => !x.Deleted && x.Email == model.Email);

                if (emailCheck != null)
                {
                    result.StatusCode = HttpStatusCode.Found;
                    result.Message = "Email adresiyle daha önce kullanıcı kaydedilmiş.";
                    return result;
                }

                var entity = new User
                {
                    Deleted = false,
                    Email = model.Email,
                    EmailConfirmed = false,
                    InsertedDate = DateTime.Now,
                    IsActive = true,
                    Name = model.Name,
                    Password = HashExtension.Sha256(model.Password),
                    Phone = model.Phone,
                    Surname = model.Surname,
                    PasswordHashCode = Guid.NewGuid().ToString(),
                    UserType = UserType.Customer
                };
                _unitOfWork.Repository<User>().Add(entity);
                _unitOfWork.Save();

                //_emailSenderService.SendEmailAsync(model.Email, "Üyelik Aktivasyonu", $"<p>Merhaba {model.Name} {model.Surname}</p><p>Aşağıdaki linki tıklayarak aktivasyon işlemini gerçekleştiriniz.</p><p><a href='http://localhost:55991/account/activation?code={entity.PasswordHashCode}'>http://localhost:55991/account/activation?code={entity.PasswordHashCode}</a></p>");
            }
            catch (Exception)
            {

            }
            return result;
        }

        public UserProfileModel GetProfile()
        {
            var user = _unitOfWork.Repository<User>().FirstOrDefault(x => !x.Deleted && x.IsActive && x.Id == AuthContent.Current.UserId);
            if (user != null)
            {
                var model = new UserProfileModel
                {
                    EmailAddress = user.Email,
                    Name = user.Name,
                    Phone = user.Phone,
                    Surname = user.Surname
                };
                return model;
            }
            return null;
        }


        public ServiceResult UpdateProfile(UserProfileModel model)
        {
            var result = new ServiceResult { StatusCode = HttpStatusCode.OK };
            try
            {
                var user = _unitOfWork.Repository<User>()
                    .FirstOrDefault(x => !x.Deleted && x.IsActive && x.Id == AuthContent.Current.UserId);

                if (user == null)
                {
                    result.Message = "Müşteri bilgileri bulunamadı.";
                    result.StatusCode = HttpStatusCode.NotFound;
                    return result;
                }
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Phone = model.Phone;
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.StatusCode = HttpStatusCode.InternalServerError;
            }
            return result;
        }

        public ServiceResult ChangePassword(PasswordModel model)
        {
            var result = new ServiceResult { StatusCode = HttpStatusCode.OK };
            try
            {
                if (model.NewPassword != model.ReNewPassword)
                {
                    result.Message = "Yeni şifre ve yeni şifre tekrar alanları uyuşmamaktadır.";
                    result.StatusCode = HttpStatusCode.NotFound;
                    return result;
                }
                var user = _unitOfWork.Repository<User>()
                    .FirstOrDefault(x => !x.Deleted && x.IsActive && x.Id == AuthContent.Current.UserId);

                if (user == null)
                {
                    result.Message = "Müşteri bilgileri bulunamadı.";
                    result.StatusCode = HttpStatusCode.NotFound;
                    return result;
                }
                var oldPassword = HashExtension.Sha256(model.OldPassword);
                if (user.Password != oldPassword)
                {
                    result.Message = "Mevcut şifre bilgisi hatalıdır. Lütfen tekrar deneyiniz.";
                    result.StatusCode = HttpStatusCode.BadRequest;
                    return result;
                }
                var newPassword = HashExtension.Sha256(model.NewPassword);
                user.Password = newPassword;
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.StatusCode = HttpStatusCode.InternalServerError;
            }
            return result;
        }
    }
}
