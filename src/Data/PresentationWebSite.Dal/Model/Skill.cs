using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PresentationWebSite.Dal.Model
{
    public class Skill
    {
        public int Id { get; set; }
        [Required]
        public int KnowledgePercent { get; set; }
        public virtual SkillCategory Category { get; set; }
        public virtual ICollection<Text> Texts { get; set; }
    }
}