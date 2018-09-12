using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OverbookingApp.Models;
using OverbookingWeb.Models;
using System.IO;
using System.Web.SessionState;
using System.Reflection;

namespace OverbookingWeb.Tests.Repositories
{
    [TestClass]
    public class UnitTestRepositories
    {

        [TestInitialize]
        public void SetupTests()
        {
            // Mocking HttpContext            
            var httpRequest = new HttpRequest("", "http://localhost/", "");

            var httpResponce = new HttpResponse(new StringWriter());

            var httpContext = new HttpContext(httpRequest, httpResponce);
            var sessionContainer =
                new HttpSessionStateContainer("id",
                                               new SessionStateItemCollection(),
                                               new HttpStaticObjectsCollection(),
                                               10,
                                               true,
                                               HttpCookieMode.AutoDetect,
                                               SessionStateMode.InProc,
                                               false);
            httpContext.Items["AspSession"] =
                typeof(HttpSessionState)
                .GetConstructor(
                                    BindingFlags.NonPublic | BindingFlags.Instance,
                                    null,
                                    CallingConventions.Standard,
                                    new[] { typeof(HttpSessionStateContainer) },
                                    null)
                .Invoke(new object[] { sessionContainer });

            HttpContext.Current = httpContext;
        }

        [TestMethod]
        public void TestSaveFlight()
        {   
            InMemoryRepository repository = new InMemoryRepository();
            Assert.AreEqual(repository.getCount("flight"), 0);

            FlightModel flight = new FlightModel();
            flight.FromTo = "GRU-JFK";
            flight.DateTime = DateTime.Parse("2018-01-01 23:59");
            flight.Company = "American Airlines";
            repository.Save(flight);

            Assert.AreEqual(repository.getCount("flight"), 1);  
            
        }

        [TestMethod]
        public void TestGetAllFlights()
        {
            InMemoryRepository repository = new InMemoryRepository();

            FlightModel flight = new FlightModel();
            flight.FromTo = "GRU-JFK";
            flight.DateTime = DateTime.Parse("2018-01-01 23:59");
            flight.Company = "American Airlines";
            repository.Save(flight);

            FlightModel anotherFlight = new FlightModel();
            anotherFlight.FromTo = "VCP-GRU";
            anotherFlight.DateTime = DateTime.Parse("2018-02-05 13:50");
            anotherFlight.Company = "Latam";
            repository.Save(anotherFlight);

            List<FlightModel> savedFlights = repository.GetAllFlights();
            Assert.AreEqual(savedFlights[0].FromTo, flight.FromTo);
            Assert.AreEqual(savedFlights[0].DateTime, flight.DateTime);
            Assert.AreEqual(savedFlights[0].Company, flight.Company);

            Assert.AreEqual(savedFlights[1].FromTo, anotherFlight.FromTo);
            Assert.AreEqual(savedFlights[1].DateTime, anotherFlight.DateTime);
            Assert.AreEqual(savedFlights[1].Company, anotherFlight.Company);

        }

        [TestMethod]
        public void TestSaveParameter()
        {
            InMemoryRepository repository = new InMemoryRepository();
            Assert.AreEqual(repository.getCount("parameter"), 0);

            ParameterModel parameter = new ParameterModel();
            parameter.Type = "age";
            parameter.Value = "32";
            parameter.Probability = 98;
            repository.Save(parameter);

            Assert.AreEqual(repository.getCount("parameter"), 1);
        }

        [TestMethod]
        public void TestSavePassenger()
        {
            InMemoryRepository repository = new InMemoryRepository();

            FlightModel flight = new FlightModel();
            flight.FromTo = "GRU-JFK";
            flight.DateTime = DateTime.Parse("2018-01-01 23:59");
            flight.Company = "American Airlines";
            repository.Save(flight);

            Assert.AreEqual(repository.getNumberOfPassengers(1), 0);

            PassengerModel passenger = new PassengerModel();
            passenger.PassengerFullName = "Jhon Doe";
            passenger.PassengerAge = 32;
            passenger.FlightId = 1;
            repository.Save(passenger);

            Assert.AreEqual(repository.getNumberOfPassengers(1), 1);
        }

    }
}
