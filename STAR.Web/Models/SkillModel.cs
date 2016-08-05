using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace STAR.Web.Models
{
    public class SkillModel
    {
        [Required]
        [DisplayName("Skill")]
        public string SkillName { get; set; }
        [StringLength(256)]
        public string Description { get; set; }
        public int ID { get; set; }
    }
}