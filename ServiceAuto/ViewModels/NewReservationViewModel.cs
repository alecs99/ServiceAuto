using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceAuto.Models;

namespace ServiceAuto.ViewModels
{
    public class NewReservationViewModel
    {
        public IEnumerable<Services> Services { get; set; }
        public IEnumerable<Mechanics> Mechanics { get; set; }
        public IList<int> SelectedMechanics { get; set; }
        public Cars Car { get; set; }
        public int ServiceId { get; set; }
        public Reservations Reservation { get; set; }

    }
}