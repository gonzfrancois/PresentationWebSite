using System;
using System.Drawing;

namespace PresentationWebSite.UI.WebMvc.Helpers.Extensions
{
    public static class ColorExtension
    {
        public static string GetHexValue(this Color color)
        {
            return "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
        }

        public static Color GetNextColor(this Color color)
        {
            Func<int, int> getNewColor = v => (int)Math.Round(Math.Abs(Math.Sin(v == 0 ? new Random().Next(1, 255) : v) * 255), 0);
            return Color.FromArgb(255, getNewColor(color.R), getNewColor(color.G), getNewColor(color.B));
        }
    }
}
