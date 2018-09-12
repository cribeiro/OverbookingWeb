using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OverbookingWeb.Models
{
    public class ParameterModel
    {
        [Key]
        [DisplayName("Parameter Id")]
        public int ParameterId { get; set; }

        [Required]
        [DisplayName("Type")]
        public string Type { get; set; }

        [Required]
        [DisplayName("Value")]
        public string Value { get; set; }

        [Required]
        [DisplayName("Probability (%)"), Range(0, 100)]
        public int Probability { get; set; }

    }
}