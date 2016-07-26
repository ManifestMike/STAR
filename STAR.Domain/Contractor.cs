using System.Collections.Generic;

namespace STAR.Domain {
    public class Contractor {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Skill> Skills { get; set; } = new HashSet<Skill>();
    }
}