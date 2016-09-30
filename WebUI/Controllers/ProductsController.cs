using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private IProductRepository repository;
        public ProductsController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult List()
        {
            return View(repository.Products);
        }

    }
}