using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BsacTimeTableCore2.Data;
using BsacTimeTableCore2.Data.DBModels;

namespace BsacTimeTableCore2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GroupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GroupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Groups
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Groups.Include(m => m.Faculty);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Groups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mgroup = await _context.Groups
                .Include(m => m.Faculty)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (mgroup == null)
            {
                return NotFound();
            }

            return View(mgroup);
        }

        // GET: Admin/Groups/Create
        public IActionResult Create()
        {
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Id");
            return View();
        }

        // POST: Admin/Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,FacultyId")] Group mgroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mgroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Id", mgroup.FacultyId);
            return View(mgroup);
        }

        // GET: Admin/Groups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mgroup = await _context.Groups.SingleOrDefaultAsync(m => m.Id == id);
            if (mgroup == null)
            {
                return NotFound();
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Id", mgroup.FacultyId);
            return View(mgroup);
        }

        // POST: Admin/Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,FacultyId")] Group mgroup)
        {
            if (id != mgroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mgroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupExists(mgroup.Id))
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
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Id", mgroup.FacultyId);
            return View(mgroup);
        }

        // GET: Admin/Groups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mgroup = await _context.Groups
                .Include(m => m.Faculty)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (mgroup == null)
            {
                return NotFound();
            }

            return View(mgroup);
        }

        // POST: Admin/Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mgroup = await _context.Groups.SingleOrDefaultAsync(m => m.Id == id);
            _context.Groups.Remove(mgroup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupExists(int id)
        {
            return _context.Groups.Any(e => e.Id == id);
        }
    }
}
