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
            SubCategoryContext context = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.SubCategoryContext)) as SubCategoryContext;

            return View(context.GetAllSubCategories(parent));
        }
    }
}