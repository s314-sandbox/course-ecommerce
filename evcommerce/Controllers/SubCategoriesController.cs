using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using evcommerce.Models;

namespace evcommerce.Controllers
{
    public class SubCategoriesController : Controller
    {
        [HttpGet]
        public IActionResult Index(int? parent)
        {
            CategoryContext categoryContext = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.CategoryContext)) as CategoryContext;
            SubCategoryContext context = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.SubCategoryContext)) as SubCategoryContext;

            ViewBag.ParentName = categoryContext.GetCategory(parent).Name;

            return View(context.GetAllSubCategories(parent));
        }
    }
}