using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PresentationWebSite.Dal.Model;

namespace PresentationWebSite.UI.WebMvc.Models.Introduction
{
    public class SkillCategoriesModel : IntroductionModelBase
    {
        public override IntroductionChildTab ActiveTab => IntroductionChildTab.Skill;
        public IList<SkillCategory> Categories { get; set; }
    }
}
