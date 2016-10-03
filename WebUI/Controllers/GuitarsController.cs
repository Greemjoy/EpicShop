using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class GuitarsController : Controller
    {
        private IGuitarRepository repository;
        public  GuitarsController(IGuitarRepository repo)
        {
            repository = repo;
        }
        public ViewResult List()
        {
            return View(repository.Guitars);
        }
    }
}