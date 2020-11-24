using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlannerProject.Data;

namespace PlannerProject.Controllers
{
    public class ChildController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChildController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: ChildController
        public ActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var applicationDbContext = _context.Child.Inclue(c => c.IdentityUser);
            return View();
        }

        // GET: ChildController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ChildController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChildController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ChildController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ChildController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ChildController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ChildController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
