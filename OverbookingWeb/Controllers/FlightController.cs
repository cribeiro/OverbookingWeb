using OverbookingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OverbookingApp.Controllers
{
    public class FlightController : Controller
    {

        private InMemoryRepository repository = new InMemoryRepository();

        // GET: Flight
        public ActionResult Index()
        {
            return View(repository.GetAllFlights());
        }

        // GET: Flight/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(new FlightModel());
        }

        // POST: Flight/Create
        [HttpPost]
        public ActionResult Create(FlightModel flight)
        {
            try
            {
                repository.Save(flight);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
