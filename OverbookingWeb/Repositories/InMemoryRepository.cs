﻿using System;
using OverbookingApp.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web;
using OverbookingWeb.Models;

public class InMemoryRepository
{
    private List<FlightModel> flights = new List<FlightModel>();

    private List<ParameterModel> parameters = new List<ParameterModel>();

    public InMemoryRepository()
    {
        if (HttpContext.Current.Session["flights"] != null)
        {
            flights = (List<FlightModel>)HttpContext.Current.Session["flights"];
        }

        if (HttpContext.Current.Session["parameters"] != null)
        {
            parameters = (List<ParameterModel>)HttpContext.Current.Session["parameters"];
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
        else if (obj is ParameterModel)
        {
            ParameterModel newParameter = (ParameterModel)obj;
            newParameter.ParameterId = parameters.Count + 1;

            parameters.Add(newParameter);
            HttpContext.Current.Session["parameters"] = parameters;

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
        else if (type == "parameter")
        {
            count = parameters.Count;
        }

        return count;
        
    }

    public List<FlightModel> GetAllFlights()
    {
        return flights;
    }

}
