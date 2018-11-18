using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BsacTimeTableCore2.Data;
using BsacTimeTableCore2.Data.DBModels;
using Microsoft.AspNetCore.Authorization;

namespace BsacTimeTableCore2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RecordsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Records
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Records.Include(r => r.Classroom).Include(r => r.Group).Include(r => r.Lecturer).Include(r => r.Subject).Include(r => r.SubjectFor).Include(r => r.SubjectType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Records/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var record = await _context.Records
                .Include(r => r.Classroom)
                .Include(r => r.Group)
                .Include(r => r.Lecturer)
                .Include(r => r.Subject)
                .Include(r => r.SubjectFor)
                .Include(r => r.SubjectType)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (record == null)
            {
                return NotFound();
            }

            return View(record);
        }

        // GET: Admin/Records/Create
        public IActionResult Create()
        {
            ViewData["ClassroomId"] = new SelectList(_context.Classrooms, "Id", "Id");
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id");
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "Id", "Id");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Id");
            ViewData["SubjectForId"] = new SelectList(_context.Set<SubjectFor>(), "Id", "Id");
            ViewData["SubjectTypeId"] = new SelectList(_context.Set<SubjectType>(), "Id", "Id");
            return View();
        }

        // POST: Admin/Records/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WeekNumber,WeekDay,SubjOrdinalNumber,Date,ClassroomId,GroupId,LecturerId,SubjectForId,SubjectId,SubjectTypeId")] Record record)
        {
            if (ModelState.IsValid)
            {
                _context.Add(record);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassroomId"] = new SelectList(_context.Classrooms, "Id", "Id", record.ClassroomId);
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id", record.GroupId);
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "Id", "Id", record.LecturerId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Id", record.SubjectId);
            ViewData["SubjectForId"] = new SelectList(_context.Set<SubjectFor>(), "Id", "Id", record.SubjectForId);
            ViewData["SubjectTypeId"] = new SelectList(_context.Set<SubjectType>(), "Id", "Id", record.SubjectTypeId);
            return View(record);
        }

        // GET: Admin/Records/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var record = await _context.Records.SingleOrDefaultAsync(m => m.Id == id);
            if (record == null)
            {
                return NotFound();
            }
            ViewData["ClassroomId"] = new SelectList(_context.Classrooms, "Id", "Id", record.ClassroomId);
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id", record.GroupId);
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "Id", "Id", record.LecturerId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Id", record.SubjectId);
            ViewData["SubjectForId"] = new SelectList(_context.Set<SubjectFor>(), "Id", "Id", record.SubjectForId);
            ViewData["SubjectTypeId"] = new SelectList(_context.Set<SubjectType>(), "Id", "Id", record.SubjectTypeId);
            return View(record);
        }

        // POST: Admin/Records/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WeekNumber,WeekDay,SubjOrdinalNumber,Date,ClassroomId,GroupId,LecturerId,SubjectForId,SubjectId,SubjectTypeId")] Record record)
        {
            if (id != record.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(record);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecordExists(record.Id))
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
            ViewData["ClassroomId"] = new SelectList(_context.Classrooms, "Id", "Id", record.ClassroomId);
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id", record.GroupId);
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "Id", "Id", record.LecturerId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Id", record.SubjectId);
            ViewData["SubjectForId"] = new SelectList(_context.Set<SubjectFor>(), "Id", "Id", record.SubjectForId);
            ViewData["SubjectTypeId"] = new SelectList(_context.Set<SubjectType>(), "Id", "Id", record.SubjectTypeId);
            return View(record);
        }

        // GET: Admin/Records/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var record = await _context.Records
                .Include(r => r.Classroom)
                .Include(r => r.Group)
                .Include(r => r.Lecturer)
                .Include(r => r.Subject)
                .Include(r => r.SubjectFor)
                .Include(r => r.SubjectType)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (record == null)
            {
                return NotFound();
            }

            return View(record);
        }

        // POST: Admin/Records/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var record = await _context.Records.SingleOrDefaultAsync(m => m.Id == id);
            _context.Records.Remove(record);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecordExists(int id)
        {
            return _context.Records.Any(e => e.Id == id);
        }
    }
}
