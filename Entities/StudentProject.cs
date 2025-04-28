using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlProject.Entities
{
    public class StudentProject
    {
        public int StudentProjectId { get; set; }
        public int StudentId { get; set; }
        public int ProjectId { get; set; }
        public string Status { get; set; }
    }

}
