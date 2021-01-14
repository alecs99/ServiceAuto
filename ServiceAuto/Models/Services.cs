using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ServiceAuto.Models
{
    public class Services
    {
        [Key]
        public int IdService { get; set; }

        [Required, MinLength(2), MaxLength(21)] 
        [RegularExpression(@"^([A-Za-z]\s*){2,20}$", ErrorMessage = "Name must contain only characters and be greater then 2"), Display(Name = "Name of the service")]
        public string ServiceName { get; set; }
        [Required, IntegerValidator(MinValue = 1)] 
        [RegularExpression(@"[0-9]{2,5}$", ErrorMessage = "Price must contain only numbers"), Display(Name = "Price")]
        public float ServicePrice { get; set; }
        [Required, IntegerValidator(MinValue = 10)] 
        [RegularExpression(@"[0-9]{1,5}$", ErrorMessage = "Execution time must contain only numbers"), Display(Name="Execution time in minutes")]
        public int ExecutionTime { get; set; }
    }
}