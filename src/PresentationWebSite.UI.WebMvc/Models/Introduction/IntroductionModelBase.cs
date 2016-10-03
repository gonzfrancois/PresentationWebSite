using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationWebSite.UI.WebMvc.Models.Introduction
{
    public abstract class IntroductionModelBase
    {
        public enum IntroductionChildTab
        {
            Graduation,
            Job,
            Skill,
            Hobby
        }
        public abstract IntroductionChildTab ActiveTab { get; }
    }
}
