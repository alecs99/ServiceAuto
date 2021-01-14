using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ServiceAuto.Models;

namespace ServiceAuto.Controllers
{
    [Authorize]
    public class CarsController : Controller
    {
        private ApplicationDbContext _context;

        public CarsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult NewCar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(Cars car)
        {
            var userId = User.Identity.GetUserId();
            IdentityUser loggedUser = _context.Users.Single(u => u.Id == userId);
            car.UserId = loggedUser;
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return View("NewCar", car);
            }
            if (car.IdCar == 0)
            {
                _context.Cars.Add(car);
            }
            else
            {
                var carInDb = _context.Cars.Single(m => m.IdCar == car.IdCar);

                carInDb.RegistrationNumber = car.RegistrationNumber;
                carInDb.Make = car.Make;
                carInDb.Model = car.Model;
                carInDb.Fuel = car.Fuel;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Cars");
        }
        public ActionResult Edit(int id)
        {
            var car = _context.Cars.SingleOrDefault(c => c.IdCar == id);
            if (car == null)
                return HttpNotFound();

            return View("NewCar", car);

        }
        public ActionResult Delete(int id)
        {
            var car = _context.Cars.SingleOrDefault(c => c.IdCar == id);
            if (car == null)
            {
                return HttpNotFound();
            }
            _context.Cars.Remove(car);
            _context.SaveChanges();
            return View("DeleteCar");

        }
        [HttpPost]
        public ActionResult DeleteCar()
        {
            return RedirectToAction("Index", "Cars");
        }
        // GET: Cars
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            IdentityUser loggedUser = _context.Users.Single(u => u.Id == userId);
            try
            {
                Cars car = _context.Cars.Single(c => c.UserId.Id == loggedUser.Id);
                return View(car);
            }
            catch (System.InvalidOperationException)
            {
                var car = new Cars()
                {
                    UserId = loggedUser,
                };
                return View("NewCar", car);
            }
        }
    }
}