using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using evcommerce.Models;

namespace evcommerce.Controllers
{
    public class AdressController : Controller
    {
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            CountryContext countryContext = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.CountryContext)) as CountryContext;
            List<Country> countries = countryContext.GetAllCountries();
            ViewBag.ListOfCountry = countries;

            return View();
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AdressCreateModel model)
        {
            if (ModelState.IsValid)
            {
                UserContext userContext = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.UserContext)) as UserContext;
                AdressContext adressContext = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.AdressContext)) as AdressContext;

                User user = userContext.GetUser(User.Identity.Name);
                if (user != null)
                {
                    // добавляем домашний адрес в бд
                    adressContext.AddAddress(new Adress
                    {
                        Name = "home",
                        CountryId = model.Country,
                        City = model.City,
                        Street = model.Street,
                        House = model.House,
                        Flat = model.Flat,
                        Info = model.Info,
                        UserId = user.Id
                    });

                    return RedirectToAction("Details", "Account");
                }
                else
                    ModelState.AddModelError("", "Неверно определен пользователь");
            }

            CountryContext countryContext = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.CountryContext)) as CountryContext;
            List<Country> countries = countryContext.GetAllCountries();
            ViewBag.ListOfCountry = countries;

            return View(model);
        }
    }
}