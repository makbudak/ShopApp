using ShopApp.Data.GenericRepository;
using ShopApp.Extensions;
using ShopApp.Model.Dto;
using ShopApp.Model.Dto.Customer;
using ShopApp.Model.Entity;
using System;
using System.Net;

namespace ShopApp.Business.Services
{
    public interface ICustomerService
    {
        Customer GetById(int id);

        Customer GetByEmail(string email);

        bool IsEmailConfirmed(string email);

        ServiceResult Login(string email, string password);

        ServiceResult Register(RegisterModel model);

        CustomerProfileModel GetProfile();

        ServiceResult UpdateProfile(CustomerProfileModel model);

        ServiceResult ChangePassword(PasswordModel model);

    }

    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSenderService _emailSenderService;

        public CustomerService(IUnitOfWork unitOfWork,
            IEmailSenderService emailSenderService)
        {
            _unitOfWork = unitOfWork;
            _emailSenderService = emailSenderService;
        }

        public Customer GetByEmail(string email)
        {
            return _unitOfWork.Repository<Customer>()
                .Get(x => x.Email == email && !x.Deleted);
        }

        public Customer GetById(int id)
        {
            return _unitOfWork.Repository<Customer>().Get(x => x.Id == id);
        }

        public bool IsEmailConfirmed(string email)
        {
            var customer = GetByEmail(email);
            if (customer == null)
                return false;
            else
                return customer.EmailConfirmed;
        }

        public ServiceResult Login(string email, string password)
        {
            var result = new ServiceResult { StatusCode = HttpStatusCode.OK };
            string hashedPassword = HashExtension.Sha256(password);

            var customer = _unitOfWork.Repository<Customer>()
                .Get(x => !x.Deleted && x.EmailConfirmed && x.IsActive && x.Email == email && x.Password == hashedPassword);
            if (customer == null)
            {
                result.StatusCode = HttpStatusCode.NotFound;
                result.Message = "Email adresi veya şifre hatalıdır. Lütfen tekrar deneyiniz.";
                return result;
            }
            else
            {
                if (!customer.EmailConfirmed)
                {
                    result.StatusCode = HttpStatusCode.NotAcceptable;
                    result.Message = "Email adresi onaylanmamış. Lütfen girilen email adresine gelen link ile aktivasyon işlemini gerçekleştiriniz.";
                    return result;
                }
                result.Data = customer;
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

                var emailCheck = _unitOfWork.Repository<Customer>()
                    .Get(x => !x.Deleted && x.Email == model.Email);

                if (emailCheck != null)
                {
                    result.StatusCode = HttpStatusCode.Found;
                    result.Message = "Email adresiyle daha önce kullanıcı kaydedilmiş.";
                    return result;
                }

                var entity = new Customer
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
                    PasswordHashCode = Guid.NewGuid().ToString()
                };
                _unitOfWork.Repository<Customer>().Add(entity);
                _unitOfWork.Save();

                _emailSenderService.SendEmailAsync(model.Email, "Üyelik Aktivasyonu", $"<p>Merhaba {model.Name} {model.Surname}</p><p>Aşağıdaki linki tıklayarak aktivasyon işlemini gerçekleştiriniz.</p><p><a href='http://localhost:55991/account/activation?code={entity.PasswordHashCode}'>http://localhost:55991/account/activation?code={entity.PasswordHashCode}</a></p>");
            }
            catch (Exception)
            {

            }
            return result;
        }

        public CustomerProfileModel GetProfile()
        {
            var customer = _unitOfWork.Repository<Customer>().Get(x => !x.Deleted && x.IsActive && x.Id == CustomerAuthContent.Current.CustomerId);
            if (customer != null)
            {
                var model = new CustomerProfileModel
                {
                    EmailAddress = customer.Email,
                    Name = customer.Name,
                    Phone = customer.Phone,
                    Surname = customer.Surname
                };
                return model;
            }
            return null;
        }


        public ServiceResult UpdateProfile(CustomerProfileModel model)
        {
            var result = new ServiceResult { StatusCode = HttpStatusCode.OK };
            try
            {
                var customer = _unitOfWork.Repository<Customer>().Get(x => !x.Deleted && x.IsActive && x.Id == CustomerAuthContent.Current.CustomerId);
                if (customer == null)
                {
                    result.Message = "Müşteri bilgileri bulunamadı.";
                    result.StatusCode = HttpStatusCode.NotFound;
                    return result;
                }
                customer.Name = model.Name;
                customer.Surname = model.Surname;
                customer.Phone = model.Phone;
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
                var customer = _unitOfWork.Repository<Customer>().Get(x => !x.Deleted && x.IsActive && x.Id == CustomerAuthContent.Current.CustomerId);
                if (customer == null)
                {
                    result.Message = "Müşteri bilgileri bulunamadı.";
                    result.StatusCode = HttpStatusCode.NotFound;
                    return result;
                }
                var oldPassword = HashExtension.Sha256(model.OldPassword);
                if (customer.Password != oldPassword)
                {
                    result.Message = "Mevcut şifre bilgisi hatalıdır. Lütfen tekrar deneyiniz.";
                    result.StatusCode = HttpStatusCode.BadRequest;
                    return result;
                }
                var newPassword = HashExtension.Sha256(model.NewPassword);
                customer.Password = newPassword;
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
