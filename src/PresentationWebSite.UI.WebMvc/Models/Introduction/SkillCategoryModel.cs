using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PresentationWebSite.Dal.Model;
using PresentationWebSite.UI.WebMvc.Models.Common;

namespace PresentationWebSite.UI.WebMvc.Models.Introduction
{
    public class SkillCategoryModel : IntroductionModelBase
    {
        public override IntroductionChildTab ActiveTab => IntroductionChildTab.Skill;

        public int Id { get; set; }
        public int DisplayPriority { get; set; }
        public virtual IEnumerable<TextModel> Texts { get; set; }
        public virtual IEnumerable<SkillModel> Skills { get; set; }
  
    }
}
