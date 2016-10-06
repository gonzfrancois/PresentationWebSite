using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
