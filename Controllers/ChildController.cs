using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PlannerProject.Data;
using PlannerProject.Models;

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
        public IActionResult Create()
        {
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: ChildController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,")] Child child)
        {
            if (ModelState.IsValid)
            {
                _context.Add(child);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View(child);
        }

        // GET: ChildController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ChildController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var child = await _context.Child.FindAsync(id);
            if (child == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", child.IdentityUserId);
            return View(child);
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
