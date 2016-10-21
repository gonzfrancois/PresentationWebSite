using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using PresentationWebSite.UI.WebMvc.Helpers.Extensions;

namespace PresentationWebSite.UI.WebMvc.Models.Introduction.SkillChart
{
    [DataContract()]
    public class ChartPieSlice
    {
        private Color? _color;

        [DataMember(Name = "label")]
        public string Label { get; set; }

        [DataMember(Name = "color", EmitDefaultValue = false)]
        public string Color
        {
            get { return _color?.GetHexValue(); }
            set { _color = string.IsNullOrEmpty(value) ? (Color?)null : ColorTranslator.FromHtml(value); }
        }
        [DataMember(Name = "value")]
        public double Value { get; set; }
        [DataMember(Name = "tooltext")]
        public string ToolTip { get; set; }

        [DataMember(Name = "category", EmitDefaultValue = false)]
        public IList<ChartPieSlice> Slices { get; set; }

    }
}