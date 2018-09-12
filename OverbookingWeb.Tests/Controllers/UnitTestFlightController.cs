using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using OverbookingApp.Models;
using OverbookingWeb.Models;
using System.IO;
using System.Web.SessionState;
using System.Reflection;
using OverbookingApp.Controllers;
using System.Web.Mvc;

namespace OverbookingWeb.Tests.Controllers
{
    [TestClass]
    public class UnitTestFlightController
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
        public void TestOverbooking()
        {
            InMemoryRepository repository = new InMemoryRepository();

            // given VCP-GRU has 95% chance
            ParameterModel route = new ParameterModel();
            route.Type = "fromto";
            route.Value = "VCP-GRU";
            route.Probability = 95;
            repository.Save(route);

            // and given every 32 years old person has 92% chance to flight
            ParameterModel age32 = new ParameterModel();
            age32.Type = "age";
            age32.Value = "32";
            age32.Probability = 92;
            repository.Save(age32);

            // and given every 40 years old person has 98% chance to flight
            ParameterModel age40 = new ParameterModel();
            age40.Type = "age";
            age40.Value = "40";
            age40.Probability = 98;
            repository.Save(age40);

            // and given flights scheduled for 01/01/2018 12:00 has 80% chance
            ParameterModel date = new ParameterModel();
            date.Type = "datetime";
            date.Value = "01/01/2018 12:00";
            date.Probability = 80;
            repository.Save(date);

            // and given we have a flight scheduled in the route VCP-GRU for 01/01/2018 12:00
            FlightModel flight = new FlightModel();
            flight.Company = "Latam airlines";
            flight.FromTo = "VCP-GRU";
            flight.DateTime = DateTime.Parse("2018-01-01 12:00");
            repository.Save(flight);

            // and given we sold tickets to 150 32 years old people
            // and 150 40 years old people
            for (int i = 0; i < 150; i++) {
                PassengerModel passenger32 = new PassengerModel();
                passenger32.PassengerFullName = "John #" + i;
                passenger32.PassengerAge = 32;
                passenger32.FlightId = 1;
                repository.Save(passenger32);

                PassengerModel passenger40 = new PassengerModel();
                passenger40.PassengerFullName = "Mary #" + i;
                passenger40.PassengerAge = 40;
                passenger40.FlightId = 1;
                repository.Save(passenger40);

            }

            var controller = new FlightController();
            var result = controller.Overbooking(1) as ViewResult;
            // then we should have a overbooking of only 17 seats
            Assert.AreEqual(result.ViewData["expectedPassengers"], 217);

        }
    }
}
