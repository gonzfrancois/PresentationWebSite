using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Helpers;
using Moq;
using NUnit.Framework;
using PresentationWebSite.Dal.Model;
using PresentationWebSite.UI.WebMvc.Helpers.Extensions;
using PresentationWebSite.UI.WebMvc.Models.Common;
using PresentationWebSite.UI.WebMvc.Models.Introduction;
using PresentationWebSite.UI.WebMvc.Tests.Helpers;

namespace PresentationWebSite.UI.WebMvc.Tests.ExtensionsTests
{
    [TestFixture]
    public class ConvertToExtensionTest
    {
        private readonly List<Language> _languages = new List<Language>()
        {
            new Language() {Id = 1, CultureIsoCode = "fr-fr"},
            new Language() {Id = 2, CultureIsoCode = "en-US"}
        };

        [Test]
        public void Should_Transform_IEnumerableOfTextModel_To_IEnumerableOfText()
        {
            var textValue = new List<string>() { "txt1", "txt2" };
            var expected = new List<Text>()
            {
                new Text() {Language = _languages[0], Value = textValue[0]},
                new Text() {Language = _languages[1], Value = textValue[1]}
            };

            IEnumerable<TextModel> act = new List<TextModel>()
            {
                new TextModel() {Language = _languages[0], Value = textValue[0]},
                new TextModel() {Language = _languages[1], Value = textValue[1]}
            };

            AssertExtension.CompareIEnumerable(expected, act.ToDto(_languages),
                (x, y) => x.Language == y.Language && x.Value == y.Value);

            AssertExtension.CompareIEnumerable(new List<Text>(), new List<TextModel>().ToDto(_languages),
                (x, y) => x.Language == y.Language && x.Value == y.Value);
        }

        [Test]
        public void Should_Transform_GraduationModel_To_Graduation()
        {
            var expected = new Grade()
            {
                Id = 1,
                ObtainingDateTime = new DateTime(2000, 1, 1),
                Texts = new List<Text>()
                        {
                            new Text() {Language = _languages[0], Value = "txt1"},
                            new Text() {Language = _languages[1], Value = "txt2"}
                        }
            };

            var act = new GraduationModel()
            {
                ObtainingDateTime = new DateTime(2000, 1, 1),
                Texts = new List<TextModel>()
                        {
                            new TextModel() {Language = _languages[0], Value = "txt1"},
                            new TextModel() {Language = _languages[1], Value = "txt2"}
                        }
            };

            Assert.AreEqual(expected.ObtainingDateTime, act.ToDto(_languages).ObtainingDateTime);

            AssertExtension.CompareIEnumerable(expected.Texts, act.ToDto(_languages).Texts,
                (x, y) => x.Language == y.Language && x.Value == y.Value);
        }

        [Test]
        public void Should_Transform_AddSkillCategoryModel_To_SkillCategory()
        {
            var expected = new SkillCategory()
            {
                Id = 1,
                DisplayPriority = 1,
                Texts = new List<Text>()
                        {
                            new Text() {Language = _languages[0], Value = "skc1"},
                            new Text() {Language = _languages[1], Value = "skc2"}
                        },
                Skills = new List<Skill>()
                {
                    new Skill() {Id = 1, KnowledgePercent = 50, Texts = new List<Text>()
                                                                {
                                                                    new Text() {Language = _languages[0], Value = "sk1"},
                                                                    new Text() {Language = _languages[1], Value = "sk2"}
                                                                }
                    },
                    new Skill() {Id = 2, KnowledgePercent = 50, Texts = new List<Text>()
                                                                {
                                                                    new Text() {Language = _languages[0], Value = "sk3"},
                                                                    new Text() {Language = _languages[1], Value = "sk4"}
                                                                }
                    }
                }
            };

            var act = new AddSkillCategoryModel()
            {
                DisplayPriority = 1,
                Texts = new List<TextModel>()
                        {
                            new TextModel() {Language = _languages[0], Value = "skc1"},
                            new TextModel() {Language = _languages[1], Value = "skc2"}
                        }
            };

            Assert.AreEqual(expected.DisplayPriority, act.ToDto(_languages).DisplayPriority);
            AssertExtension.CompareIEnumerable(expected.Texts, act.ToDto(_languages).Texts,
                (x, y) => x.Language == y.Language && x.Value == y.Value);
        }

