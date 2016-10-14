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

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }
        

        public Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public RedirectToRouteResult AddToCart(int guitarId, string returnUrl)
        {
            Guitar guitar = repository.Guitars
                .FirstOrDefault(b => b.GuitarId == guitarId);

            if (guitar != null)
            {
                GetCart().AddItem(guitar, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int guitarId, string returnUrl)
        {
            Guitar guitar = repository.Guitars
                .FirstOrDefault(b => b.GuitarId == guitarId);

            if (guitar != null)
            {
                GetCart().RemoveLine(guitar);
            }

            return RedirectToAction("Index", new { returnUrl });
        }
    }
}