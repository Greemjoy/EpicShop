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
        public int pageSize = 4;
        public  GuitarsController(IGuitarRepository repo)
        {
            repository = repo;
        }
        public ViewResult List(int page = 1)
        {
            return View(repository.Guitars
                .OrderBy(guitar => guitar.GuitarId)
                .Skip((page - 1)*pageSize)
                .Take(pageSize));
        }
    }
}