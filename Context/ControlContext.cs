using ControlProject.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ControlProject.Context
{
    public class ControlContext : DbContext
    {

        public DbSet<Student> Students { get; set; }
        public DbSet<StudentProject> StudentProjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Project> Projects { get; set; }


    }
}