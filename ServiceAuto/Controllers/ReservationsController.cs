using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ServiceAuto.Models;
using ServiceAuto.ViewModels;

namespace ServiceAuto.Controllers
{
    [Authorize]
    public class ReservationsController : Controller
    {
        private ApplicationDbContext _context;

        public ReservationsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult NewReservation()
        {
            var servicesList = _context.Services.ToList();
            var userId = User.Identity.GetUserId();
            var car = _context.Cars.Single(c => c.UserId.Id == userId);
            var mechanicsList = _context.Mechanics.ToList();
            var viewModel = new NewReservationViewModel
            {
                Reservation = new Reservations(),
                Services = servicesList,
                Mechanics = mechanicsList,
                Car = car
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Save(NewReservationViewModel ReservationViewModel)
        {
            if (ReservationViewModel.Reservation.IdReservation == 0)
            {
                var userId = User.Identity.GetUserId();
                var car = _context.Cars.Single(c => c.UserId.Id == userId);
                ReservationViewModel.Reservation.Car = car;
                ReservationViewModel.Reservation.Mechanics = new List<Mechanics>();
                ReservationViewModel.Reservation.Service =
                    _context.Services.Single(s => s.IdService == ReservationViewModel.ServiceId);
                foreach (var mechanicID in ReservationViewModel.SelectedMechanics)
                {
                    var mechanic = _context.Mechanics.Single(m => m.IdMechanic == mechanicID);
                    ReservationViewModel.Reservation.Mechanics.Add(mechanic);
                }
                _context.Reservations.Add(ReservationViewModel.Reservation);
            }
            else
            {
                var reservationInDb = _context.Reservations.Single(r => r.IdReservation == ReservationViewModel.Reservation.IdReservation);

                reservationInDb.Date = ReservationViewModel.Reservation.Date;
                reservationInDb.Service = _context.Services.Single(s => s.IdService == ReservationViewModel.ServiceId);
                foreach (var mechanicID in ReservationViewModel.SelectedMechanics)
                {
                    var mechanic = _context.Mechanics.Single(m => m.IdMechanic == mechanicID);
                    reservationInDb.Mechanics.Add(mechanic);
                }
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Reservations");
        }
        // GET: Reservations
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                var reservations = _context.Reservations;
                return View(reservations);
            }
            else
            {
                var userId = User.Identity.GetUserId();
                try
                {
                    var reservations = _context.Reservations.Where(c => c.Car.UserId.Id == userId);
                    return View(reservations);
                }
                catch (System.InvalidOperationException)
                {
                    return View("NewReservation");
                }
            }
            
        }

        public ActionResult Edit(int id)
        {
            var reservation = _context.Reservations.SingleOrDefault(m => m.IdReservation == id);

            if (reservation == null)
                return HttpNotFound();

            var servicesList = _context.Services.ToList();
            var userId = User.Identity.GetUserId();
            var car = _context.Cars.Single(c => c.UserId.Id == userId);
            var mechanicsList = _context.Mechanics.ToList();

            var reservationModel = new NewReservationViewModel()
            {
                Reservation = reservation,
                Services = servicesList,
                Mechanics = mechanicsList,
                Car = car

            };
            return View("NewReservation", reservationModel);
        }
        public ActionResult Delete(int id)
        {
            var reservation = _context.Reservations.SingleOrDefault(m => m.IdReservation == id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            _context.Reservations.Remove(reservation);
            _context.SaveChanges();
            return View("DeleteReservation");

        }
        [HttpPost]
        public ActionResult DeleteReservation()
        {
            return RedirectToAction("Index", "Reservations");
        }

        public ActionResult Details(int id)
        {
            var reservation = _context.Reservations.Include(r => r.Car)
                .Include(r => r.Mechanics)
                .Include(r => r.Service)
                .Single(r => r.IdReservation == id);
            
            return View("Details", reservation);
        }
    }
}