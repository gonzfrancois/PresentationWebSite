using System;
using System.Collections.Generic;
using System.Globalization;
using PresentationWebSite.Dal.Model;
using PresentationWebSite.UI.WebMvc.Models.Common;

namespace PresentationWebSite.UI.WebMvc.Models.Introduction
{
    public class GraduationModel : IntroductionModelBase
    {
        public override IntroductionChildTab ActiveTab => IntroductionChildTab.Graduation;

        public DateTime ObtainingDateTime { get; set; }
        public IEnumerable<TextModel> Texts { get; set; }

    }
}