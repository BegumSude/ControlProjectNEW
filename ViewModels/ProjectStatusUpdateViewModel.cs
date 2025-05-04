using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlProject.ViewModels
{
    public class ProjectStatusUpdateViewModel
    {
        public int StudentId { get; set; }   // Öğrencinin ID'si
        public string StudentName { get; set; }  // Öğrencinin adı
        public List<UpdateProjectViewModel> Projects { get; set; }  // Öğrencinin projeleri
    }
}