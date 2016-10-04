using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PresentationWebSite.Dal.Model;

namespace PresentationWebSite.UI.WebMvc.Models.Introduction
{
    public class GraduationModel : IntroductionModelBase
    {
        public override IntroductionChildTab ActiveTab => IntroductionChildTab.Graduation;

        public DateTime ObtainingDateTime { get; set; }
        public int TextKey { get; set; }
        private Dictionary<CultureInfo, string> _texts;

        public Dictionary<CultureInfo, string> Texts
        {
            get
            {
                if (_texts == null)
                {
                    return new Dictionary<CultureInfo, string>()
                    {
                        {new CultureInfo("fr-FR"), ""},
                        {new CultureInfo("es-ES"), ""},
                        {new CultureInfo("en-GB"), ""},
                        {new CultureInfo("ca-ES"), ""},
                    };
                }
                return _texts;
            }
            set { _texts = value; }
        }
    }
}