using System.Text;

namespace PresentationWebSite.UI.WebMvc.Helpers
{
    public static class Utilities
    {
        public static string GetGlyphiconStarsFromPercents(int percent, int maxNumberOfStars)
        {
            var result = new StringBuilder();
            if (maxNumberOfStars > 0)
            {
                for (var i = 0; i < maxNumberOfStars; i++)
                {
                    result.Append(i < percent*maxNumberOfStars/100
                        ? "<span class='glyphicon glyphicon-star'></span>"
                        : "<span class='glyphicon glyphicon-star-empty'></span>");
                }
            }
            return result.ToString();
        }
    }
}
