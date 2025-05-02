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
            var completed = _context.StudentProjects
                .Where(sp => sp.Status.Trim().ToLower() == "tamamlandı")
                .Select(sp => new { sp.StudentId, sp.ProjectId })
                .ToList();

            var teacherProjectCounts = _context.Teachers
                .ToList()
                .Select(t => new
                {
                    TeacherName = t.Name,
                    CompletedProjects = _context.Students
                        .Where(s => s.TeacherId == t.TeacherId)
                        .ToList()
                        .SelectMany(s => completed.Where(c => c.StudentId == s.StudentId))
                        .Count()
                })
                .OrderBy(t => t.TeacherName) // 🔁 Renk sırası için sabit sıralama
                .ToList();

            // 🔁 Aynı sırada olacak şekilde renkleri eşleştir
            var colors = new List<string>
            {
                "rgba(255, 204, 153, 0.6)", //  Abdullah Hoca
                "rgba(216, 191, 216, 0.6)", //  buse
                "rgba(176, 196, 222, 0.6)", //  Erhan
                "rgba(175, 238, 238, 0.6)", // fatih
                "rgba(135, 206, 250, 0.6)", //  Murat
                "rgba(255, 204, 229, 0.6)"  // Münire
            };

            // Label'lar ve datalar ViewBag'e atanıyor
            ViewBag.DoughnutLabels = teacherProjectCounts.Select(t => t.TeacherName).ToList();
            ViewBag.DoughnutData = teacherProjectCounts.Select(t => t.CompletedProjects).ToList();
            ViewBag.ChartColors = colors.Take(teacherProjectCounts.Count).ToList(); // renkleri sınırladık

            return PartialView();
        }


        public ActionResult Cards() 
        {
           
            ViewBag.StudentCount = _context.Students.Count(); 
            ViewBag.TeacherCount = _context.Teachers.Count();
            ViewBag.ProjectCount = _context.Projects.Count();

            return PartialView();
           
        } 
        public ActionResult ChartBar() 
        {
            // Öğrencilerin tamamladığı proje sayısı
            var completedStudentIds = _context.StudentProjects
                .Where(sp => sp.Status.Trim().ToLower() == "tamamlandı")
                .GroupBy(sp => sp.StudentId)
                .Where(g => g.Select(sp => sp.ProjectId).Distinct().Count() >= 2)
                .Select(g => g.Key) 
                .ToList();

            var teacherData = _context.Teachers
                .Select(t => new
                {
                    TeacherName = t.Name,
                    CompletedCount = _context.Students
                        .Where(s => s.TeacherId == t.TeacherId && completedStudentIds.Contains(s.StudentId))
                        .Count()
                })
                .OrderBy(t => t.TeacherName)
                .ToList();


            ViewBag.ChartColors = new List<string>
            {
                "rgba(255, 204, 153, 0.6)", //  Abdullah Hoca
                "rgba(216, 191, 216, 0.6)", //  buse
                "rgba(176, 196, 222, 0.6)", //  Erhan
                "rgba(175, 238, 238, 0.6)", // fatih
                "rgba(135, 206, 250, 0.6)", //  Murat
                "rgba(255, 204, 229, 0.6)"  // Münire
            };




            ViewBag.BarLabels = teacherData.Select(t => t.TeacherName).ToList();
            ViewBag.BarData = teacherData.Select(t => t.CompletedCount).ToList();
        
            return PartialView();
           
        }

        public ActionResult MiniCards()
        {

            //Progress Bar
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

            //Günün Sözü
            string filePath = Server.MapPath("~/Assets/Text/motivation.txt");
            string[] quotes = System.IO.File.ReadAllLines(filePath, System.Text.Encoding.UTF8);
            int dayOfYear = DateTime.Now.DayOfYear;
            int index = dayOfYear % quotes.Length;
            ViewBag.Motivation = quotes[index];



            


            return PartialView();
        }

       


    }
}