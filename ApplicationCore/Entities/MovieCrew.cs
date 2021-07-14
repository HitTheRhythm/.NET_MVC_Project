using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class MovieCrew
    {
        public int Department { get; set; }
        public string CrewId { get; set; }
        public string Name { get; set; }
        public int MovieId { get; set; }
        public  string Job { get; set; }
    }
}

