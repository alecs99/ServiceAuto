using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Services.Description;
using ServiceAuto.Models;

namespace ServiceAuto.Controllers
{
    public class ServicesController : Controller
    {
        private ApplicationDbContext _context;

        public ServicesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Services
        public ActionResult Index()
        {
            var services = _context.Services;
            return View(services);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult NewService()
        {
            var service = new Services();
            return View(service);
        }

        [HttpPost]
        public ActionResult Save(Services service)
        {
            if (!ModelState.IsValid)
            {
                return View("NewService", service);
            }
            if (service.IdService == 0)
                _context.Services.Add(service);
            else
            {
                var serviceInDb = _context.Services.Single(s => s.IdService == service.IdService);

                serviceInDb.ServiceName = service.ServiceName;
                serviceInDb.ServicePrice = service.ServicePrice;
                serviceInDb.ExecutionTime = service.ExecutionTime;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Services");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var service = _context.Services.SingleOrDefault(s => s.IdService == id);
            if (service == null)
                return HttpNotFound();

            return View("NewService", service);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var service = _context.Services.SingleOrDefault(s => s.IdService == id);
            if (service == null)
            {
                return HttpNotFound();
            }
            _context.Services.Remove(service);
            _context.SaveChanges();
            return View("DeleteService");

        }
        [HttpPost]
        public ActionResult DeleteService()
        {
            return RedirectToAction("Index", "Services");
        }
    }
}