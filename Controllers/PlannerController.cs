using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PlannerProject.Controllers
{
    public class PlannerController : Controller
    {
        // GET: Planner
        public ActionResult Index()
        {
            return View();
        }

        // GET: Planner/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Planner/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Planner/Create
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

        // GET: Planner/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Planner/Edit/5
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

        // GET: Planner/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Planner/Delete/5
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
