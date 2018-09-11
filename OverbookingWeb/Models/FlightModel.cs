using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OverbookingApp.Models
{
    public class FlightModel
    {
        [Key]
        [DisplayName("Flight Id")]
        public int FlightId { get; set; }

        [Required]
        [DisplayName("Leaving from / Going to")]
        public string FromTo { get; set; }

        [Required]
        [DisplayName("Departure date and time")]
        public DateTime DateTime { get; set; }

        [Required]
        [DisplayName("Company")]
        public string Company { get; set; }
    }
}