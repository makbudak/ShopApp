using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Services;
using ShopApp.Extensions;
using ShopApp.Model.Dto;
using ShopApp.Model.Dto.User;
using System.Threading.Tasks;

namespace ShopApp.WebUI.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private ICustomerService _customerService;
        private IEmailSenderService _emailSender;
        private ICartService _cartService;
        public AccountController(ICartService cartService,
            ICustomerService customerService, IEmailSenderService emailSender)
        {
            _cartService = cartService;
            _customerService = customerService;
            _emailSender = emailSender;
        }
        public IActionResult Login(string ReturnUrl = null)
        {
            return View(new LoginModel()
            {
                ReturnUrl = ReturnUrl
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var customer = _customerService.GetByEmail(model.Email);

            if (customer == null)
            {
                ModelState.AddModelError("", "Bu kullanıcı adı ile daha önce hesap oluşturulmamış");
                return View(model);
            }

            if (!_customerService.IsEmailConfirmed(model.Email))
            {
                ModelState.AddModelError("", "Lütfen email hesabınıza gelen link ile üyeliğinizi onaylayınız.");
                return View(model);
            }

            var check = _customerService.LoginControl(model.Email, model.Password);
            if (check)
            {
                return Redirect(model.ReturnUrl ?? "~/");
            }

            ModelState.AddModelError("", "Girilen kullanıcı adı veya parola yanlış");
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new UserModel()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email
            };

            //var result = await _userManager.CreateAsync(user, model.Password);
            //if (result.Succeeded)
            //{
            //    // generate token
            //    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //    var url = Url.Action("ConfirmEmail", "Account", new
            //    {
            //        userId = user.Id,
            //        token = code
            //    });

            //    // email
            //    await _emailSender.SendEmailAsync(model.Email, "Hesabınızı onaylayınız.", $"Lütfen email hesabınızı onaylamak için linke <a href='https://localhost:5001{url}'>tıklayınız.</a>");
            //    return RedirectToAction("Login", "Account");
            //}

            ModelState.AddModelError("", "Bilinmeyen hata oldu lütfen tekrar deneyiniz.");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            //await _signInManager.SignOutAsync();
            TempData.Put("message", new AlertMessage()
            {
                Title = "Oturum Kapatıldı.",
                Message = "Hesabınız güvenli bir şekilde kapatıldı.",
                AlertType = "warning"
            });
            return Redirect("~/");
        }

        public async Task<IActionResult> ConfirmEmail(int customerId, string token)
        {
            if (customerId == 0 || token == null)
            {
                TempData.Put("message", new AlertMessage()
                {
                    Title = "Geçersiz token.",
                    Message = "Geçersiz Token",
                    AlertType = "danger"
                });
                return View();
            }
            var customer = _customerService.GetById(1);
            if (customer != null)
            {
                //var result = await _userManager.ConfirmEmailAsync(user, token);
                //if (result.Succeeded)
                //{
                //    // cart objesini oluştur.
                //    _cartService.InitializeCart(user.Id);

                //    TempData.Put("message", new AlertMessage()
                //    {
                //        Title = "Hesabınız onaylandı.",
                //        Message = "Hesabınız onaylandı.",
                //        AlertType = "success"
                //    });
                //    return View();
                //}
            }
            TempData.Put("message", new AlertMessage()
            {
                Title = "Hesabınızı onaylanmadı.",
                Message = "Hesabınızı onaylanmadı.",
                AlertType = "warning"
            });
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                return View();
            }

            //var user = await _userManager.FindByEmailAsync(Email);

            //if (user == null)
            //{
            //    return View();
            //}

            //var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            //var url = Url.Action("ResetPassword", "Account", new
            //{
            //    userId = user.Id,
            //    token = code
            //});

            // email
            await _emailSender.SendEmailAsync(Email, "Reset Password", $"Parolanızı yenilemek için linke <a href='https://localhost:5001'>tıklayınız.</a>");

            return View();
        }

        public IActionResult ResetPassword(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Home", "Index");
            }

            var model = new ResetPasswordModel { Token = token };

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //var user = await _userManager.FindByEmailAsync(model.Email);
            //if (user == null)
            //{
            //    return RedirectToAction("Home", "Index");
            //}

            //var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

            //if (result.Succeeded)
            //{
            //    return RedirectToAction("Login", "Account");
            //}

            return View(model);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}