        [Test]
        public void Should_Transform_AddSkillModel_To_Skill()
        {
            var skillCategories = new List<SkillCategory>()
            {
                NewSkillCategory(1),
                NewSkillCategory(2)
            };
            var expected = new Skill()
            {
                Id = 1,
                KnowledgePercent = 50,
                Category = skillCategories[0],
                Texts = new List<Text>()
                            {
                                new Text() {Language = _languages[0], Value = "sk1.fr"},
                                new Text() {Language = _languages[1], Value = "sk1.en"}
                            }
            };

            var act = new AddSkillModel()
            {
                CategoryId = 1,
                KnowledgePercent = 50,
                Texts = new List<TextModel>()
                            {
                                new TextModel() {Language = _languages[0], Value = "sk1.fr"},
                                new TextModel() {Language = _languages[1], Value = "sk1.en"}
                            }
            };
            var result = act.ToDto(skillCategories, _languages);

            Assert.AreEqual(expected.KnowledgePercent, result.KnowledgePercent);
            Assert.AreEqual(expected.Category, result.Category);
            
            AssertExtension.CompareIEnumerable(expected.Texts, result.Texts, (x, y) => x.Language == y.Language && x.Value == y.Value);
        }

        [Test]
        public void Should_Transform_AddWorkModel_To_Work()
        {
            var jobs = new List<Job>()
            {
                new Job() {Id = 1, Works = new List<Work>()},
                new Job() {Id = 2, Works = new List<Work>()},
            };

            var expected = new Work()
            {
                Id = 1,
                DisplayPriority = 1,
                Job = jobs[0],
                Texts = new List<Text>()
                            {
                                new Text() {Language = _languages[0], Value = "wk1.fr"},
                                new Text() {Language = _languages[1], Value = "wk1.en"}
                            }
            };

            var act = new AddWorkModel()
            {
                DisplayPriority = 1,
                JobId = 1,
                Texts = new List<TextModel>()
                {
                    new TextModel() {Language = _languages[0], Value = "wk1.fr"},
                    new TextModel() {Language = _languages[1], Value = "wk1.en"}
                }
            };

            var result = act.ToDto(jobs, _languages);
            
            Assert.AreEqual(expected.Job, result.Job);
            Assert.AreEqual(expected.DisplayPriority, result.DisplayPriority);
            AssertExtension.CompareIEnumerable(expected.Texts, result.Texts, (x, y) => x.Language == y.Language && x.Value == y.Value);
        }

        [Test]
        public void Should_Transform_JobModel_To_Job()
        {
            var expecteds = new List<Job>()
            { 
                new Job()
                {
                    Id = 1,
                    StarterDate = new DateTime(2000,1,1),
                    EndDate = null,
                    Texts = new List<Text>()
                                {
                                    new Text() {Language = _languages[0], Value = "jb1.fr"},
                                    new Text() {Language = _languages[1], Value = "jb1.en"}
                                }
                },
                new Job()
                {
                    Id = 2,
                    StarterDate = new DateTime(2005,2,21),
                    EndDate = new DateTime(2006,3,22),
                    Texts = new List<Text>()
                                {
                                    new Text() {Language = _languages[0], Value = "jb2.fr"},
                                    new Text() {Language = _languages[1], Value = "jb2.en"}
                                }
                },
            };
            var results = new List<JobModel>()
            { 
                new JobModel()
                {
                    StarterDate = new DateTime(2000, 1, 1),
                    IsNotActualJob = false,
                    EndDate = new DateTime(),
                    Texts = new List<TextModel>()
                                {
                                    new TextModel() {Language = _languages[0], Value = "jb1.fr"},
                                    new TextModel() {Language = _languages[1], Value = "jb1.en"}
                                }
                },
                new JobModel()
                {
                    IsNotActualJob = true,
                    StarterDate = new DateTime(2005,2,21),
                    EndDate = new DateTime(2006,3,22),
                    Texts = new List<TextModel>()
                                {
                                    new TextModel() {Language = _languages[0], Value = "jb2.fr"},
                                    new TextModel() {Language = _languages[1], Value = "jb2.en"}
                                }
                },
            };

            for (int i = 0; i < results.Count; i++)
            {
                var expected = expecteds[i];
                var result = results[i].ToDto(_languages);

                Assert.AreEqual(expected.StarterDate, result.StarterDate);
                Assert.AreEqual(expected.EndDate, result.EndDate);
                AssertExtension.CompareIEnumerable(expected.Texts, result.Texts, (x, y) => x.Language == y.Language && x.Value == y.Value);
            }

            
        }

