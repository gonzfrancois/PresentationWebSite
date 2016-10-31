using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using NUnit.Framework;
using PresentationWebSite.Dal.Model;
using PresentationWebSite.UI.WebMvc.Helpers.Extensions;
using PresentationWebSite.UI.WebMvc.Models.Common;

namespace PresentationWebSite.UI.WebMvc.Tests.ExtensionsTests
{
    [TestFixture()]
    public class TranslationExtensionTest
    {
        private readonly List<Language> _languages = new List<Language>()
        {
            new Language() {Id = 1, CultureIsoCode = "fr-FR"},
            new Language() {Id = 2, CultureIsoCode = "en-US"}
        };

        [Test]
        public void Should_Return_Text_According_To_Culture()
        {
            var texts = new List<Text>()
            {
                new Text() {Language = _languages[0], Value = "text.fr"},
                new Text() {Language = _languages[1], Value = "text.en"}
            };

            foreach (var language in _languages)
            {
                Assert.AreEqual(
                    texts.GetText(new CultureInfo(language.CultureIsoCode)),
                    texts.FirstOrDefault(x => x.Language.CultureIsoCode == language.CultureIsoCode)?.Value
                    );
            }
        }

        [Test]
        public void Should_Return_TextModel_According_To_Culture()
        {
            var texts = new List<TextModel>()
            {
                new TextModel() {Language = _languages[0], Value = "text.fr"},
                new TextModel() {Language = _languages[1], Value = "text.en"}
            };

            foreach (var language in _languages)
            {
                Assert.AreEqual(
                    texts.GetText(new CultureInfo(language.CultureIsoCode)),
                    texts.FirstOrDefault(x => x.Language.CultureIsoCode == language.CultureIsoCode)?.Value
                    );
            }

        }
    }
}
