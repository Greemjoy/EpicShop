using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class CartController : Controller
    {
        private IGuitarRepository repository;
        private IOrderProcessor orderProcessor;
        public CartController(IGuitarRepository repo, IOrderProcessor processor)
        {
            repository = repo;
            orderProcessor = processor;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart,int guitarId, string returnUrl)
        {
            Guitar guitar = repository.Guitars
                .FirstOrDefault(b => b.GuitarId == guitarId);

            if (guitar != null)
            {
                cart.AddItem(guitar, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int guitarId, string returnUrl)
        {
            Guitar guitar = repository.Guitars
                .FirstOrDefault(b => b.GuitarId == guitarId);

            if (guitar != null)
            {
                cart.RemoveLine(guitar);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }
        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if(cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, cart is empty.");
            }
            if (ModelState.IsValid)
            {
                orderProcessor.ProcessorOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(new ShippingDetails());
            }
        }

    }
}