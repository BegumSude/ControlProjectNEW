using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlProject.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Project> Projects { get; set; }
    }
}