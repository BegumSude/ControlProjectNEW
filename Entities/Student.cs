using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlProject.Entities
{
    public class Student
    {
        public int StudentId { get; set; }
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public double SuccessRate { get; set; }

        public virtual Teacher Teacher { get; set; } // Navigation Property
        public virtual ICollection<StudentProject> StudentProjects { get; set; }



    }
}