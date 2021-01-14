using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace ServiceAuto.Models
{
    public class Reservations
    {
        [Key]
        public int IdReservation { get; set; }
        [Required]
        public Cars Car { get; set; }
        [Required]
        public Services Service { get; set; }
        [Required, Display(Name = "Reservation date and hour")]
        public DateTime Date { get; set; }
        [Required]
        public virtual ICollection<Mechanics> Mechanics { get; set; }
    }
}