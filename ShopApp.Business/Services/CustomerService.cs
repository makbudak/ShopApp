using ShopApp.Data.GenericRepository;
using ShopApp.Extensions;
using ShopApp.Model.Dto;
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
    }

    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IEmailSenderService _emailSenderService;

        public CustomerService(IUnitOfWork unitOfWork,
            IEmailSenderService emailSenderService)
        {
            _unitofwork = unitOfWork;
            _emailSenderService = emailSenderService;
        }

        public Customer GetByEmail(string email)
        {
            return _unitofwork.Repository<Customer>()
                .Get(x => x.Email == email && !x.Deleted);
        }

        public Customer GetById(int id)
        {
            return _unitofwork.Repository<Customer>().Get(x => x.Id == id);
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

            var customer = _unitofwork.Repository<Customer>()
                .Get(x => !x.Deleted && x.EmailConfirmed && x.IsActive && x.Email == email && x.Password == hashedPassword);
            if (customer == null)
            {
                result.StatusCode = HttpStatusCode.NotFound;
                result.Message = "Kullanıcı bulunamadı.";
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

                var emailCheck = _unitofwork.Repository<Customer>()
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
                _unitofwork.Repository<Customer>().Add(entity);
                _unitofwork.Save();

                _emailSenderService.SendEmailAsync(model.Email, "Üyelik Aktivasyonu", $"<p>Merhaba {model.Name} {model.Surname}</p><p>Aşağıdaki linki tıklayarak aktivasyon işlemini gerçekleştiriniz.</p><p><a href='http://localhost:55991/account/activation?code={entity.PasswordHashCode}'>http://localhost:55991/account/activation?code={entity.PasswordHashCode}</a></p>");
            }
            catch (Exception)
            {

            }
            return result;
        }
    }
}
