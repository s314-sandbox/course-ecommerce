using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using evcommerce.Models;

namespace evcommerce.Controllers
{
    public class ItemsController : Controller
    {
        [HttpGet]
        public IActionResult Index(int category, int page = 1)
        {
            int categoryId = category;
            int pageNumber = page;

            ItemContext context = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.ItemContext)) as ItemContext;

            ViewBag.amount = context.GetItemCount(categoryId);
            ViewBag.category = categoryId;
            ViewBag.page = pageNumber;

            return View(context.GetAllItemsByCategory(categoryId, page));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            ItemContext context = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.ItemContext)) as ItemContext;
            ItemListViewModel item = context.GetItemInfo(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }
    }
}