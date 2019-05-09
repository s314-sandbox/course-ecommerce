using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using evcommerce.Models;

namespace evcommerce.Controllers
{
    public class OrderController : Controller
    {
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            UserContext userContext = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.UserContext)) as UserContext;
            BasketContext context = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.BasketContext)) as BasketContext;
            User user = userContext.GetUser(User.Identity.Name);
            List<BasketPositionView> basketPositions = context.GetAllPositionsByUser(user.Id);
            ViewBag.totalCost = basketPositions.Sum(p => p.Cost * p.Amount);

            DeliveryContext deliveryContext = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.DeliveryContext)) as DeliveryContext;
            PaymentContext paymentContext = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.PaymentContext)) as PaymentContext;
            AdressContext adressContext = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.AdressContext)) as AdressContext;

            List<Delivery> deliveries = deliveryContext.GetAllDeliveryTypes();
            List<Payment> payments = paymentContext.GetAllPaymentTypes();
            List<AdressListModel> adresses = adressContext.GetAllAdressesByUser(user.Id);
            ViewBag.ListOfDeliveries = deliveries;
            ViewBag.ListOfPayments = payments;
            ViewBag.ListOfAdresses = adresses.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Country + ", " + p.City + ", " + p.Street + ", д. " + p.House + ", кв. " + p.Flat
            });

            return View(new OrderCreateModel { BasketPositionView = basketPositions });
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(OrderCreateModel model)
        {
            UserContext userContext = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.UserContext)) as UserContext;
            User user = userContext.GetUser(User.Identity.Name);
            if (ModelState.IsValid)
            {
                BasketContext basketContext = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.BasketContext)) as BasketContext;
                OrderContext orderContext = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.OrderContext)) as OrderContext;
                OrderInfoContext infoContext = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.OrderInfoContext)) as OrderInfoContext;

                
                List<Basket> basketItems = basketContext.GetBasketContentByUser(user.Id);

                List<BasketPositionView> basketPositions = basketContext.GetAllPositionsByUser(user.Id);
                model.BasketPositionView = basketPositions;

                if (basketItems.Count > 0 && user != null)
                {
                    int lastId = infoContext.AddOrder(new OrderInfo
                    {
                        UserId = user.Id,
                        Date = DateTime.Now,
                        PaymentId = model.PaymentId,
                        DeliveryId = model.DeliveryId,
                        AdressId = model.AdressId
                    });

                    // Add order for each basket items
                    foreach (Basket item in basketItems)
                    {
                        orderContext.AddOrderedItem(new Order
                        {
                            OrderInfoId = lastId,
                            ItemId = item.ItemId,
                            Amount = item.Amount
                        });
                    }

                    foreach (Basket item in basketItems)
                    {
                        basketContext.RemovePosition(item.Id, user.Id);
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Корзина пуста");
            }


            DeliveryContext deliveryContext = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.DeliveryContext)) as DeliveryContext;
            PaymentContext paymentContext = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.PaymentContext)) as PaymentContext;
            AdressContext adressContext = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.AdressContext)) as AdressContext;

            List<Delivery> deliveries = deliveryContext.GetAllDeliveryTypes();
            List<Payment> payments = paymentContext.GetAllPaymentTypes();
            List<AdressListModel> adresses = adressContext.GetAllAdressesByUser(user.Id);
            ViewBag.ListOfDeliveries = deliveries;
            ViewBag.ListOfPayments = payments;
            ViewBag.ListOfAdresses = adresses.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Country + ", " + p.City + ", " + p.Street + ", д. " + p.House + ", кв. " + p.Flat
            });

            return View(model);
        }


        [Authorize]
        [HttpGet]
        public IActionResult Details(int? id)
        {
            UserContext userContext = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.UserContext)) as UserContext;
            OrderContext orderContext = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.OrderContext)) as OrderContext;
            OrderInfoContext infoContext = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.OrderInfoContext)) as OrderInfoContext;

            User user = userContext.GetUser(User.Identity.Name);
            OrderInfo order = infoContext.GetOrderInfo(id);

            if (user != null && (order.UserId == user.Id || User.IsInRole("admin")))
            {
                OrderInfoEntryModel entryModel = infoContext.GetOrderEntry(order.Id);
                entryModel.OrderedItemModel = orderContext.GetOrderedItemsByInfoId(order.Id);
                ViewBag.TotalCost = infoContext.GetTotalPriceForOrderInfo(order.Id);

                return View(entryModel);
            }
            return Forbid();
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult List()
        {
            OrderInfoContext infoContext = HttpContext.RequestServices.GetService(typeof(evcommerce.Models.OrderInfoContext)) as OrderInfoContext;

            List<AdminOrderListEntryModel> listEntries = infoContext.GetAllOrders();
            return View(listEntries);
        }
    }
}