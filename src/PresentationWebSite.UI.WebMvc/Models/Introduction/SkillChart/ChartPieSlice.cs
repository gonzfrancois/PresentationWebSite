using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using System.Text;
using PresentationWebSite.UI.WebMvc.Helpers.Extensions;

namespace PresentationWebSite.UI.WebMvc.Models.Introduction.SkillChart
{
    [DataContract()]
    public class ChartPieSlice
    {
        private Color? _color;
        private string _label;

        [DataMember(Name = "label")]
        public string Label
        {
            get { return GetAbbrText(_label); }
            set { _label = value; }
        }

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

        private static string GetAbbrText(string text)
        {
            var result = new StringBuilder();
            if (text.Length > 10 && text.Contains("."))
            {
                foreach (var ch in text.Split('.')[0])
                    if (char.IsUpper(ch))
                        result.Append(ch);

                result.Append('.');
                for (var i = 1; i < text.Split('.').Length; i++)
                    result.Append(text.Split('.')[i]);
            }
            else
                result.Append(text.Replace(" ", "{br}"));
            return result.ToString();
        
    }
    }
}