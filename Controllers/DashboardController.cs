using ControlProject.Context;
using ControlProject.Services;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ControlProject.Entities;

namespace ControlProject.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        ControlContext _context= new ControlContext();
        public ActionResult Index()
        {
            if (Session["currentUser"] == null)
            {
                return RedirectToAction("Index", "Login");
            }


            var total = _context.StudentProjects.Count();
            var completed = _context.StudentProjects.Count(p => p.Status == "Tamamlandı");
            ViewBag.CompletedPercentage = total > 0 ? (completed * 100) / total : 0;

            var studentCount = _context.Students.Count();
            var projectCount = _context.Projects.Count();
            var totalExpected = studentCount * projectCount;

            var completedCount = _context.StudentProjects
                .Where(sp => sp.Status == "Tamamlandı")
                .Count();

            var notCompletedCount = totalExpected - completedCount;

            ViewBag.CompletedCount = completedCount;
            ViewBag.NotCompletedCount = notCompletedCount;
            ViewBag.TotalExpectedProjects = totalExpected;

            ViewBag.CompletedPercentage = totalExpected > 0
                ? (completedCount * 100) / totalExpected
                : 0;


            return View();
        }

        public ActionResult DashboardTable()
        {

            var students = _context.Students
        .Include(x => x.Teacher)
        .ToList();

            var successService = new StudentSuccessService(_context);

            foreach (var student in students)
            {
                student.SuccessRate = successService.CalculateSuccessRateForStudent(student.StudentId);
            }

            _context.SaveChanges(); // Güncellediğimiz başarı oranlarını veritabanına kaydetsin

            var topStudents = students
                .OrderByDescending(s => s.SuccessRate)
                .Take(15)
                .ToList();

            return PartialView(topStudents);
        }


        public ActionResult ChartsD() 
        {
            var total = _context.StudentProjects.Count();
            var completed = _context.StudentProjects.Count(p => p.Status == "Tamamlandı");
            ViewBag.CompletedPercentage = total > 0 ? (completed * 100) / total : 0;

            var studentCount = _context.Students.Count();
            var projectCount = _context.Projects.Count();
            var totalExpected = studentCount * projectCount;

            var completedCount = _context.StudentProjects
                .Where(sp => sp.Status == "Tamamlandı")
                .Count();

            var notCompletedCount = totalExpected - completedCount;

            ViewBag.CompletedCount = completedCount;
            ViewBag.NotCompletedCount = notCompletedCount;
            ViewBag.TotalExpectedProjects = totalExpected;

            ViewBag.CompletedPercentage = totalExpected > 0
                ? (completedCount * 100) / totalExpected
                : 0;


            return PartialView();
            
        }
        
        public ActionResult Cards() 
        {
           
            ViewBag.StudentCount = _context.Students.Count(); 
            ViewBag.TeacherCount = _context.Teachers.Count();
            ViewBag.ProjectCount = _context.Projects.Count();
           

            return PartialView();
           
        }


    }
}