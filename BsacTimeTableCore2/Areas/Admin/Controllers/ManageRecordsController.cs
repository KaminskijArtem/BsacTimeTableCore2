using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BsacTimeTableCore2.Data;
using BsacTimeTableCore2.Data.DBModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BsacTimeTableCore2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ManageRecordsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ManageRecordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Name");

            return View();
        }

        public async Task<IActionResult> Open(int? id)
        {
            ViewBag.Lecturers = new SelectList(_context.Lecturers, "Id", "Name");
            ViewBag.Subjects = new SelectList(_context.Subjects, "Id", "Name");
            ViewBag.Classrooms = new SelectList(_context.Classrooms, "Id", "Name");
            ViewBag.SubjectTypes = new SelectList(_context.SubjectTypes, "Id", "Name");

            var listGroups = await _context.Groups.Where(x => x.FacultyId == id)
                .Include(r => r.Records)
                .Include("Records.Lecturer")
                .Include("Records.Subject")
                .Include("Records.Classroom")
                .ToListAsync();
            listGroups = SetUpRecords(listGroups);
            return View(listGroups);
        }

        [HttpPost]
        public IActionResult Open(List<Group> groups, int? id)
        {
            var insertingRecords = new List<Record>();
            var updatingRecords = new List<Record>();
            foreach (var g in groups)
            {
                var ir = g.Records.Where(x => (x.Id == 0 && x.IsChanged != null));
                insertingRecords = insertingRecords.Concat(ir).ToList();
                var ur = g.Records.Where(x => x.Id != 0 && x.IsChanged != null);
                updatingRecords = updatingRecords.Concat(ur).ToList();
            }
            insertingRecords.ForEach(x => x.IsChanged = null);
            updatingRecords.ForEach(x => x.IsChanged = null);
            _context.Records.AddRange(insertingRecords);
            _context.UpdateRange(updatingRecords);
            var s = _context.SaveChanges();
            return Redirect(Request.Path + Request.QueryString);
        }

        private List<Group> SetUpRecords(List<Group> listGroups)
        {
            foreach (var g in listGroups)
            {
                for (var i = 1; i < 6; i++)
                {
                    for (var j = 1; j < 7; j++)
                    {
                        if (!g.Records.Where(x => (x.SubjOrdinalNumber == j && (int)x.Date.DayOfWeek == i)).Any())
                        {
                            g.Records.Add(new Record { SubjOrdinalNumber = j, Date = new DateTime(2018, 11, 18).AddDays(i) });
                        }
                    }
                }
                g.Records = g.Records.OrderBy(x => x.Date.Date).ThenBy(x => x.SubjOrdinalNumber).ToList();
            }

            return listGroups;
        }
    }
}