        [Ignore("Not implemented_ Error <<Stream closed when access>> ")]
        public void Should_Transform_HobyModel_To_Hobby()
        {
            var image = TestResources.TestsResources.test;
            using (var stream = new MemoryStream())
            {
                image.Save(stream, ImageFormat.Jpeg);
                stream.Seek(0, 0);
                var webImg = new WebImage(stream);

                var expected = new Hobby()
                {
                    Id = 1,
                    Content = webImg.GetBytes(),
                    Texts = new List<Text>()
                    {
                        new Text() {Language = _languages[0], Value = "hb1.fr"},
                        new Text() {Language = _languages[1], Value = "hb1.en"}
                    }
                };

                var context = new Mock<HttpContextBase>();
                var request = new Mock<HttpRequestBase>();
                var files = new Mock<HttpFileCollectionBase>();
                var file = new Mock<HttpPostedFileBase>();
                context.Setup(x => x.Request).Returns(request.Object);

                files.Setup(x => x.Count).Returns(1);
                
                // The required properties from my Controller side
                file.Setup(x => x.InputStream).Returns(stream);
                file.Setup(x => x.ContentLength).Returns((int) stream.Length);

                files.Setup(x => x.Get(0).InputStream).Returns(file.Object.InputStream);
                request.Setup(x => x.Files).Returns(files.Object);
                request.Setup(x => x.Files[0]).Returns(file.Object);

                var act = new HobbyModel()
                {
                    Picture = file.Object,
                    Texts = new List<TextModel>()
                    {
                        new TextModel() {Language = _languages[0], Value = "hb1.fr"},
                        new TextModel() {Language = _languages[1], Value = "hb1.en"}
                    }
                };

                var result = act.ToDto(_languages);

                Assert.AreEqual(expected.Content, result.Content);
                AssertExtension.CompareIEnumerable(expected.Texts, result.Texts,
                    (x, y) => x.Language == y.Language && x.Value == y.Value);
            }
        }

        private SkillCategory NewSkillCategory(int id)
        {
            return new SkillCategory()
            {
                Id = id,
                DisplayPriority = 1,
                Texts = new List<Text>()
                        {
                            new Text() {Language = _languages[0], Value = "skc1."+id},
                            new Text() {Language = _languages[1], Value = "skc2."+id}
                        },
                Skills = new List<Skill>()
                {
                    new Skill() {Id = 1, KnowledgePercent = 50, Texts = new List<Text>()
                                                                {
                                                                    new Text() {Language = _languages[0], Value = "sk1."+id},
                                                                    new Text() {Language = _languages[1], Value = "sk2."+id}
                                                                }
                    },
                    new Skill() {Id = 2, KnowledgePercent = 50, Texts = new List<Text>()
                                                                {
                                                                    new Text() {Language = _languages[0], Value = "sk3."+id},
                                                                    new Text() {Language = _languages[1], Value = "sk4."+id}
                                                                }
                    }
                }
            };
        }
    }
}
