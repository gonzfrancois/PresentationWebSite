using System;
using System.Collections.Generic;
using System.Drawing;
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

        public ChartPie ChartDatas { get; set; }

        public IList<Color> Palette { get; set; } = new List<Color>()
        {
            //ColorTranslator.FromHtml("#f8bd19"),
            //ColorTranslator.FromHtml("#e44a00"),
            //ColorTranslator.FromHtml("#ccff66"),
            //ColorTranslator.FromHtml("#008ee4"),
            //ColorTranslator.FromHtml("#33bdda")
            ColorTranslator.FromHtml("#4285f4"),
            ColorTranslator.FromHtml("#34a853"),
            ColorTranslator.FromHtml("#fbbc05"),
            ColorTranslator.FromHtml("#f65314"),
            ColorTranslator.FromHtml("#ea4335")
        };
    }
}
