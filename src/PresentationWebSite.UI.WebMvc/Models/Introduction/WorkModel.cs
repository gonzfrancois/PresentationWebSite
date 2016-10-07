using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PresentationWebSite.Dal.Model;
using PresentationWebSite.UI.WebMvc.Models.Common;

namespace PresentationWebSite.UI.WebMvc.Models.Introduction
{
    public class WorkModel : IntroductionModelBase
    {
        public override IntroductionChildTab ActiveTab =>IntroductionChildTab.Job;
        public int Id { get; set; }
        public int DisplayPriority { get; set; }

        public virtual ICollection<TextModel> Texts { get; set; }
        public virtual Job Job { get; set; }
    }
}
