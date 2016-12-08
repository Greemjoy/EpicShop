using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class NavController : Controller
    {
        private IGuitarRepository repository;
        public NavController(IGuitarRepository repo)
        {
            repository = repo;
        }
        public PartialViewResult Menu(string type = null)
        {
            ViewBag.SelectType = type;
            IEnumerable<string> types = repository.Guitars
                .Select(guitar => guitar.Type)
                .Distinct()
                .OrderBy(x => x);
            return PartialView("FlexMenu", types);
        }
    }
}