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
        public async Task<IActionResult> DetailsParent(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parent = await _context.Parent
                .Include(p => p.IdentityUserId)
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
                parentChildJunction.ChildId = child.Id;
                parentChildJunction.ParentId = _context.Parent.Where(parent => parent.IdentityUserId == userId).FirstOrDefault().Id;
                _context.ParentChildJunction.Add(parentChildJunction);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", child.IdentityUserId);
            return View(child);
        }


        //GET: Parents/Details/[Parent]
        //public async Task<IActionResult> DetailsParent(int? id)

        //New method to add a chore
        //GET CreateChore
        [HttpGet]
        public ActionResult CreateChoreList(int? id)
        {
            ViewBag.Child_Id = id;
            return View();
        }

        //POST/CreateChore
        //Creates chore to be added to the list to be displayed. 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateChoreList([Bind("DayOfWeek,Reward,Title,Comment,ChildId")] ChoreList choreList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(choreList);
                await _context.SaveChangesAsync();
                //Redirect to the CreateChoreItem when it gets created instead of Index.
                return RedirectToAction("CreateChoreItem", new { id = choreList.Id });
            }

            return RedirectToAction(nameof(Index));
        }

        //New method to add a chore
        //GET CreateChore
        [HttpGet]
        public ActionResult CreateChoreItem(int? id)
        {
            ViewBag.ChoreListId = id;
            return View();
        }

        //POST/CreateChore
        //Creates chore to be added to the list to be displayed. 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateChoreItem([Bind("Name, isComplete,EndTime,Description,ChoreListId")] ChoreItem chore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chore);
                await _context.SaveChangesAsync();
                return RedirectToAction("CreateChoreItem", new { id = chore.ChoreListId});
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ViewChores(int? id)
        {
            //Using ChildId as a filter. Can only bring up 1 list per child. 
            //If want to bring up more than 1 list will have to filter by List and not Child. (Possible future implementation.)
            ViewBag.ChoreListId = id;
            var ViewChoreList = from s in _context.ChoreItem
                                        join st in _context.ChoreList on s.ChoreListId equals st.Id into st2
                                        from st in st2.DefaultIfEmpty()
                                        select new ChoreVM { ChoreItemVM = s, ChoreListVM = st };
            var FilterList = ViewChoreList.Where(c => c.ChoreListVM.ChildId == id);

            return View(FilterList);
        }



        //New method to add a chore
        //GET CreateChore
        //Parents version of the ChoreList. (ParentsTask)
        public ActionResult CreateReminder()
        {
            return View();
        }

        //POST/CreateChore
        //Creates chore to be added to the list to be displayed. 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReminder([Bind("Id, reminder")] ParentsTask reminder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reminder);
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


        public async Task<IActionResult> EditParent(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parent = await _context.Child.FindAsync(id);
            if (parent == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", parent.IdentityUserId);
            return View(parent);
        }

        // POST: Parents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditParent(int id, [Bind("Id,FirstName,LastName,IdentityUserId")] Parent parent)
        {
            if (id != parent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParentExists(parent.Id))
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
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", parent.IdentityUserId);
            return View(parent);
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
