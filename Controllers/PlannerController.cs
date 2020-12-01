using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlannerProject.Data;
using PlannerProject.Models;

namespace PlannerProject.Controllers
{
    public class PlannerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlannerController(ApplicationDbContext context)
        {
            _context = context;
        }
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
        public async Task<IActionResult> Create([Bind("Id, DayOfWeek, Reward, chores")] Planner planner)
        {
            if (ModelState.IsValid)
            {
                var model = new Planner();
                _context.Add(planner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public ActionResult CreateChore(int plannerId)
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> CreateChore([Bind("Name, isCompleted")] Chore chore)
        //{
        //    var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    _context.Add();
        //    await _context.SaveChangesAsync();
        //}

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
