using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PresentationWebSite.UI.WebMvc.Models.Common;

namespace PresentationWebSite.UI.WebMvc.Models.Introduction
{
    public class AddSkillModel : IntroductionModelBase
    {
        public override IntroductionChildTab ActiveTab => IntroductionChildTab.Skill;
        public int CategoryId { get; set; }
        public IEnumerable<TextModel> Texts { get; set; }
        public int KnowledgePercent { get; set; }
    }
}
