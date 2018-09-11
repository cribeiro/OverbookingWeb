using OverbookingWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OverbookingWeb.Controllers
{
    public class PassengerController : Controller
    {
        private InMemoryRepository repository = new InMemoryRepository();

        // GET: Passenger/Create/1
        public ActionResult Create(int flightId)
        {
            PassengerModel newPassenger = new PassengerModel();
            newPassenger.FlightId = flightId;

            return View(newPassenger);
        }

        // POST: Passenger/Create
        [HttpPost]
        public ActionResult Create(PassengerModel passenger)
        {
            try
            {
                repository.Save(passenger);

                return RedirectToAction("Index", "Flight");
            }
            catch
            {
                return View();
            }
        }
    }
}
