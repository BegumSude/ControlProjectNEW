using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlProject.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public string GrupAdi { get; set; }

        public int? TrainerId { get; set; }
        public virtual Trainer Trainer { get; set; }


      

    }
}