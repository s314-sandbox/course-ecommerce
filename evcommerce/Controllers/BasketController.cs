using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using evcommerce.Models;

namespace evcommerce.Controllers
{
    public class BasketController : Controller
    {
        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            UserContext userContext = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.UserContext)) as UserContext;
            BasketContext context = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.BasketContext)) as BasketContext;
            User user = userContext.GetUser(User.Identity.Name);
            IEnumerable<evcommerce.Models.BasketPositionView> basketPositions = context.GetAllPositionsByUser(user.Id);
            ViewBag.totalCost = basketPositions.Sum(p => p.Cost * p.Amount);

            return View(basketPositions);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add([Bind(include: "Id")] Item item, int amount)
        {
            UserContext userContext = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.UserContext)) as UserContext;
            User user = userContext.GetUser(User.Identity.Name);

            if (ModelState.IsValid)
            {
                BasketContext context = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.BasketContext)) as BasketContext;
                context.AddPosition(item, amount, user.Id);
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return RedirectToAction("Index");
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, int amount)
        {
            UserContext userContext = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.UserContext)) as UserContext;
            User user = userContext.GetUser(User.Identity.Name);

            if (ModelState.IsValid)
            {
                BasketContext context = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.BasketContext)) as BasketContext;
                context.RemovePosition(id, user.Id);
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return RedirectToAction("Index");
        }
    }
}