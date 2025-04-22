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
        public virtual Student Student { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public string Status { get; set; }

    }
}