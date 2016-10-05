using System.Collections.Generic;

namespace PresentationWebSite.Dal.Model
{
    public class SkillCategory
    {
        public int Id { get; set; }
        public int DisplayPriority { get; set; }
        public virtual ICollection<Text> Texts { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }

    }
}