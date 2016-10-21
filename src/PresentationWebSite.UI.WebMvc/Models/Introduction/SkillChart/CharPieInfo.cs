using System.Drawing;
using System.Runtime.Serialization;
using PresentationWebSite.UI.WebMvc.Helpers.Extensions;

namespace PresentationWebSite.UI.WebMvc.Models.Introduction.SkillChart
{
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
            get { return _hoverfillcolor.GetHexValue(); }
            set { _hoverfillcolor = ColorTranslator.FromHtml(value); }
        }
        [DataMember(Name = "plotFillHoverAlpha")]
        public int PlotFillHoverAlpha { get; set; }
        [DataMember(Name = "piebordercolor")]
        public string Piebordercolor
        {
            get { return _piebordercolor.GetHexValue(); }
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

        public CharPieInfo()
        {
            //Set default values
            ValueFontSize = 12;
            ValueFontColor = "#FFFFFF";
            ValueBgColor = "#000000";
            ValueBgAlpha = 50;
            ValueBorderRadius = 5;
            ShowPlotBorder = 1;
            PieFillAlpha = 100;
            Pieborderthickness = 2;
            Palette = 1;
            Hoverfillcolor = "#CCCCCC";
            PlotFillHoverAlpha = 30;
            Piebordercolor = "#FFFFFF";
            Numberprefix = "$";
            PlotToolText = "$label";
            Theme = "fint";
        }
        
    }
}