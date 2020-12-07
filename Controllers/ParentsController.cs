using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlannerProject.Data;
using PlannerProject.Models;

namespace PlannerProject.Controllers
{
    public class ParentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Parents
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var CurrentParent = _context.Parent.Where(e => e.IdentityUserId == userId).SingleOrDefault();          

            //ParentChildJunction parentChild = new ParentChildJunction();
            
            if (CurrentParent == null)
            {
                return RedirectToAction(nameof(Create));
            }
            //ViewData["FirstName, LastName"] = new SelectList(_context.Users, "ChildId", "ParentId");
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

        // GET: Parents/Details/[Child]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var child = await _context.Child
                .Include(p => p.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (child == null)
            {
                return NotFound();
            }

            return View(child);
        }

        //GET: Parents/Details/[Parent]
        //
        public async Task<IActionResult> DetailsParent(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parent = await _context.Parent
                .Include(p => p.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parent == null)
            {
                return NotFound();
            }

            return View(parent);
        }

        // GET: Parents/Create
        public IActionResult Create()
        {
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Parents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,IdentityUserId")] Parent parent)
        {
            if (ModelState.IsValid)
            {
                var ParentId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                parent.IdentityUserId = ParentId;

                _context.Add(parent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", parent.IdentityUserId);
            return View(parent);
        }

        //New Method CreateChild
        //GET CreateChild
        public ActionResult CreateChild()
        {
            return View();
        }

        //POST/CreateChild
        //Creates child and adds to junction table connecting them to their parent. 
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

        //New method to add a chore
        //GET CreateChore
        public ActionResult CreateChore()
        {
            return View();
        }

        //POST/CreateChore
        //Creates chore to be added to the list to be displayed. 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateChore([Bind("Name, isComplete")] Chore chore)
        {
            if (ModelState.IsValid) {
                _context.Add(chore);
                await _context.SaveChangesAsync();
            }

            return View();
        }

        

                // GET: Parents/Edit/5
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

        // POST: Parents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,IdentityUserId")] Child child)
        {
            if (id != child.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(child);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParentExists(child.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", child.IdentityUserId);
            return View(child);
        }

        // GET: Parents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parent = await _context.Parent
                .Include(p => p.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parent == null)
            {
                return NotFound();
            }

            return View(parent);
        }

        // POST: Parents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parent = await _context.Parent.FindAsync(id);
            _context.Parent.Remove(parent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParentExists(int id)
        {
            return _context.Parent.Any(e => e.Id == id);
        }
    }
}
