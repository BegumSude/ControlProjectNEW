using ControlProject.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlProject.Services
{
    public class StudentSuccessService
    {
        private readonly ControlContext _context;

        public StudentSuccessService(ControlContext context)
        {
            _context = context;
        }

        public double CalculateSuccessRateForStudent(int studentId)
        {
            int totalProjects = 6; 

            var completedProjects = _context.StudentProjects
                .Where(sp => sp.StudentId == studentId && sp.Status == "Tamamlandı")
                .Count();

            double successRate = (double)completedProjects / totalProjects * 100;

            return Math.Round(successRate, 2);
        }

        public void UpdateStudentSuccessRate(int studentId)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentId == studentId);

            if (student != null)
            {
                student.SuccessRate = CalculateSuccessRateForStudent(studentId);
                _context.SaveChanges();
            }
        }
    }
}