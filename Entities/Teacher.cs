﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlProject.Entities
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<Student> Students { get; set; }

    }
}