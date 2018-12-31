using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BsacTimeTableCore2.Models;
using BsacTimeTableCore2.Data;
using BsacTimeTableCore2.Services;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

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

        public IActionResult DetailsWeek(int idgroup, int subgroup, string date)
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

            ViewData["date"] = dt.ToString("dd/MM/yyyy");
            ViewData["subgroup"] = subgroup;
            ViewData["groupName"] = _context.Groups.Where(p => p.Id == idgroup).First().Name; 

            var records = _context.Records.Where(r => (r.GroupId == idgroup) &&
                           new[] { subgroup, 3 }.Contains(r.SubjectForId) &&
                           (r.Date >= dateFrom && r.Date < dateTo))
                           .Include(x => x.Lecturer)
                           .Include(x => x.Subject)
                           .Include(x => x.Classroom)
                           .Include(x => x.SubjectType)
                           .OrderBy(x => x.Date).ThenBy(x => x.SubjOrdinalNumber)
                           .ToList();

            return View(records);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
