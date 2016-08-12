using System.ComponentModel.DataAnnotations;

namespace STAR.Web.Models {
    public class PostContractorViewModel {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        
        public string SkillIds { get; set; }
    }
}