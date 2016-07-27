using System.Collections.Generic;
using System.ComponentModel;

namespace STAR.Domain {
    public class Contractor {
        public int ID { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public ICollection<Skill> Skills { get; set; } = new HashSet<Skill>();
    }
}