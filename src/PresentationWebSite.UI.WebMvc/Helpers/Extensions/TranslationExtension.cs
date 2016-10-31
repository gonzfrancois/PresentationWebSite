using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using PresentationWebSite.Dal.Model;
using PresentationWebSite.UI.WebMvc.Models.Common;

namespace PresentationWebSite.UI.WebMvc.Helpers.Extensions
{
    public static class TranslationExtension
    {
        public static string GetText(this IEnumerable<TextModel> self, CultureInfo culture)
        {
            return self.FirstOrDefault(x => x.Language.CultureIsoCode == culture.Name)?.Value;
        }

        public static string GetText(this ICollection<Text> self, CultureInfo culture)
        {
            return self.FirstOrDefault(x => x.Language.CultureIsoCode == culture.Name)?.Value;
        }
    }
}