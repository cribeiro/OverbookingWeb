using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OverbookingWeb.Models
{
    public class ParameterController : Controller
    {
        private InMemoryRepository repository = new InMemoryRepository();

        // GET: Parameter/Create
        public ActionResult Create()
        {
            return View(new ParameterModel());
        }

        // POST: Parameter/Create
        [HttpPost]
        public ActionResult Create(ParameterModel parameter)
        {
            try
            {
                repository.Save(parameter);

                return RedirectToAction("Index", "Flight");
            }
            catch
            {
                return View();
            }
        }

        
    }
}
