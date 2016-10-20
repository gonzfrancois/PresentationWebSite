using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationWebSite.UI.WebMvc.Helpers
{
    internal static class Utilities
    {
        public static string GetGlyphiconStarsFromPercents(int percent, int maxNumberOfStars)
        {
            var value = percent*maxNumberOfStars/100;
            var result = new StringBuilder();
            for (var i = 0; i < maxNumberOfStars; i++)
            {
                result.Append(value < i
                    ? "<span class='glyphicon glyphicon-star'></span>"
                    : "<span class='glyphicon glyphicon-star-empty'></span>");
            }
            return result.ToString();
        }
    }
}
