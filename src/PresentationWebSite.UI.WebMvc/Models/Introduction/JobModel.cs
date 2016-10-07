using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PresentationWebSite.Dal.Model;
using PresentationWebSite.UI.WebMvc.Models.Common;

namespace PresentationWebSite.UI.WebMvc.Models.Introduction
{
    public class JobModel : IntroductionModelBase
    {
        public override IntroductionChildTab ActiveTab => IntroductionChildTab.Job;
        public DateTime StarterDate { get; set; }
        public bool IsNotActualJob { get; set; }
        public DateTime EndDate { get; set; }
        public virtual IList<TextModel> Texts { get; set; }
        public virtual IList<WorkModel> Works { get; set; }
    }
}
