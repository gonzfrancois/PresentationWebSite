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
using PresentationWebSite.UI.WebMvc.Models.Home;
using PresentationWebSite.UI.WebMvc.Models.Introduction;
using PresentationWebSite.UI.WebMvc.Tests.Helpers;

namespace PresentationWebSite.UI.WebMvc.Tests.ExtensionsTests
{
    [TestFixture]
    public class ConvertToExtensionTest
    {
        private readonly List<Language> _languages = new List<Language>()
        {
            new Language() {Id = 1, CultureIsoCode = "fr-FR"},
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

        [Test]
        public void Should_Transform_HobyModel_To_Hobby()
        {
            var image = TestResources.TestsResources.test;

            var expectedStream = new MemoryStream();
            image.Save(expectedStream, ImageFormat.Jpeg);

            var expectedWebImage = new WebImage(expectedStream);
            expectedWebImage.Resize(350, 200, false);

            var expected = new Hobby()
            {
                Id = 1,
                Content = expectedWebImage.GetBytes(),
                Texts = new List<Text>()
                    {
                        new Text() {Language = _languages[0], Value = "hb1.fr"},
                        new Text() {Language = _languages[1], Value = "hb1.en"}
                    }
            };

            var imgFake = new Mock<HttpPostedFileBase>();
            imgFake.Setup(x => x.InputStream).Returns(new MemoryStream(expectedWebImage.GetBytes()));

            var act = new HobbyModel()
            {
                Picture = imgFake.Object,
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

        [Test]
        public void Should_Transform_ApplicationUserModel_To_ApplicationUser()
        {
            var expected = new ApplicationUser()
            {
                Id = 1,
                City = "Perpignan",
                DateOfBirth = new DateTime(2000, 1, 20),
                FamilyName = "Dupont",
                FirstName = "Gilles",
                Email = "gilles@dupont.fr",
                PhoneNumber = "0033468000000",
                ZipCode = "66000",
                LinkedInUrl = "gilles.dupont66",
                TwitterName = "gillesDupont",
                DisplayWork = new List<Text>()
                {
                    new Text() {Language = _languages[0], Value = "jb1.fr"},
                    new Text() {Language = _languages[1], Value = "jb1.en"}
                },
                ApplicationUserPresentations = new List<Text>()
                {
                    new Text() {Language = _languages[0], Value = "Presentation.fr"},
                    new Text() {Language = _languages[1], Value = "Presentation.en"}
                },
                PresentationTitleTexts = new List<Text>()
                {
                    new Text() {Language = _languages[0], Value = "PresentationTitle.fr"},
                    new Text() {Language = _languages[1], Value = "PresentationTitle.en"}
                },
                PresentationSubTitleTexts = new List<Text>()
                {
                    new Text() {Language = _languages[0], Value = "PresentationSubTitle.fr"},
                    new Text() {Language = _languages[1], Value = "PresentationSubTitle.en"}
                }
            };

            var model = new ApplicationUserModel()
            {
                Id = 1,
                City = "Perpignan",
                DateOfBirth = new DateTime(2000, 1, 20),
                FamilyName = "Dupont",
                FirstName = "Gilles",
                Email = "gilles@dupont.fr",
                PhoneNumber = "0033468000000",
                ZipCode = "66000",
                LinkedInUrl = "gilles.dupont66",
                TwitterName = "gillesDupont",
                DisplayWork = new List<TextModel>()
                                {
                                    new TextModel() {Language = _languages[0], Value = "jb1.fr"},
                                    new TextModel() {Language = _languages[1], Value = "jb1.en"}
                                },
                PresentationTexts = new List<TextModel>()
                                {
                                    new TextModel() {Language = _languages[0], Value = "Presentation.fr"},
                                    new TextModel() {Language = _languages[1], Value = "Presentation.en"}
                                },
                PresentationTitleTexts = new List<TextModel>()
                                {
                                    new TextModel() {Language = _languages[0], Value = "PresentationTitle.fr"},
                                    new TextModel() {Language = _languages[1], Value = "PresentationTitle.en"}
                                },
                PresentationSubTitleTexts = new List<TextModel>()
                                {
                                    new TextModel() {Language = _languages[0], Value = "PresentationSubTitle.fr"},
                                    new TextModel() {Language = _languages[1], Value = "PresentationSubTitle.en"}
                                },
            };

            var result = model.ToDto(_languages);

            Assert.AreEqual(expected.Id, result.Id);
            Assert.AreEqual(expected.City, result.City);
            Assert.AreEqual(expected.DateOfBirth, result.DateOfBirth);
            Assert.AreEqual(expected.FamilyName, result.FamilyName);
            Assert.AreEqual(expected.FirstName, result.FirstName);
            Assert.AreEqual(expected.Email, result.Email);
            Assert.AreEqual(expected.PhoneNumber, result.PhoneNumber);
            Assert.AreEqual(expected.ZipCode, result.ZipCode);
            Assert.AreEqual(expected.LinkedInUrl, result.LinkedInUrl);
            Assert.AreEqual(expected.TwitterName, result.TwitterName);

            AssertExtension.CompareIEnumerable(expected.DisplayWork, result.DisplayWork,
                (x, y) => x.Language == y.Language && x.Value == y.Value);
            AssertExtension.CompareIEnumerable(expected.ApplicationUserPresentations, result.ApplicationUserPresentations,
                (x, y) => x.Language == y.Language && x.Value == y.Value);
            AssertExtension.CompareIEnumerable(expected.PresentationTitleTexts, result.PresentationTitleTexts,
                (x, y) => x.Language == y.Language && x.Value == y.Value);
            AssertExtension.CompareIEnumerable(expected.PresentationSubTitleTexts, result.PresentationSubTitleTexts,
                (x, y) => x.Language == y.Language && x.Value == y.Value);
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

        [Test]
        public void Should_Transform_SkillCategory_To_AddSkillCategoryModel()
        {
            var expected = new AddSkillCategoryModel()
            {
                Id = 1,
                DisplayPriority = 1,
                Texts = new List<TextModel>()
                        {
                            new TextModel() {Language = _languages[0], Value = "skc1.fr"},
                            new TextModel() {Language = _languages[1], Value = "skc1.en"}
                        },
                Skills = new List<SkillModel>()
                {
                    new SkillModel()
                    {
                        KnowledgePercent = 50,
                        Texts = new List<TextModel>()
                                {
                                    new TextModel() {Language = _languages[0], Value = "sk1"},
                                    new TextModel() {Language = _languages[1], Value = "sk2"}
                                }
                    },
                    new SkillModel() {
                        KnowledgePercent = 60,
                        Texts = new List<TextModel>()
                                {
                                    new TextModel() {Language = _languages[0], Value = "sk3"},
                                    new TextModel() {Language = _languages[1], Value = "sk4"}
                                }
                    }
                }
            };

            var act = new SkillCategory()
            {
                Id = 1,
                DisplayPriority = 1,
                Texts = new List<Text>()
                        {
                            new Text() {Language = _languages[0], Value = "skc1.fr"},
                            new Text() {Language = _languages[1], Value = "skc1.en"}
                        },
                Skills = new List<Skill>()
                {
                    new Skill()
                    {
                        Id = 1,
                        KnowledgePercent = 50,
                        Texts = new List<Text>()
                                {
                                    new Text() {Language = _languages[0], Value = "sk1"},
                                    new Text() {Language = _languages[1], Value = "sk2"}
                                }
                    },
                    new Skill()
                    {
                        Id = 2,
                        KnowledgePercent = 60,
                        Texts = new List<Text>()
                                {
                                    new Text() {Language = _languages[0], Value = "sk3"},
                                    new Text() {Language = _languages[1], Value = "sk4"}
                                }
                    }
                }
            };

            var result = act.ToDto();

            Assert.AreEqual(expected.Id, result.Id);
            Assert.AreEqual(expected.DisplayPriority, result.DisplayPriority);

            AssertExtension.CompareIEnumerable(expected.Texts, result.Texts,
                (x, y) => x.Language == y.Language && x.Value == y.Value);

            AssertExtension.CompareIEnumerable(expected.Skills, result.Skills,
                (x, y) => x.KnowledgePercent == y.KnowledgePercent &&
                AssertExtension.CompareIEnumerable(x.Texts, y.Texts,
                (a, b) => a.Language == b.Language && a.Value == b.Value));
        }

        [Test]
        public void Should_Transform_Skill_To_AddSkillModel()
        {
            var expected = new AddSkillModel()
            {
                KnowledgePercent = 50,
                Texts = new List<TextModel>()
                            {
                                new TextModel() {Language = _languages[0], Value = "sk1.fr"},
                                new TextModel() {Language = _languages[1], Value = "sk1.en"}
                            }
            };

            var act = new Skill()
            {
                KnowledgePercent = 50,
                Texts = new List<Text>()
                            {
                                new Text() {Language = _languages[0], Value = "sk1.fr"},
                                new Text() {Language = _languages[1], Value = "sk1.en"}
                            }
            };

            var result = act.ToDto();

            Assert.AreEqual(expected.KnowledgePercent, result.KnowledgePercent);
            AssertExtension.CompareIEnumerable(expected.Texts, result.Texts,
                (x, y) => x.Language == y.Language && x.Value == y.Value);
        }

        [Test]
        public void Should_Transform_IEnumerableOfText_To_IEnumerableOfTextModel()
        {
            var expected = new List<TextModel>()
                           {
                               new TextModel() {Language = _languages[0], Value = "sk1.fr"},
                               new TextModel() {Language = _languages[1], Value = "sk1.en"}
                           };

            var act = new List<Text>()
                        {
                            new Text() {Language = _languages[0], Value = "sk1.fr"},
                            new Text() {Language = _languages[1], Value = "sk1.en"}
                        };

            var result = act.ToDto();

            AssertExtension.CompareIEnumerable(expected, result,
                (x, y) => x.Language == y.Language && x.Value == y.Value);
        }

        [Test]
        public void Should_Transform_ApplicationUser_To_ApplicationUserModel()
        {

            var expected = new ApplicationUserModel()
            {
                Id = 1,
                City = "Perpignan",
                DateOfBirth = new DateTime(2000, 1, 20),
                FamilyName = "Dupont",
                FirstName = "Gilles",
                Email = "gilles@dupont.fr",
                PhoneNumber = "0033468000000",
                ZipCode = "66000",
                LinkedInUrl = "gilles.dupont66",
                TwitterName = "gillesDupont",
                DisplayWork = new List<TextModel>()
                                {
                                    new TextModel() {Language = _languages[0], Value = "jb1.fr"},
                                    new TextModel() {Language = _languages[1], Value = "jb1.en"}
                                },
                PresentationTexts = new List<TextModel>()
                                {
                                    new TextModel() {Language = _languages[0], Value = "Presentation.fr"},
                                    new TextModel() {Language = _languages[1], Value = "Presentation.en"}
                                },
                PresentationTitleTexts = new List<TextModel>()
                                {
                                    new TextModel() {Language = _languages[0], Value = "PresentationTitle.fr"},
                                    new TextModel() {Language = _languages[1], Value = "PresentationTitle.en"}
                                },
                PresentationSubTitleTexts = new List<TextModel>()
                                {
                                    new TextModel() {Language = _languages[0], Value = "PresentationSubTitle.fr"},
                                    new TextModel() {Language = _languages[1], Value = "PresentationSubTitle.en"}
                                },
            };

            var act = new ApplicationUser()
            {
                Id = 1,
                City = "Perpignan",
                DateOfBirth = new DateTime(2000, 1, 20),
                FamilyName = "Dupont",
                FirstName = "Gilles",
                Email = "gilles@dupont.fr",
                PhoneNumber = "0033468000000",
                ZipCode = "66000",
                LinkedInUrl = "gilles.dupont66",
                TwitterName = "gillesDupont",
                DisplayWork = new List<Text>()
                {
                    new Text() {Language = _languages[0], Value = "jb1.fr"},
                    new Text() {Language = _languages[1], Value = "jb1.en"}
                },
                ApplicationUserPresentations = new List<Text>()
                {
                    new Text() {Language = _languages[0], Value = "Presentation.fr"},
                    new Text() {Language = _languages[1], Value = "Presentation.en"}
                },
                PresentationTitleTexts = new List<Text>()
                {
                    new Text() {Language = _languages[0], Value = "PresentationTitle.fr"},
                    new Text() {Language = _languages[1], Value = "PresentationTitle.en"}
                },
                PresentationSubTitleTexts = new List<Text>()
                {
                    new Text() {Language = _languages[0], Value = "PresentationSubTitle.fr"},
                    new Text() {Language = _languages[1], Value = "PresentationSubTitle.en"}
                }
            };

            var result = act.ToDto(_languages);

            Assert.AreEqual(expected.Id, result.Id);
            Assert.AreEqual(expected.City, result.City);
            Assert.AreEqual(expected.DateOfBirth, result.DateOfBirth);
            Assert.AreEqual(expected.FamilyName, result.FamilyName);
            Assert.AreEqual(expected.FirstName, result.FirstName);
            Assert.AreEqual(expected.Email, result.Email);
            Assert.AreEqual(expected.PhoneNumber, result.PhoneNumber);
            Assert.AreEqual(expected.ZipCode, result.ZipCode);
            Assert.AreEqual(expected.LinkedInUrl, result.LinkedInUrl);
            Assert.AreEqual(expected.TwitterName, result.TwitterName);

            AssertExtension.CompareIEnumerable(expected.DisplayWork, result.DisplayWork,
                (x, y) => x.Language == y.Language && x.Value == y.Value);
            AssertExtension.CompareIEnumerable(expected.PresentationTexts, result.PresentationTexts,
                (x, y) => x.Language == y.Language && x.Value == y.Value);
            AssertExtension.CompareIEnumerable(expected.PresentationTitleTexts, result.PresentationTitleTexts,
                (x, y) => x.Language == y.Language && x.Value == y.Value);
            AssertExtension.CompareIEnumerable(expected.PresentationSubTitleTexts, result.PresentationSubTitleTexts,
                (x, y) => x.Language == y.Language && x.Value == y.Value);
        }

    }
}
