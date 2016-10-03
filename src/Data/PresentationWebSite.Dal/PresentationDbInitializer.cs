using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PresentationWebSite.Dal.Model;

namespace PresentationWebSite.Dal
{
    public class PresentationDbInitializer : System.Data.Entity.DropCreateDatabaseAlways<PresentationDbContext>
    {
        protected override void Seed(PresentationDbContext context)
        {
            var fr = new Language() { CultureIsoCode = "fr-FR" };
            context.Languages.Add(fr);
            var en = new Language() { CultureIsoCode = "en-GB" };
            context.Languages.Add(en);
            var es = new Language() { CultureIsoCode = "es-ES" };
            context.Languages.Add(es);
            var ca = new Language() { CultureIsoCode = "ca-ES" };
            context.Languages.Add(ca);

            var bacTexts = new List<Text>()
                {
                    new Text() {Language = fr, TextKey = 1, Value = "Mon bac fr"},
                    new Text() {Language = es, TextKey = 1, Value = "Mon bac es"},
                    new Text() {Language = en, TextKey = 1, Value = "Mon bac en"},
                    new Text() {Language = ca, TextKey = 1, Value = "Mon bac ca"}
                };
            context.Texts.AddRange(bacTexts);

            var btsTexts = new List<Text>()
                {
                    new Text() {Language = fr, TextKey = 2, Value = "Mon BTS fr"},
                    new Text() {Language = es, TextKey = 2, Value = "Mon BTS es"},
                    new Text() {Language = en, TextKey = 2, Value = "Mon BTS en"},
                    new Text() {Language = ca, TextKey = 2, Value = "Mon BTS ca"}
                };

            context.Grades.Add(new Grade() { ObtainingDateTime = new DateTime(2008, 6, 30), Texts = bacTexts });
            context.Grades.Add(new Grade() { ObtainingDateTime = new DateTime(2010, 10, 1), Texts = btsTexts });

            base.Seed(context);
        }
    }
}
