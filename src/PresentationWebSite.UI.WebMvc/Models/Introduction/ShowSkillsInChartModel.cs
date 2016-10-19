using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.DynamicData;
using Microsoft.Ajax.Utilities;

namespace PresentationWebSite.UI.WebMvc.Models.Introduction
{
    public class ShowSkillsInChartModel : IntroductionModelBase
    {
        private IList<Color> _palette = new List<Color>()
        {
            ColorTranslator.FromHtml("#f8bd19"),
            ColorTranslator.FromHtml("#e44a00"),
            ColorTranslator.FromHtml("#ccff66"),
            ColorTranslator.FromHtml("#008ee4"),
            ColorTranslator.FromHtml("#33bdda")
        };
        public override IntroductionChildTab ActiveTab => IntroductionChildTab.Skill;

        public ChartPie ChartDatas { get; set; }

        public IList<Color> Palette
        {
            get { return _palette; }
            set { _palette = value; }
        }
    }

    [DataContract]
    public class ChartPie
    {
        [DataMember(Name = "chart")]
        public CharPieInfo Infos { get; set; }
        [DataMember(Name = "category")]
        public IEnumerable<ChartPieSlice> Slices { get; set; }
    }
    [DataContract]
    public class CharPieInfo
    {
        private bool _showPlotBorder;
        private Color _hoverfillcolor;
        private Color _piebordercolor;

        [DataMember(Name = "caption", EmitDefaultValue = false)]
        public string Caption { get; set; }
        [DataMember(Name = "captionFontSize", EmitDefaultValue = false)]
        public int CaptionFontSize { get; set; }
        [DataMember(Name = "subcaption", EmitDefaultValue = false)]
        public string SubCaption { get; set; }
        [DataMember(Name = "subCaptionFontSize", EmitDefaultValue = false)]
        public int SubCaptionFontSize { get; set; }
        [DataMember(Name = "showPlotBorder")]
        public int ShowPlotBorder
        {
            get { return _showPlotBorder ? 1 : 0; }
            set { _showPlotBorder = value == 1; }
        }
        [DataMember(Name = "piefillalpha")]
        public int PieFillAlpha { get; set; }
        [DataMember(Name = "pieborderthickness")]
        public int Pieborderthickness { get; set; }
        [DataMember(Name = "hoverfillcolor")]
        public string Hoverfillcolor
        {
            get { return "#" + _hoverfillcolor.R.ToString("X2") + _hoverfillcolor.G.ToString("X2") + _hoverfillcolor.B.ToString("X2"); }
            set { _hoverfillcolor = ColorTranslator.FromHtml(value); }
        }
        [DataMember(Name = "plotFillHoverAlpha")]
        public int PlotFillHoverAlpha { get; set; }
        [DataMember(Name = "piebordercolor")]
        public string Piebordercolor
        {
            get { return "#" + _piebordercolor.R.ToString("X2") + _piebordercolor.G.ToString("X2") + _piebordercolor.B.ToString("X2"); }
            set { _piebordercolor = ColorTranslator.FromHtml(value); }
        }
        [DataMember(Name = "numberprefix")]
        public string Numberprefix { get; set; }
        [DataMember(Name = "plottooltext")]
        public string PlotToolText { get; set; }
        [DataMember(Name = "theme")]
        public string Theme { get; set; }

        [DataMember(Name = "palette")]
        public int Palette { get; set; }
        [DataMember(Name = "valueFontSize")]
        public int ValueFontSize { get; set; }
        [DataMember(Name = "valueBgColor")]
        public string ValueBgColor { get; set; }
        [DataMember(Name = "valueBgAlpha")]
        public int ValueBgAlpha { get; set; }
        [DataMember(Name = "valueFontColor")]
        public string ValueFontColor { get; set; }
        [DataMember(Name = "valueBorderRadius")]
        public int ValueBorderRadius { get; set; }
    }

    [DataContract()]
    public class ChartPieSlice
    {
        private Color? _color;

        [DataMember(Name = "label")]
        public string Label { get; set; }

        [DataMember(Name = "color", EmitDefaultValue = false)]
        public string Color
        {
            get
            {
                if (_color != null)
                    return "#" + _color.Value.R.ToString("X2") + _color.Value.G.ToString("X2") + _color.Value.B.ToString("X2");
                return null;
            }
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
