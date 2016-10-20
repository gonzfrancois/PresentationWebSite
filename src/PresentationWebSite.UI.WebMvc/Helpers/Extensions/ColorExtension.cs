using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationWebSite.UI.WebMvc.Helpers.Extensions
{
    internal static class ColorExtension
    {
        internal static string GetHexValue(this Color color)
        {
            return "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
        }

        internal static Color GetNextColor(this Color color)
        {
            Func<int, int> getNewColor = v => (int)Math.Round(Math.Abs(Math.Sin(v) * 255), 0);
            return Color.FromArgb(1, getNewColor(color.R), getNewColor(color.G), getNewColor(color.B));
        }
    }
}
