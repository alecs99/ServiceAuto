using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ServiceAuto.Models
{
    public class Mechanics
    {
        [Key]
        public int IdMechanic { get; set; }
        [Required, MinLength(2), MaxLength(21)]
        [RegularExpression(@"^([A-Za-z]\s*){2,20}$", ErrorMessage = "Name must contain only characters and be greater then 2")]
        public string LastName { get; set; }
        [Required, MinLength(2), MaxLength(21)] 
        [RegularExpression(@"^([A-Za-z]\s*){2,20}$", ErrorMessage = "Name must contain only characters and be greater then 2")]
        public string FirstName { get; set; }
        [Required, IntegerValidator(MinValue = 1)] 
        [RegularExpression(@"[0-9]{2,5}$", ErrorMessage = "Salary must contain only numbers")]
        public float Salary { get; set; }
        [IntegerValidator(MinValue = 0)]
        public float Bonus { get; set; }
        public virtual ICollection<Reservations> Reservations { get; set; }
    }
}