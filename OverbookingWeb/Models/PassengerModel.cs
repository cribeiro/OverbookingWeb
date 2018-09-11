using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OverbookingWeb.Models
{
    public class PassengerModel
    {

        [Required]
        [DisplayName("Flight Id")]
        public int FlightId { get; set; }

        [Required]
        [DisplayName("Passenger Full Name")]
        public String PassengerFullName { get; set; }

        [Required]
        [DisplayName("Passenger Age"), Range(0, 100)]
        public int PassengerAge { get; set; }

    }
}