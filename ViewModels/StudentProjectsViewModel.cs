using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlProject.ViewModels
{
    public class StudentProjectsViewModel
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public double SuccessRate { get; set; }
        public string ProjectTitle { get; set; }
        public string Status { get; set; }
    }
}