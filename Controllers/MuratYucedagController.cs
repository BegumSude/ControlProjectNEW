using ControlProject.Context;
using ControlProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


namespace ControlProject.Controllers
{
    public class MuratYucedagController : Controller
    {
        public ControlContext db = new ControlContext();

        public ActionResult Index()
        {
            var teacher = db.Teachers.FirstOrDefault(t => t.Name == "Murat");
            var muratHocaProjects = db.StudentProjects
                .Where(sp => sp.Student.TeacherId == teacher.TeacherId)
                .ToList();

            var model = new ProjectCountsViewModel
            {
                ToplamProje = muratHocaProjects.Count(),
                TamamlananProje = muratHocaProjects.Count(p => p.Status == "Tamamlandı"),
                TamamlanmayanProje = muratHocaProjects.Count(p => p.Status == "Başlamadı" || p.Status == "Devam Ediyor")
            };

            return View(model);
        }

        public ActionResult Detail()
        {
            // İlgili öğretmeni bul
            var teacher = db.Teachers.FirstOrDefault(t => t.Name == "Murat");

            // Murat Hoca'nın projelerini al
            var muratHocaProjects = db.StudentProjects
                .Where(sp => sp.Student.TeacherId == teacher.TeacherId) // teacher.Id daha güvenlidir
                .ToList();

            // İstatistikleri hesapla
            var total = muratHocaProjects.Count();
            var completed = muratHocaProjects.Count(p => p.Status == "Tamamlandı");
            var notCompleted = total - completed;
            var completedPercentage = total > 0 ? (completed * 100) / total : 0;

            // ViewBag ile verileri gönder

            ViewBag.toplamProje = muratHocaProjects.Count();
            ViewBag.tamamlananProje = muratHocaProjects.Count(p => p.Status == "Tamamlandı");
            ViewBag.tamamlanmayanProje = muratHocaProjects.Count(p =>
            p.Status == "Başlamadı" || p.Status == "Devam Ediyor");

            return PartialView();
        }




        public ActionResult StudentTable()
        {
            ViewBag.studentCount = db.Students.Where(s => s.TeacherId == 1).Count();

            var data = db.StudentProjects
                .Include(sp => sp.Student)
                .Include(sp => sp.Project)
                .Where(sp => sp.Student.TeacherId == 1)
                .Select(sp => new StudentProjectsViewModel
                {
                    StudentId = sp.StudentId,
                    StudentName = sp.Student.Name,
                    SuccessRate = sp.Student.SuccessRate,
                    ProjectTitle = sp.Project.Title,
                    Status = sp.Status
                }).ToList();

            var projectTitles = db.Projects
        .OrderBy(p => p.ProjectId)
        .Take(7)
        .Select(p => p.Title)
        .ToList();

            ViewBag.ProjectTitles = projectTitles;

            // Öğrenci adına göre grupluyoruz
            var groupedData = data.GroupBy(d => d.StudentName).ToList();

            return PartialView(groupedData);
        }

        [HttpGet]
        public ActionResult UpdateProjectStatus(int studentId)
        {
            // Öğrenci bilgisi alınır
            var student = db.Students.FirstOrDefault(s => s.StudentId == studentId);
            if (student == null)
            {
                return HttpNotFound();
            }

            // StudentProjects tablosundaki veriler ve projeler ile ilişki kurarak liste alınır
            var projects = db.StudentProjects
                              .Where(sp => sp.StudentId == studentId)
                              .Join(db.Projects,  // Burada Project tablosu ile join yapıyoruz
                                    sp => sp.ProjectId,  // StudentProjects tablosunda ProjectId'yi kullanıyoruz
                                    p => p.ProjectId,    // Projects tablosunda ProjectId ile eşliyoruz
                                    (sp, p) => new UpdateProjectViewModel
                                    {
                                        StudentProjectId = sp.StudentProjectId,
                                        ProjectName = p.Title,  // Proje adı burada alınır
                                        Status = sp.Status,
                                        AvailableStatuses = new List<string> { "Tamamlandı", "Devam Ediyor", "Başlamadı" }
                                    })
                              .ToList();

            // Model oluşturulur
            var model = new ProjectStatusUpdateViewModel
            {
                StudentId = studentId,
                StudentName = student.Name,
                Projects = projects
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateProjectStatus(ProjectStatusUpdateViewModel model)
        {

            foreach (var project in model.Projects)
            {
                var studentProject = db.StudentProjects
                                       .FirstOrDefault(sp => sp.StudentProjectId == project.StudentProjectId);

                if (studentProject != null)
                {
                    studentProject.Status = project.Status;
                }
            }

            db.SaveChanges();

            // Durumları güncelledikten sonra, öğrenci projeleri sayfasına geri yönlendir
            return RedirectToAction("Index");

        }


    }
}