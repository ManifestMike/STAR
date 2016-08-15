using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STAR.Domain {
    public class Position {

        public int PositionId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public int? contractorId { get; set; } 
    }
}
