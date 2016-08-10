using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STAR.Domain {
    public class Job {

        public int JobId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public bool isFilled { get; set; } 
    }
}
