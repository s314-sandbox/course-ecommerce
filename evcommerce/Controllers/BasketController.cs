using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using evcommerce.Models;

namespace evcommerce.Controllers
{
    public class BasketController : Controller
    {
        [HttpGet]
        public IActionResult Index(int user) // TODO: Remove user hardcode
        {

            BasketContext context = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.BasketContext)) as BasketContext;
            IEnumerable<evcommerce.Models.BasketPositionView> basketPositions = context.GetAllPositionsByUser(user);
            ViewBag.totalCost = basketPositions.Sum(p => p.Cost);

            return View(basketPositions);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add([Bind(include: "Id")] Item item, int amount)
        {

            if (ModelState.IsValid)
            {
                BasketContext context = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.BasketContext)) as BasketContext;
                context.AddPosition(item, amount);
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, int amount)
        {

            if (ModelState.IsValid)
            {
                BasketContext context = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.BasketContext)) as BasketContext;
                context.RemovePosition(id);
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return RedirectToAction("Index");
        }
    }
}