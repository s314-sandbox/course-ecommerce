using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using evcommerce.Models;

namespace evcommerce.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Role"] = ClaimsIdentity.DefaultRoleClaimType;
            CategoryContext context = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.CategoryContext)) as CategoryContext;

            return View(context.GetAllCategories());
        }


        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            CategoryContext context = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.CategoryContext)) as CategoryContext;
            Category category = context.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind(include: "Name")] Category category)
        {

            if (ModelState.IsValid)
            {
                CategoryContext context = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.CategoryContext)) as CategoryContext;
                context.AddCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CategoryContext context = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.CategoryContext)) as CategoryContext;
            Category category = context.GetCategory(id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            CategoryContext context = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.CategoryContext)) as CategoryContext;
            context.DeleteCategory(id);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CategoryContext context = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.CategoryContext)) as CategoryContext;
            Category category = context.GetCategory(id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind]Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                CategoryContext context = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.CategoryContext)) as CategoryContext;
                context.UpdateCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }
    }

}