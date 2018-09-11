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
        [DisplayName("Parameter"),
            RegularExpression(@"^(age|fromto|datetime)$", ErrorMessage = "Please, choose 'age', 'fromto' or 'datetime'.")]
        public string Parameter { get; set; }

        [Required]
        [DisplayName("Value")]
        public string Value { get; set; }

        [Required]
        [DisplayName("Probability"), Range(0.00, 1.00)]
        public decimal Probability { get; set; }

    }
}