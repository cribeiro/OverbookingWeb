using System;
using OverbookingApp.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web;


public class InMemoryRepository
{
    private List<FlightModel> flights = new List<FlightModel>();

    public InMemoryRepository()
    {
        if (HttpContext.Current.Session["flights"] != null)
        {
            flights = (List<FlightModel>)HttpContext.Current.Session["flights"];
        }
    }


    public int Save(Object obj)
    {

        if (obj is FlightModel)
        {
            FlightModel newFlight = (FlightModel)obj;
            newFlight.FlightId = flights.Count + 1;

            flights.Add(newFlight);
            HttpContext.Current.Session["flights"] = flights;

        }

        return 1;
    }

    public int getCount(string type)
    {
        int count = 0;
       
        if (type == "flight")
        {
            count = flights.Count;

        }

        return count;
        
    }

}
