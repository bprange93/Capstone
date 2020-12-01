using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlannerProject.Data;
using PlannerProject.Models;

namespace PlannerProject.Controllers
{
    public class ParentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParentController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: ParentController
        public async Task<ActionResult> Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var CurrentParent = _context.Parent.Where(e => e.IdentityUserId == userId).SingleOrDefault();
            if (CurrentParent == null)
            {
                return RedirectToAction("Create");
            }
            var parentChildJunction = _context.ParentChildJunction.Where(data => data.ParentId == CurrentParent.Id).ToList();
            var applicationDbContext = new List<Child>();
            if (parentChildJunction != null)
            {
                parentChildJunction.ForEach(junction =>
                {
                    var children = _context.Child.Where(child => child.Id == junction.ChildId).First();
                    applicationDbContext.Add(children);
                });
            }
            return View(applicationDbContext);
        }
        // GET: ParentController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parent = await _context.Parent
                .Include(e => e.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parent == null)
            {
                return NotFound();
            }

            return View(parent);
        }

        // GET: ParentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ParentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName")] Parent parent)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                parent.IdentityUserId = userId;
                _context.Add(parent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", parent.IdentityUserId);
            return View(parent);
        }

        public ActionResult CreateChild()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateChild([Bind("Id,FirstName,LastName,")] Child child)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Add(child);
                await _context.SaveChangesAsync();

                ParentChildJunction parentChildJunction = new ParentChildJunction();
                parentChildJunction.ChildId = _context.Child.ToList().Last().Id;
                parentChildJunction.ParentId = _context.Parent.Where(parent => parent.IdentityUserId == userId).ToList().First().Id;
                _context.ParentChildJunction.Add(parentChildJunction);
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", child.IdentityUserId);
            return View(child);
        }



        // GET: ParentController/Edit/5
        public ActionResult Edit(int? id)
        {
            return View();
        }

        // POST: ParentController/Edit/5
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

        // GET: ParentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ParentController/Delete/5
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
