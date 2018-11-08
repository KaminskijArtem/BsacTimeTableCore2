using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BsacTimeTableCore2.Models;
using BsacTimeTableCore2.Data;
using BsacTimeTableCore2.Services;

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
        
        public IActionResult DetailsWeek(int id)
        {
            IAcessoryService service = new AcessoryService();
            ViewData["currWeek"] = service.GetCurrentWeek();
            ViewData["lectureName"] = (from p in _context.Lecturers
                                     where p.Id == id
                                     select p.Name).First();

            var records = (from r in _context.Records
                           join l in _context.Groups on r.GroupId equals l.Id
                           join s in _context.Subjects on r.SubjectId equals s.Id
                           join c in _context.Classrooms on r.ClassroomId equals c.Id
                           where (r.Id == id)

                           //    && (r.DateTo >= DateTime.Today && r.DateFrom <= DateTime.Today)
                           orderby r.WeekDay, r.SubjOrdinalNumber, r.WeekNumber
                           select new LectureRecordViewModel
                            {
                                IdRecord = r.Id,
                                WeekDay = r.WeekDay,
                                WeekNumber = r.WeekNumber,
                                GroupName = l.Name,
                                SubjectName = s.AbnameSubject,
                                SubjOrdinalNumber = r.SubjOrdinalNumber,
                                Classroom = c.Name + " (к." + c.Building + ")",
                                IdSubjectType = r.SubjectTypeId
                            }
            ).ToList();

            return View(records);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}
