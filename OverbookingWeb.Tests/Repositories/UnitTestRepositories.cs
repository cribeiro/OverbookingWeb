using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OverbookingApp.Models;

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
    }
}
