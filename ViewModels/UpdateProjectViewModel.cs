using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlProject.ViewModels
{
    public class UpdateProjectViewModel
    {
        public int StudentProjectId { get; set; }  // Proje ID'si
        public string ProjectName { get; set; }  // Proje adı
        public string Status { get; set; }  // Proje durumu
        public List<string> AvailableStatuses { get; set; }  // Durum seçenekleri
    }
}