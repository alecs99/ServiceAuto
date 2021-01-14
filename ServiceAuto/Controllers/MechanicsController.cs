using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceAuto.Models;

namespace ServiceAuto.Controllers
{

    public class MechanicsController : Controller
    {
        private ApplicationDbContext _context;

        public MechanicsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Mechanics
        public ActionResult Index()
        {
            var mechanics = _context.Mechanics;
            return View(mechanics);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult NewMechanic()
        {
            var mechanic = new Mechanics();
            return View(mechanic);
        }
        [HttpPost]
        public ActionResult Save(Mechanics mechanic)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return View("NewMechanic", mechanic);
            }
            if (mechanic.IdMechanic == 0)
                _context.Mechanics.Add(mechanic);
            else
            {
                var mechanicInDb = _context.Mechanics.Single(m => m.IdMechanic == mechanic.IdMechanic);

                mechanicInDb.LastName = mechanic.LastName;
                mechanicInDb.FirstName = mechanic.FirstName;
                mechanicInDb.Salary = mechanic.Salary;
                mechanicInDb.Bonus = mechanic.Bonus;

            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Mechanics");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var mechanic = _context.Mechanics.SingleOrDefault(m => m.IdMechanic == id);
            if (mechanic == null)
                return HttpNotFound();

            return View("NewMechanic", mechanic);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var mechanic = _context.Mechanics.SingleOrDefault(m => m.IdMechanic == id);
            if (mechanic == null)
            {
                return HttpNotFound();
            }
            _context.Mechanics.Remove(mechanic);
            _context.SaveChanges();
            return View("DeleteMechanic");

        }
        [HttpPost]
        public ActionResult DeleteMechanic()
        {
            return RedirectToAction("Index", "Mechanics");
        }
    }
}