using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BsacTimeTableCore2.Models;
using BsacTimeTableCore2.Data;
using BsacTimeTableCore2.Services;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace BsacTimeTableCore2.Controllers
{
    public class LectureController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LectureController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string searchString, int? page)
        {
            ViewData["searchString"] = searchString;
            var lecturers = (from p in _context.Lecturers 
            select new LectureViewModel{IdLecturer = p.Id,  NameLecturer = p.Name});

            if (!String.IsNullOrEmpty(searchString))
            {
                lecturers = lecturers.Where(g => g.NameLecturer.Contains(searchString));
            }

            return View(PaginatedList<LectureViewModel>.Create(lecturers, page ?? 1, 10));
        }
        
        public IActionResult DetailsWeek(int id, string date)
        {
            DateTime dt;
            if (date == null)
            {
                dt = DateTime.Today;
                if (dt.DayOfWeek == DayOfWeek.Sunday)
                    dt = dt.AddDays(1);
            }
            else
                dt = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var dateFrom = dt.AddDays(1 - (int)dt.DayOfWeek);
            var dateTo = dt.AddDays(7 - (int)dt.DayOfWeek);

            ViewData["lectureName"] = _context.Lecturers.Where(p => p.Id == id).First().Name;

            var records = _context.Records.Where(r => (r.LecturerId == id) &&
                           (r.Date >= dateFrom && r.Date < dateTo))
                           .Include(x => x.Group)
                           .Include(x => x.Subject)
                           .Include(x => x.Classroom)
                           .Include(x => x.SubjectType)
                           .GroupBy(l => new { l.SubjectType, l.Date, l.Classroom, l.SubjOrdinalNumber, l.Subject })
                           .OrderBy(x => x.Key.Date).ThenBy(x => x.Key.SubjOrdinalNumber)
                           .Select(g => new LectureRecordViewModel
                           {
                               ClassroomName = g.Key.Classroom.Name,
                               Date = g.Key.Date,
                               GroupName = string.Join(", ", g.Select(i => i.Group.Name)),
                               SubjectName = g.Key.Subject.Name,
                               SubjectTypeName = g.Key.SubjectType.Name,
                               SubjOrdinalNumber = g.Key.SubjOrdinalNumber
                           })
                           .ToList();

            return View(records);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}
