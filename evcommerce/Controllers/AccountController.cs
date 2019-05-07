using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using evcommerce.Models;

namespace evcommerce.Controllers
{
    public class AccountController : Controller
    {
        private UserContext db;

        public AccountController(UserContext context)
        {
            db = context;
        }


        [Authorize]
        public IActionResult Details()
        {
            UserContext context = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.UserContext)) as UserContext;
            AdressContext adressContext = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.AdressContext)) as AdressContext;

            User user = context.GetUser(User.Identity.Name);
            AccountDetailsModel viewModel = new AccountDetailsModel();

            viewModel.Name = user.Name;
            viewModel.Surname = user.Surname;
            viewModel.Phone = user.Phone;
            viewModel.Mail = user.Mail;
            viewModel.Login = user.Login;
            viewModel.AdressListModel = adressContext.GetAllAdressesByUser(user.Id);

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = db.GetUser(model.Login, model.Password);
                if (user != null)
                {
                    await Authenticate(model.Login, user.IsAdmin);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и/или пароль");
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = db.GetUser(model.Login);
                if (user == null)
                {
                    // добавляем пользователя в бд
                    db.AddUser(new User {
                        Name = model.Name,
                        Surname = model.Surname,
                        Phone = model.Phone,
                        Mail = model.Mail,
                        Login = model.Login,
                        Password = model.Password,
                        IsAdmin = false
                    });

                    await Authenticate(model.Login, false); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }


        private async Task Authenticate(string userName, bool admin)
        {
            string role = "user";
            if (admin) role = "admin";
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}