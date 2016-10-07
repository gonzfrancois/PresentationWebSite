using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PresentationWebSite.UI.WebMvc.Models.Common;

namespace PresentationWebSite.UI.WebMvc.Models.Introduction
{
    public class AddWorkModel : IntroductionModelBase
    {
        public override IntroductionChildTab ActiveTab => IntroductionChildTab.Job;

        public int JobId { get; set; }
        public int DisplayPriority { get; set; }
        public IEnumerable<TextModel> Texts { get; set; }
    }
}
