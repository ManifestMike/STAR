using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Extensions;

namespace STAR.Domain {
    public class Contractor {
        public int ID { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        //[DisplayName("Primary Phone")]
        //public string PrimaryPhone { get; set; }
        //[DisplayName("Other Phone")]
        //public string OtherPhone { get; set; }
        //[DisplayName("Primary Email")]
        //public string PrimaryEmail { get; set; }
        //[DisplayName("Secondary Email")]
        //public string SecondaryEmail { get; set; }
        //[DisplayName("Notes")]
        //public string Notes { get; set; }

        public ICollection<Skill> Skills { get; set; } = new HashSet<Skill>();        
        
        //@Pre: takes a list of skill ids 
        //@Post: moves those skills to the front of the list within the Skills ICollection
        public void MoveToFront(List<int> list) {
            MoveToFrontHelper(list);
        }
        private void MoveToFrontHelper(List<int> list) { 
            var skillList = this.Skills.ToList();
            for (int i = 0; i < list.Count; i++) {
                int index = skillList.FindIndex(x => x.SkillId == list[i]);
                skillList.MoveElementToIndex(index, i);
                this.Skills = skillList;
            }
        }
    }
}