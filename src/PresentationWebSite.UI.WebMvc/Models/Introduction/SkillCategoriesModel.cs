using System.Collections.Generic;
using System.Drawing;
using PresentationWebSite.Dal.Model;
using PresentationWebSite.UI.WebMvc.Models.Introduction.SkillChart;

namespace PresentationWebSite.UI.WebMvc.Models.Introduction
{
    public class SkillCategoriesModel : IntroductionModelBase
    {
        public override IntroductionChildTab ActiveTab => IntroductionChildTab.Skill;
        public IList<SkillCategory> Categories { get; set; }

        public ChartPie ChartDatas { get; set; }

        public IList<Color> Palette { get; set; } = new List<Color>()
        {
            ColorTranslator.FromHtml("#4285f4"),
            ColorTranslator.FromHtml("#34a853"),
            ColorTranslator.FromHtml("#fbbc05"),
            ColorTranslator.FromHtml("#f65314"),
            ColorTranslator.FromHtml("#ea4335")
        };
    }
}
