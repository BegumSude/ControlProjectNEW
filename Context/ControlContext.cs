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
        public DbSet<Team> Teams { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Project> Projects { get; set; }
   
    }
}