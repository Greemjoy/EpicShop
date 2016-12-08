using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class AdminController : Controller
    {
        IGuitarRepository repository;

        public AdminController(IGuitarRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Guitars);
        }

        public ViewResult Edit(int guitarId)
        {
            Guitar book = repository.Guitars.FirstOrDefault(b => b.GuitarId == guitarId);

            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(Guitar guitar)
        {
            if (ModelState.IsValid)
            {
                repository.SaveGuitar(guitar);
                TempData["message"] = string.Format("Changing guitar info \"{0}\" сохранены", guitar.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(guitar);
            }
        }
    }
}