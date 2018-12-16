﻿using System;
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

        public async Task<IActionResult> Open(int? id, string date)
        {
            DateTime _date = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (_date.DayOfWeek == 0)
                _date = _date.AddDays(-1);
            var dateFrom = _date.AddDays(1-(int)_date.DayOfWeek);
            var dateTo = _date.AddDays(7-(int)_date.DayOfWeek);
            ViewBag.Lecturers = new SelectList(_context.Lecturers, "Id", "Name");
            ViewBag.Subjects = new SelectList(_context.Subjects, "Id", "Name");
            var classrooms = _context.Classrooms.ToList();
            classrooms.ForEach(x => x.Name = x.Name + " (к." + x.Building + ")");
            ViewBag.Classrooms = new SelectList(classrooms, "Id", "Name");
            ViewBag.SubjectTypes = new SelectList(_context.SubjectTypes, "Id", "Name");
            ViewBag.DateFrom = dateFrom;

            var listGroups = await _context.Groups.Where(x => x.FacultyId == id)
                .Include(r => r.Records)
                .Include("Records.Lecturer")
                .Include("Records.Subject")
                .Include("Records.Classroom")
                .ToListAsync();
            listGroups.ForEach(x => x.Records = x.Records.Where(r => r.Date >= dateFrom && r.Date < dateTo).ToList());//TODO: Refactor to custom join
            listGroups = SetUpRecords(listGroups, dateFrom);
            return View(listGroups);
        }

        [HttpPost]
        public IActionResult Open(List<Group> groups)
        {
            var deletingRecords = new List<Record>();
            var insertingRecords = new List<Record>();
            var updatingRecords = new List<Record>();
            foreach (var g in groups)
            {
                var ir = g.Records.Where(x => (x.Id == 0 && x.IsChanged != null));
                insertingRecords = insertingRecords.Concat(ir).ToList();
                var ur = g.Records.Where(x => x.Id != 0 && x.IsChanged != null && x.IsDeleted == null);
                updatingRecords = updatingRecords.Concat(ur).ToList();
                var dl = g.Records.Where(x => x.Id != 0 && x.IsDeleted != null);
                deletingRecords = deletingRecords.Concat(dl).ToList();
            }
            insertingRecords.ForEach(x => x.IsChanged = null);
            updatingRecords.ForEach(x => x.IsChanged = null);
            _context.Records.AddRange(insertingRecords);
            _context.UpdateRange(updatingRecords);
            _context.RemoveRange(deletingRecords);
            var s = _context.SaveChanges();
            return Redirect(Request.Path + Request.QueryString);
        }

        private List<Group> SetUpRecords(List<Group> listGroups, DateTime dt)
        {
            foreach (var g in listGroups)
            {
                for (var i = 1; i < 6; i++)
                {
                    for (var j = 1; j < 8; j++)
                    {
                        if (!g.Records.Where(x => (x.SubjOrdinalNumber == j && (int)x.Date.DayOfWeek == i)).Any())
                        {
                            g.Records.Add(new Record { GroupId = g.Id, SubjectForId = 3, SubjOrdinalNumber = j, Date = dt.AddDays(i - 1) });
                        }
                    }
                }
                g.Records = g.Records.OrderBy(x => x.Date.Date).ThenBy(x => x.SubjOrdinalNumber).ToList();
            }

            return listGroups;
        }
    }
}