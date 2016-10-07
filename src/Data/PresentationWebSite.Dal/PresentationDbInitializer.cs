using System;
using System.Collections.Generic;
using PresentationWebSite.Dal.Model;

namespace PresentationWebSite.Dal
{
    internal class PresentationDbInitializer : System.Data.Entity.DropCreateDatabaseAlways<PresentationDbContext>
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
                    new Text() {Language = fr, Value = "Mon bac fr"},
                    new Text() {Language = es, Value = "Mon bac es"},
                    new Text() {Language = en, Value = "Mon bac en"},
                    new Text() {Language = ca, Value = "Mon bac ca"}
                };
            context.Texts.AddRange(bacTexts);

            var btsTexts = new List<Text>()
                {
                    new Text() {Language = fr, Value = "Mon BTS fr"},
                    new Text() {Language = es, Value = "Mon BTS es"},
                    new Text() {Language = en, Value = "Mon BTS en"},
                    new Text() {Language = ca, Value = "Mon BTS ca"}
                };

            context.Grades.Add(new Grade() { ObtainingDateTime = new DateTime(2008, 6, 30), Texts = bacTexts });
            context.Grades.Add(new Grade() { ObtainingDateTime = new DateTime(2010, 10, 1), Texts = btsTexts });

            var cat1Txt = new List<Text>()
                {
                    new Text() {Language = fr, Value = "Categorie 1"},
                    new Text() {Language = es, Value = "Categoria 1"},
                    new Text() {Language = en, Value = "Category 1"},
                    new Text() {Language = ca, Value = "Categoria 1"}
                };
            context.Texts.AddRange(cat1Txt);

            var cat2Txt = new List<Text>()
                {
                    new Text() {Language = fr, Value = "Categorie 2"},
                    new Text() {Language = es, Value = "Categoria 2"},
                    new Text() {Language = en, Value = "Category 2"},
                    new Text() {Language = ca, Value = "Categoria 2"}
                };
            context.Texts.AddRange(cat2Txt);

            var sk1ATxt = new List<Text>()
                {
                    new Text() {Language = fr, Value = "compétence 1 a"},
                    new Text() {Language = es, Value = "comp 1 a"},
                    new Text() {Language = en, Value = "skill 1 a"},
                    new Text() {Language = ca, Value = "co 1 a"}
                };
            context.Texts.AddRange(sk1ATxt);
            var sk1BTxt = new List<Text>()
                {
                    new Text() {Language = fr, Value = "compétence 1 b"},
                    new Text() {Language = es, Value = "comp 1 b"},
                    new Text() {Language = en, Value = "skill 1 b"},
                    new Text() {Language = ca, Value = "co 1 b"}
                };
            context.Texts.AddRange(sk1BTxt);

            var sk2ATxt = new List<Text>()
                {
                    new Text() {Language = fr, Value = "compétence 2 a"},
                    new Text() {Language = es, Value = "comp 2 a"},
                    new Text() {Language = en, Value = "skill 2 a"},
                    new Text() {Language = ca, Value = "co 2 a"}
                };
            context.Texts.AddRange(sk2ATxt);
            var sk2BTxt = new List<Text>()
                {
                    new Text() {Language = fr, Value = "compétence 2 b"},
                    new Text() {Language = es, Value = "comp 2 b"},
                    new Text() {Language = en, Value = "skill 2 b"},
                    new Text() {Language = ca, Value = "co 2 b"}
                };
            context.Texts.AddRange(sk2BTxt);

            var cat1 = new SkillCategory() {DisplayPriority = 0, Texts = cat1Txt};
            var cat2 = new SkillCategory() {DisplayPriority = 0, Texts = cat2Txt};
            context.SkillGategories.Add(cat1);
            context.SkillGategories.Add(cat2);

            var sk1A = new Skill() {Category = cat1, KnowledgePercent = 50, Texts = sk1ATxt};
            var sk1B = new Skill() {Category = cat1, KnowledgePercent = 10, Texts = sk1BTxt};
            context.Skills.Add(sk1A);
            context.Skills.Add(sk1B);
            var sk2A = new Skill() { Category = cat2, KnowledgePercent = 40, Texts = sk2ATxt};
            var sk2B = new Skill() { Category = cat2, KnowledgePercent = 15, Texts = sk2BTxt};

            context.Skills.Add(sk2A);
            context.Skills.Add(sk2B);


            var work1Texts = new List<Text>()
                {
                    new Text() {Language = fr, Value = "work1 fr"},
                    new Text() {Language = es, Value = "work1 es"},
                    new Text() {Language = en, Value = "work1 en"},
                    new Text() {Language = ca, Value = "work1 ca"}
                };
            context.Texts.AddRange(work1Texts);

            var work2Texts = new List<Text>()
                {
                    new Text() {Language = fr, Value = "work2 fr"},
                    new Text() {Language = es, Value = "work2 es"},
                    new Text() {Language = en, Value = "work2 en"},
                    new Text() {Language = ca, Value = "work2 ca"}
                };
            context.Texts.AddRange(work2Texts);

            var work3Texts = new List<Text>()
                {
                    new Text() {Language = fr, Value = "work3 fr"},
                    new Text() {Language = es, Value = "work3 es"},
                    new Text() {Language = en, Value = "work3 en"},
                    new Text() {Language = ca, Value = "work3 ca"}
                };
            context.Texts.AddRange(work3Texts);

            var work4Texts = new List<Text>()
                {
                    new Text() {Language = fr, Value = "work4 fr"},
                    new Text() {Language = es, Value = "work4 es"},
                    new Text() {Language = en, Value = "work4 en"},
                    new Text() {Language = ca, Value = "work4 ca"}
                };
            context.Texts.AddRange(work4Texts);

            var work1 = new Work()
            {
                DisplayPriority = 0,
                Texts = work1Texts
            };
            
            var work2 = new Work()
            {
                DisplayPriority = 1,
                Texts = work2Texts
            };
            
            var work3 = new Work()
            {
                DisplayPriority = 2,
                Texts = work3Texts
            };
            
            var work4 = new Work()
            {
                DisplayPriority = 1,
                Texts = work4Texts
            };
            context.Works.Add(work1);
            context.Works.Add(work2);
            context.Works.Add(work3);
            context.Works.Add(work4);

            var job1Texts = new List<Text>()
                {
                    new Text() {Language = fr, Value = "job1 fr"},
                    new Text() {Language = es, Value = "job1 es"},
                    new Text() {Language = en, Value = "job1 en"},
                    new Text() {Language = ca, Value = "job1 ca"}
                };
            context.Texts.AddRange(job1Texts);

            var job2Texts = new List<Text>()
                {
                    new Text() {Language = fr, Value = "job2 fr"},
                    new Text() {Language = es, Value = "job2 es"},
                    new Text() {Language = en, Value = "job2 en"},
                    new Text() {Language = ca, Value = "job2 ca"}
                };
            context.Texts.AddRange(job2Texts);

            var job1 = new Job()
            {
                StarterDate = new DateTime(2010,6,1),
                EndDate = new DateTime(2010,8,1),
                Works = new List<Work>() { work1,work2},
                Texts = job1Texts
            };
            context.Jobs.Add(job1);

            var job2 = new Job()
            {
                StarterDate = new DateTime(2011, 6, 1),
                Works = new List<Work>() { work3, work4 },
                Texts = job2Texts
            };
            context.Jobs.Add(job2);

            var hobby1Texts = new List<Text>()
                {
                    new Text() {Language = fr, Value = "hobby1 fr"},
                    new Text() {Language = es, Value = "hobby1 es"},
                    new Text() {Language = en, Value = "hobby1 en"},
                    new Text() {Language = ca, Value = "hobby1 ca"}
                };
            context.Texts.AddRange(hobby1Texts);

            var hobby2Texts = new List<Text>()
                {
                    new Text() {Language = fr, Value = "hobby2 fr"},
                    new Text() {Language = es, Value = "hobby2 es"},
                    new Text() {Language = en, Value = "hobby2 en"},
                    new Text() {Language = ca, Value = "hobby2 ca"}
                };
            context.Texts.AddRange(hobby2Texts);

            var hobby1 = new Hobby() { Texts = hobby1Texts};
            context.Hobbies.Add(hobby1);

            var hobby2 = new Hobby() { Texts = hobby2Texts };
            context.Hobbies.Add(hobby2);

            base.Seed(context);
        }
    }
}
