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

        public IActionResult Open(int? id)
        {
            return View();
        }
    }
}