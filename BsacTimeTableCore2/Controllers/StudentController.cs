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
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string searchString, int? page)
        {
            ViewData["searchString"] = searchString;
            var groups = (from p in _context.Groups
                          orderby p.Id
                          select new GroupViewModel { IdGroup = p.Id, NameGroup = p.Name });

            if (!String.IsNullOrEmpty(searchString))
            {
                groups = groups.Where(g => g.NameGroup.Contains(searchString));
            }

            return View(PaginatedList<GroupViewModel>.Create(groups, page ?? 1, 10));
        }

        public IActionResult DetailsWeek(int idgroup, int subgroup)
        {
            IAcessoryService service = new AcessoryService();
            ViewData["currWeek"] = service.GetCurrentWeek();
            ViewData["subgroup"] = subgroup;
            ViewData["groupName"] = (from p in _context.Groups
                                     where p.Id == idgroup
                                     select p.Name).First();///sdfsdfsdffds

            var records = (from r in _context.Records
                           join l in _context.Lecturers on r.LecturerId equals l.Id
                           join s in _context.Subjects on r.SubjectId equals s.Id
                           join c in _context.Classrooms on r.ClassroomId equals c.Id
                           where (r.GroupId == idgroup) &&
                           new[] { subgroup, 3 }.Contains(r.SubjectForId)

                           //    && (r.DateTo >= DateTime.Today && r.DateFrom <= DateTime.Today)
                           orderby r.WeekDay, r.SubjOrdinalNumber, r.WeekNumber
                           select new StudentRecordViewModel
                            {
                                IdRecord = r.Id,
                                WeekDay = r.WeekDay,
                                WeekNumber = r.WeekNumber,
                                LectureName = l.Name,
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
