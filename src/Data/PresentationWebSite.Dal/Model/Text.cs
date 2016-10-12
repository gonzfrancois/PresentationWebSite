using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PresentationWebSite.Dal.Model
{
    public class Text
    {
        public int Id { get; set; }
        public string Value { get; set; }
        [Required]
        public virtual Language Language { get; set; }

        public virtual ICollection<Grade> Graduations { get; set; }
        public virtual ICollection<SkillCategory> SkillCategories { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<Work> Works { get; set; }
        public virtual ICollection<Hobby> Hobbies { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUser { get; set; }
       
    }
}
