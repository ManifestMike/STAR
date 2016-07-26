using System.Collections.Generic;

namespace STAR.Domain {
    public class Skill {
        public int SkillId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Contractor> Contractors { get; set; } = new HashSet<Contractor>();
    }
}
