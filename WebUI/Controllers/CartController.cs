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
        public CartController(IGuitarRepository repo)
        {
            repository = repo;
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

    }
}