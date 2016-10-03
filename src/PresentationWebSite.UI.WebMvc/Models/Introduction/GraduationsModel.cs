using System.Collections;
using System.Collections.Generic;
using PresentationWebSite.Dal.Model;

namespace PresentationWebSite.UI.WebMvc.Models.Introduction
{
    public class GraduationsModel : IntroductionModelBase
    {
        public override IntroductionChildTab ActiveTab => IntroductionChildTab.Graduation;
        public IList<Grade> Graduations { get; set; }
    }
}