using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PresentationWebSite.UI.WebMvc.Models.Introduction.SkillChart
{
    [DataContract]
    public class ChartPie
    {
        [DataMember(Name = "chart")]
        public CharPieInfo Infos { get; set; }
        [DataMember(Name = "category")]
        public IEnumerable<ChartPieSlice> Slices { get; set; }
    }
}