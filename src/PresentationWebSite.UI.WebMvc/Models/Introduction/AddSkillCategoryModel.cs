using System.Collections.Generic;
using PresentationWebSite.UI.WebMvc.Models.Common;

namespace PresentationWebSite.UI.WebMvc.Models.Introduction
{
    public class AddSkillCategoryModel : IntroductionModelBase
    {
        public override IntroductionChildTab ActiveTab => IntroductionChildTab.Skill;

        public int Id { get; set; }
        public int DisplayPriority { get; set; }
        public virtual IEnumerable<TextModel> Texts { get; set; }
        public virtual IEnumerable<SkillModel> Skills { get; set; }
  
    }
}
