using G23NHNT.Models;
using G23NHNT.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using static G23NHNT.Models.Account;

namespace G23_NHNT.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            var user = await _accountRepository.LoginAsync(userName, password);
            if (user != null)
            {
                HttpContext.Session.SetInt32("UserId", user.IdUser);
                HttpContext.Session.SetString("UserName", user.UserName);
                HttpContext.Session.SetInt32("Role", user.Role);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role == 1 ? "ChuTro" : "NguoiTimPhong")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng!";
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Password != model.ConfirmPassword)
            {
                ViewBag.Error = "Mật khẩu xác nhận không khớp.";
                return View(model);
            }

            // Kiểm tra tên đăng nhập
            if (await _accountRepository.IsUserNameExistAsync(model.UserName))
            {
                ViewBag.Error = "Tên đăng nhập đã tồn tại.";
                return View(model);
            }
            var account = new Account
            {
                UserName = model.UserName,
                Password = model.Password,
                Role = model.Role,
                PhoneNumber = model.PhoneNumber,
            };
            bool isRegistered = await _accountRepository.RegisterAsync(account);

            if (isRegistered)
            {
                HttpContext.Session.SetString("UserName", model.UserName);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.UserName),
                    new Claim(ClaimTypes.Role, model.Role == 1 ? "ChuTro" : "NguoiTimPhong")
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToAction("Login", "Account");
            }

            ViewBag.Error = "Đăng ký thất bại.";
            return View(model);
        }
    }
}
