using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PresentationWebSite.Dal.Model
{
    public class Skill
    {
        public int Id { get; set; }
        [Required]
        public virtual SkillCategory Category { get; set; }
        public virtual ICollection<Text> Texts { get; set; }
        public int KnowledgePercent { get; set; }

    }
}