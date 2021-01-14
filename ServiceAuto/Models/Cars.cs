using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ServiceAuto.Models
{
    public class Cars
    {
        [Key]
        public int IdCar { get; set; }
        public IdentityUser UserId { get; set; }
        [Required, RegularExpression(@"^[A-Z]{1,2}-\d{2,3}-[A-Z]{3}$"), Display(Name = "Registration Number")]
        public string RegistrationNumber { get; set; }
        [Required, MinLength(2), MaxLength(25)] 
        [RegularExpression(@"^([A-Za-z]\s*){2,10}$", ErrorMessage = "Make must not contain numbers and be greater than 2")]
        public string Make { get; set; }
        [Required, MinLength(1), MaxLength(25)]
        public string Model { get; set; }
        [Required, MinLength(1), MaxLength(10)]
        [RegularExpression(@"^([A-Za-z]\s*){2,10}$", ErrorMessage = "Fuel must not contain numbers and be greater than 2")]
        public string Fuel { get; set; }
        public virtual ICollection<Reservations> Reservations { get; set; }
        
    }
}