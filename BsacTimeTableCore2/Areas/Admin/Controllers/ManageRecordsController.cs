using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BsacTimeTableCore2.Data;
using BsacTimeTableCore2.Data.DBModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace BsacTimeTableCore2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ManageRecordsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ManageRecordsController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Name");

            return View();
        }

        public FileResult DownloadTTInDocx()
        {
            var path = _hostingEnvironment.WebRootPath + "\\documents\\emptyFile.docx";
            using (WordprocessingDocument wdDoc = WordprocessingDocument.Open(path, true))
            {
                wdDoc.MainDocumentPart.Document.RemoveAllChildren();
                wdDoc.Save();
            }

            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, "application/x-msdownload", "1.docx");
        }

        public async Task<IActionResult> Open(int? id, string date)
        {
            ViewBag.groupsJSON = JsonConvert.SerializeObject(
                await _context.Groups.Where(x => x.FacultyId == id).ToListAsync());

            DateTime _date = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (_date.DayOfWeek == 0)
                _date = _date.AddDays(-1);
            var dateFrom = _date.AddDays(1 - (int)_date.DayOfWeek);
            var dateTo = _date.AddDays(7 - (int)_date.DayOfWeek);
            ViewBag.Lecturers = new SelectList(_context.Lecturers, "Id", "Name");
            ViewBag.Subjects = new SelectList(_context.Subjects, "Id", "Name");
            var classrooms = _context.Classrooms.ToList();
            classrooms.ForEach(x => x.Name = x.Name + " (к." + x.Building + ")");
            ViewBag.Classrooms = new SelectList(classrooms, "Id", "Name");
            ViewBag.SubjectTypes = new SelectList(_context.SubjectTypes, "Id", "Name");
            ViewBag.DateFrom = dateFrom;

            return View();
        }

        // GET: Admin/Records/GetRecordsByGroupId
        public async Task<string> GetRecordsByGroupId(int id, string date)
        {
            var _date = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (_date.DayOfWeek == 0)
                _date = _date.AddDays(-1);
            var dateFrom = _date.AddDays(1 - (int)_date.DayOfWeek);
            var dateTo = _date.AddDays(7 - (int)_date.DayOfWeek);

            var recordsQuery1 = _context.Records
                .Where(x => x.Date >= dateFrom && x.Date < dateTo && x.GroupId == id && x.SubjectForId != 2)
                .OrderBy(x => x.Date).ThenBy(x => x.SubjOrdinalNumber)
                .Select(x => new
                {
                    x.LecturerId,
                    x.Lecturer.Name,
                    x.SubjectId,
                    x.Subject.AbnameSubject,
                    x.SubjectForId,
                    x.SubjOrdinalNumber,
                    x.Date,
                    x.ClassroomId,
                    ClassroomName = x.Classroom.Name + " (к." + x.Classroom.Building + ")",
                    x.SubjectTypeId
                });

            var recordsQuery2 = _context.Records
                .Where(x => x.Date >= dateFrom && x.Date < dateTo && x.GroupId == id && x.SubjectForId == 2)
                .OrderBy(x => x.Date).ThenBy(x => x.SubjOrdinalNumber)
                .Select(x => new
                {
                    x.LecturerId,
                    x.Lecturer.Name,
                    x.SubjectId,
                    x.Subject.AbnameSubject,
                    x.SubjectForId,
                    x.SubjOrdinalNumber,
                    x.Date,
                    x.ClassroomId,
                    ClassroomName = x.Classroom.Name + " (к." + x.Classroom.Building + ")",
                    x.SubjectTypeId
                });

            return JsonConvert.SerializeObject(new
            {
                groupId = id,
                recordsForAllAndFirstSubgroup = await recordsQuery1.ToListAsync(),
                recordsForSecondSubgroup = await recordsQuery2.ToListAsync()
            });
        }
    }
}