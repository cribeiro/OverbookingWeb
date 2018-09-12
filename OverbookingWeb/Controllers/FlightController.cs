using OverbookingApp.Models;
using OverbookingWeb.Models;
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

        // GET: Flight
        public ActionResult Overbooking(int flightId)
        {
            FlightModel flight = repository.getFlightById(flightId);

            decimal datetimeProbability = 1.00m;
            for (int i = 0; i < repository.parameters.Count; i++)
            {
                if (repository.parameters[i].Type == "datetime" && repository.parameters[i].Value == flight.DateTime.ToString("dd/MM/yyyy HH:mm"))
                {
                    datetimeProbability = repository.parameters[i].Probability / 100.00m;
                    break;
                }
            }

            decimal fromToProbability = 1.00m;
            for (int j = 0; j < repository.parameters.Count; j++)
            {
                if (repository.parameters[j].Type == "fromto" && repository.parameters[j].Value == flight.FromTo)
                {
                    fromToProbability = repository.parameters[j].Probability / 100.00m ;
                    break;
                }
            }

            decimal acumulatedPassengerProbability = 0.00m;

            for (int k = 0; k < flight.passengers.Count; k++)
            {
                acumulatedPassengerProbability += probabilityOfAge(flight.passengers[k].PassengerAge, repository.parameters);
            }
        
            int expectedPassengers = (int) Math.Ceiling(datetimeProbability * fromToProbability * acumulatedPassengerProbability);

            ViewData["expectedPassengers"] = expectedPassengers;

            return View(flight);
        }

        private decimal probabilityOfAge(int age, List<ParameterModel> parameters)
        {
            decimal ageProbability = 1.00m;

            // TODO: split parameters in 3 diferent structures indexed
            // by values. So, we can access items in constant time.
            for (int l = 0; l < parameters.Count; l++)
            {
                if (parameters[l].Type == "age" && parameters[l].Value == age.ToString())
                {
                    ageProbability = parameters[l].Probability / 100.00m;
                    break;
                }

            }

            return ageProbability;

        }
    }

}
