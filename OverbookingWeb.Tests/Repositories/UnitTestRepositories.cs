using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OverbookingApp.Models;
using OverbookingWeb.Models;

namespace OverbookingWeb.Tests.Repositories
{
    [TestClass]
    public class UnitTestRepositories
    {
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
            Assert.AreSame(savedFlights[0].FromTo, flight.FromTo);
            Assert.AreSame(savedFlights[0].DateTime, flight.DateTime);
            Assert.AreSame(savedFlights[0].Company, flight.Company);

            Assert.AreSame(savedFlights[1].FromTo, anotherFlight.FromTo);
            Assert.AreSame(savedFlights[1].DateTime, anotherFlight.DateTime);
            Assert.AreSame(savedFlights[1].Company, anotherFlight.Company);

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
