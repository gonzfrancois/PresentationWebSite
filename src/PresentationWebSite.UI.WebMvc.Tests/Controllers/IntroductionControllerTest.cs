using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using PresentationWebSite.Dal.Model;
using PresentationWebSite.UI.WebMvc.Controllers;
using PresentationWebSite.UI.WebMvc.Controllers.CustomActionResult;
using PresentationWebSite.UI.WebMvc.Helpers;
using PresentationWebSite.UI.WebMvc.Helpers.Extensions;
using PresentationWebSite.UI.WebMvc.Models.Common;
using PresentationWebSite.UI.WebMvc.Models.Introduction;
using PresentationWebSite.UI.WebMvc.Models.Introduction.SkillChart;
using PresentationWebSite.UI.WebMvc.Tests.Helpers;

namespace PresentationWebSite.UI.WebMvc.Tests.Controllers
{
    [TestFixture]
    public class IntroductionControllerTest
    {
        [Test]
        public void ShowGraduationTest()
        {
            // Act
            var uow = new UnitOfWorkFakeFactory().Uow.Object;
            var controller = new IntroductionController(uow);

            var result = controller.ShowGraduations() as ViewResult;

            var expected = new GraduationsModel { Graduations = uow.GradesRepository.Get().ToList() };

            // Assert
            Assert.IsNotNull(result);
            AssertExtension.PropertyValuesAreEquals(expected, result.Model);
        }

        [Test]
        public void DeleteGraduationTest()
        {
            // Arrange
            var uow = new UnitOfWorkFakeFactory().Uow.Object;
            var controller = new IntroductionController(uow);

            var expectedList = new List<Grade>(uow.GradesRepository.Get().ToList());
            var idToRemove = expectedList.Last().Id;
            expectedList.RemoveAll(x => x.Id == idToRemove);

            // Act
            var expected = new GraduationsModel { Graduations = expectedList };
            var result = controller.DeleteGraduation(idToRemove) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteValues["action"], nameof(IntroductionController.ShowGraduations));
            AssertExtension.CompareIEnumerable(expected.Graduations, uow.GradesRepository.Get().ToList(),
                (x, y) => x.ObtainingDateTime == y.ObtainingDateTime
                          && x.Id == y.Id
                          &&
                          AssertExtension.CompareIEnumerable(x.Texts, y.Texts,
                              (a, b) => a.Language == b.Language && a.Value == b.Value)
            );
        }

        [Test]
        public void AddGraduation_Get_Test()
        {
            // Act
            var uow = new UnitOfWorkFakeFactory().Uow.Object;
            var controller = new IntroductionController(uow);

            var result = controller.AddGraduation() as ViewResult;

            var expected = new GraduationModel()
            {
                Texts = uow.LanguagesRepository.Get().ToList().Select(l => new TextModel() { Language = l }).ToList()
            };

            // Assert
            Assert.IsNotNull(result);
            AssertExtension.PropertyValuesAreEquals(expected, result.Model);
        }

        [Test]
        public void AddGraduation_Post_Test()
        {
            // Act
            var uow = new UnitOfWorkFakeFactory().Uow.Object;
            var paramUser = new GraduationModel()
            {
                ObtainingDateTime = new DateTime(2010, 9, 1),
                Texts = new List<TextModel>()
                {
                    new TextModel() {Language = uow.LanguagesRepository.Get().ToList()[0], Value = "Graduation.fr"},
                    new TextModel() {Language = uow.LanguagesRepository.Get().ToList()[1], Value = "Graduation.en"}
                }
            };
            var expected = paramUser.ToDto(uow.LanguagesRepository.Get().ToList());
            expected.Id = uow.GradesRepository.Get().Count() + 1;

            var nbGrades = uow.GradesRepository.Get().ToList().Count;
            var result = new IntroductionController(uow).AddGraduation(paramUser) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteValues["Action"], nameof(IntroductionController.ShowGraduations));
            Assert.AreEqual(nbGrades + 1, uow.GradesRepository.Get().Count());
            AssertExtension.PropertyValuesAreEquals(expected, uow.GradesRepository.Get().Last());
        }

        [Test]
        public void DeleteSkillCategoryTest()
        {
            // Arrange
            var uow = new UnitOfWorkFakeFactory().Uow.Object;
            var controller = new IntroductionController(uow);

            var expectedList = new List<SkillCategory>(uow.SkillCategoriesRepository.Get().ToList());
            var idToRemove = expectedList.Last().Id;
            expectedList.RemoveAll(x => x.Id == idToRemove);

            // Act
            var result = controller.DeleteSkillCategory(idToRemove) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteValues["action"], nameof(IntroductionController.ShowSkills));
            AssertExtension.CompareIEnumerable(expectedList, uow.SkillCategoriesRepository.Get().ToList(),
                (x, y) => x.DisplayPriority == y.DisplayPriority
                          && x.Id == y.Id
                          && AssertExtension.CompareIEnumerable(x.Skills, y.Skills,
                              (a, b) => a.Id == b.Id && a.KnowledgePercent == b.KnowledgePercent)
                          && AssertExtension.CompareIEnumerable(x.Texts, y.Texts,
                              (c, d) => c.Language == d.Language && c.Value == d.Value)
            );
        }

        [Test]
        public void AddSkillCategory_Get_Test()
        {
            // Act
            var uow = new UnitOfWorkFakeFactory().Uow.Object;
            var controller = new IntroductionController(uow);

            var result = controller.AddSkillCategory() as ViewResult;

            var expected = new AddSkillCategoryModel
            {
                Texts = uow.LanguagesRepository.Get()
                    .Select(language => new TextModel() { Language = language })
                    .ToList()
            };

            // Assert
            Assert.IsNotNull(result);
            AssertExtension.PropertyValuesAreEquals(expected, result.Model);
        }

        [Test]
        public void AddSkillCategory_Post_Test()
        {
            // Act
            var uow = new UnitOfWorkFakeFactory().Uow.Object;
            var paramUser = new AddSkillCategoryModel()
            {
                Texts = new List<TextModel>()
                {
                    new TextModel() {Language = uow.LanguagesRepository.Get().ToList()[0], Value = "NewSkillCat.fr"},
                    new TextModel() {Language = uow.LanguagesRepository.Get().ToList()[1], Value = "NewSkillCat.en"}
                }
            };
            var expected = paramUser.ToDto(uow.LanguagesRepository.Get().ToList());
            expected.Id = uow.SkillCategoriesRepository.Get().Count() + 1;

            var nbGrades = uow.SkillCategoriesRepository.Get().ToList().Count;
            var result = new IntroductionController(uow).AddSkillCategory(paramUser) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteValues["Action"], nameof(IntroductionController.ShowSkills));
            Assert.AreEqual(nbGrades + 1, uow.SkillCategoriesRepository.Get().Count());
            AssertExtension.PropertyValuesAreEquals(expected, uow.SkillCategoriesRepository.Get().Last());
        }

        [Test]
        public void ShowSkills_Test()
        {
            var uow = new UnitOfWorkFakeFactory().Uow.Object;
            var expected = new SkillCategoriesModel()
            {
                Categories = uow.SkillCategoriesRepository.Get().ToList(),
                ChartDatas = new ChartPie()
                {
                    Infos = new CharPieInfo(),
                    Slices = new List<ChartPieSlice>()
                    {
                        new ChartPieSlice()
                        {
                            Label = uow.SkillCategoriesRepository.Find(1).Texts.GetText(CultureInfo.CurrentUICulture),
                            Color = "#4285f4",
                            Value = 2,
                            ToolTip = uow.SkillCategoriesRepository.Find(1).Texts.GetText(CultureInfo.CurrentUICulture),
                            Slices = new List<ChartPieSlice>()
                            {
                                new ChartPieSlice()
                                {
                                    Label = uow.SkillsRepository.Find(1).Texts.GetText(CultureInfo.CurrentUICulture),
                                    Color = "#4285f4",
                                    Value = uow.SkillsRepository.Find(1).KnowledgePercent,
                                    ToolTip =
                                        uow.SkillsRepository.Find(1).Texts.GetText(CultureInfo.CurrentUICulture) + " " +
                                        Utilities.GetGlyphiconStarsFromPercents(
                                            uow.SkillsRepository.Find(1).KnowledgePercent, 5)
                                },
                                new ChartPieSlice()
                                {
                                    Label = uow.SkillsRepository.Find(2).Texts.GetText(CultureInfo.CurrentUICulture),
                                    Color = "#4285f4",
                                    Value = uow.SkillsRepository.Find(2).KnowledgePercent,
                                    ToolTip =
                                        uow.SkillsRepository.Find(2).Texts.GetText(CultureInfo.CurrentUICulture) + " " +
                                        Utilities.GetGlyphiconStarsFromPercents(
                                            uow.SkillsRepository.Find(2).KnowledgePercent, 5)
                                },
                            }
                        },
                        new ChartPieSlice()
                        {
                            Label = uow.SkillCategoriesRepository.Find(2).Texts.GetText(CultureInfo.CurrentUICulture),
                            Color = "#34a853",
                            Value = 2,
                            ToolTip = uow.SkillCategoriesRepository.Find(2).Texts.GetText(CultureInfo.CurrentUICulture),
                            Slices = new List<ChartPieSlice>()
                            {
                                new ChartPieSlice()
                                {
                                    Label = uow.SkillsRepository.Find(3).Texts.GetText(CultureInfo.CurrentUICulture),
                                    Color = "#34a853",
                                    Value = uow.SkillsRepository.Find(3).KnowledgePercent,
                                    ToolTip =
                                        uow.SkillsRepository.Find(3).Texts.GetText(CultureInfo.CurrentUICulture) + " " +
                                        Utilities.GetGlyphiconStarsFromPercents(
                                            uow.SkillsRepository.Find(3).KnowledgePercent, 5)
                                },
                                new ChartPieSlice()
                                {
                                    Label = uow.SkillsRepository.Find(4).Texts.GetText(CultureInfo.CurrentUICulture),
                                    Color = "#34a853",
                                    Value = uow.SkillsRepository.Find(4).KnowledgePercent,
                                    ToolTip =
                                        uow.SkillsRepository.Find(4).Texts.GetText(CultureInfo.CurrentUICulture) + " " +
                                        Utilities.GetGlyphiconStarsFromPercents(
                                            uow.SkillsRepository.Find(4).KnowledgePercent, 5)
                                },
                            }
                        }
                    }
                }
            };

            var controller = new IntroductionController(uow);
            var result = controller.ShowSkills() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected.ChartDatas.ToJson(), ((SkillCategoriesModel)result.Model).ChartDatas.ToJson());

        }

        [Test]
        public void AddSkill_Get_Test()
        {
            // Act
            var uow = new UnitOfWorkFakeFactory().Uow.Object;
            var controller = new IntroductionController(uow);
            const int skillCategoryId = 1;
            var expected = new AddSkillModel
            {
                Texts = uow.LanguagesRepository.Get().Select(language => new TextModel() { Language = language }).ToList(),
                CategoryId = skillCategoryId
            };

            var result = controller.AddSkill(skillCategoryId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            AssertExtension.PropertyValuesAreEquals(expected, result.Model);
        }

        [Test]
        public void AddSkill_Post_Test()
        {
            // Act
            var uow = new UnitOfWorkFakeFactory().Uow.Object;
            const int skillCategorieId = 1;
            var paramUser = new AddSkillModel()
            {
                KnowledgePercent = 50,
                CategoryId = skillCategorieId,
                Texts = new List<TextModel>()
                {
                    new TextModel() {Language = uow.LanguagesRepository.Get().ToList()[0], Value = "NewSkill.fr"},
                    new TextModel() {Language = uow.LanguagesRepository.Get().ToList()[1], Value = "NewSkill.en"}
                }
            };

            var expected = paramUser.ToDto(uow.SkillCategoriesRepository.Get().ToList(),
                uow.LanguagesRepository.Get().ToList());

            expected.Id = uow.SkillsRepository.Get().Count() + 1;

            var nbSkills = uow.SkillsRepository.Get().ToList().Count;
            var result = new IntroductionController(uow).AddSkill(paramUser) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteValues["Action"], nameof(IntroductionController.ShowSkills));
            Assert.AreEqual(nbSkills + 1, uow.SkillsRepository.Get().Count());
            AssertExtension.PropertyValuesAreEquals(expected, uow.SkillsRepository.Get().Last());
        }

        [Test]
        public void DeleteSkillTest()
        {
            // Arrange
            var uow = new UnitOfWorkFakeFactory().Uow.Object;
            var controller = new IntroductionController(uow);

            var expectedList = new List<Skill>(uow.SkillsRepository.Get().ToList());
            var idToRemove = expectedList.Last().Id;
            expectedList.RemoveAll(x => x.Id == idToRemove);

            // Act
            var result = controller.DeleteSkill(idToRemove) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteValues["action"], nameof(IntroductionController.ShowSkills));
            AssertExtension.CompareIEnumerable(expectedList, uow.SkillsRepository.Get().ToList(),
                (x, y) => x.Id == y.Id &&
                          AssertExtension.CompareIEnumerable(x.Texts, y.Texts,
                              (a, b) => a.Language == b.Language && a.Value == b.Value)
            );
        }

        [Test]
        public void DeleteWorkTest()
        {
            // Arrange
            var uow = new UnitOfWorkFakeFactory().Uow.Object;
            var controller = new IntroductionController(uow);

            var expectedList = new List<Work>(uow.WorksRepository.Get().ToList());
            var idToRemove = expectedList.Last().Id;
            expectedList.RemoveAll(x => x.Id == idToRemove);

            // Act
            var result = controller.DeleteWork(idToRemove) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteValues["action"], nameof(IntroductionController.ShowJobs));
            AssertExtension.CompareIEnumerable(expectedList, uow.WorksRepository.Get().ToList(),
                (x, y)
                    => x.Id == y.Id
                       && x.DisplayPriority == y.DisplayPriority
                       && AssertExtension.CompareIEnumerable(x.Texts, y.Texts,
                           (a, b) => a.Language == b.Language && a.Value == b.Value)
            );
        }

        [Test]
        public void AddWork_Get_Test()
        {
            // Act
            var uow = new UnitOfWorkFakeFactory().Uow.Object;
            var controller = new IntroductionController(uow);
            const int workCategoryId = 1;
            var expected = new AddWorkModel()
            {
                JobId = workCategoryId,
                Texts = uow.LanguagesRepository.Get().Select(language => new TextModel() { Language = language }).ToList()
            };

            var result = controller.AddWork(workCategoryId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            AssertExtension.PropertyValuesAreEquals(expected, result.Model);
        }

        [Test]
        public void AddWork_Post_Test()
        {
            // Act
            var uow = new UnitOfWorkFakeFactory().Uow.Object;
            const int jobId = 1;
            var paramUser = new AddWorkModel()
            {
                JobId = jobId,
                Texts = new List<TextModel>()
                {
                    new TextModel() {Language = uow.LanguagesRepository.Get().ToList()[0], Value = "NewJob.fr"},
                    new TextModel() {Language = uow.LanguagesRepository.Get().ToList()[1], Value = "NewJob.en"}
                }
            };

            var expected = paramUser.ToDto(uow.JobsRepository.Get().ToList(),
                uow.LanguagesRepository.Get().ToList());

            expected.Id = uow.WorksRepository.Get().Count() + 1;

            var nbWorks = uow.WorksRepository.Get().ToList().Count;
            var result = new IntroductionController(uow).AddWork(paramUser) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteValues["Action"], nameof(IntroductionController.ShowJobs));
            Assert.AreEqual(nbWorks + 1, uow.WorksRepository.Get().Count());
            AssertExtension.PropertyValuesAreEquals(expected, uow.WorksRepository.Get().Last());
        }

        [Test]
        public void ShowJobsTest()
        {
            // Act
            var uow = new UnitOfWorkFakeFactory().Uow.Object;
            var controller = new IntroductionController(uow);

            var result = controller.ShowJobs() as ViewResult;

            var expected = new JobsModel() { Jobs = uow.JobsRepository.Get().ToList() };

            // Assert
            Assert.IsNotNull(result);
            AssertExtension.PropertyValuesAreEquals(expected, result.Model);
        }

        [Test]
        public void DeleteJobTest()
        {
            // Arrange
            var uow = new UnitOfWorkFakeFactory().Uow.Object;
            var controller = new IntroductionController(uow);

            var expectedList = new List<Job>(uow.JobsRepository.Get().ToList());
            var idToRemove = expectedList.Last().Id;
            expectedList.RemoveAll(x => x.Id == idToRemove);

            // Act
            var result = controller.DeleteJob(idToRemove) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteValues["action"], nameof(IntroductionController.ShowJobs));
            AssertExtension.CompareIEnumerable(expectedList, uow.JobsRepository.Get().ToList(),
                (x, y) => x.StarterDate == y.StarterDate
                          && x.EndDate == y.EndDate
                          && x.Id == y.Id
                          && AssertExtension.CompareIEnumerable(x.Works, y.Works, (a, b) => a.Id == b.Id)
                          &&
                          AssertExtension.CompareIEnumerable(x.Texts, y.Texts,
                              (c, d) => c.Language == d.Language && c.Value == d.Value)
            );
        }

        [Test]
        public void AddJob_Get_Test()
        {
            // Act
            var uow = new UnitOfWorkFakeFactory().Uow.Object;
            var controller = new IntroductionController(uow);
            var expected = new JobModel()
            {
                Texts = uow.LanguagesRepository.Get().Select(language => new TextModel() { Language = language }).ToList(),
            };

            var result = controller.AddJob() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            AssertExtension.PropertyValuesAreEquals(expected, result.Model);
        }

        [Test]
        public void AddJob_Post_Test()
        {
            // Act
            var uow = new UnitOfWorkFakeFactory().Uow.Object;
            var paramUser = new JobModel()
            {
                StarterDate = new DateTime(2000, 1, 15),
                EndDate = new DateTime(2001, 1, 16),
                IsNotActualJob = true,
                Texts = new List<TextModel>()
                {
                    new TextModel() {Language = uow.LanguagesRepository.Get().ToList()[0], Value = "NewSkillCat.fr"},
                    new TextModel() {Language = uow.LanguagesRepository.Get().ToList()[1], Value = "NewSkillCat.en"}
                }
            };
            var expected = paramUser.ToDto(uow.LanguagesRepository.Get().ToList());
            expected.Id = uow.SkillCategoriesRepository.Get().Count() + 1;

            var nbJobs = uow.JobsRepository.Get().ToList().Count;
            var result = new IntroductionController(uow).AddJob(paramUser) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteValues["Action"], nameof(IntroductionController.ShowJobs));
            Assert.AreEqual(nbJobs + 1, uow.JobsRepository.Get().Count());
            AssertExtension.PropertyValuesAreEquals(expected, uow.JobsRepository.Get().Last());
        }

        [Test]
        public void ShowHobbiesTest()
        {
            // Act
            var uow = new UnitOfWorkFakeFactory().Uow.Object;
            var controller = new IntroductionController(uow);

            var result = controller.ShowHobbies() as ViewResult;

            var expected = new HobbiesModel() { Hobbies = uow.HobbiesRepository.Get().ToList() };

            // Assert
            Assert.IsNotNull(result);
            AssertExtension.PropertyValuesAreEquals(expected, result.Model);
        }

        [Test]
        public void DeleteHobbyTest()
        {
            // Arrange
            var uow = new UnitOfWorkFakeFactory().Uow.Object;
            var controller = new IntroductionController(uow);

            var expectedList = new List<Hobby>(uow.HobbiesRepository.Get().ToList());
            var idToRemove = expectedList.Last().Id;
            expectedList.RemoveAll(x => x.Id == idToRemove);

            // Act
            var result = controller.DeleteHobby(idToRemove) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteValues["action"], nameof(IntroductionController.ShowHobbies));
            AssertExtension.CompareIEnumerable(expectedList, uow.HobbiesRepository.Get().ToList(),
                (x, y)
                    => x.Id == y.Id
                       && x.Content == y.Content
                       &&
                       AssertExtension.CompareIEnumerable(x.Texts, y.Texts,
                           (a, b) => a.Language == b.Language && a.Value == b.Value)
            );
        }

        [Test]
        public void AddHobby_Get_Test()
        {
            // Act
            var uow = new UnitOfWorkFakeFactory().Uow.Object;
            var controller = new IntroductionController(uow);

            var result = controller.AddHobby() as ViewResult;

            var expected = new HobbyModel()
            {
                Texts = uow.LanguagesRepository.Get().ToList().Select(l => new TextModel() { Language = l }).ToList()
            };

            // Assert
            Assert.IsNotNull(result);
            AssertExtension.PropertyValuesAreEquals(expected, result.Model);
        }

        [Test]
        public void AddHobby_Post_Test()
        {
            // Act
            var uow = new UnitOfWorkFakeFactory().Uow.Object;
            var image = TestResources.TestsResources.test;
            
            var expectedStream = new MemoryStream();
            image.Save(expectedStream, ImageFormat.Jpeg);
            var expectedFile = new Mock<HttpPostedFileBase>();
            expectedFile.Setup(x => x.InputStream).Returns(expectedStream);

            var paramStream = new MemoryStream();
            image.Save(paramStream, ImageFormat.Jpeg);
            var paramFile = new Mock<HttpPostedFileBase>();
            paramFile.Setup(x => x.InputStream).Returns(paramStream);

            var paramUser = new HobbyModel()
            {
                Picture = paramFile.Object,
                Texts = new List<TextModel>()
                {
                    new TextModel() {Language = uow.LanguagesRepository.Get().ToList()[0], Value = "Graduation.fr"},
                    new TextModel() {Language = uow.LanguagesRepository.Get().ToList()[1], Value = "Graduation.en"}
                }
            };
            var expected = new HobbyModel()
            {
                Picture = expectedFile.Object,
                Texts = new List<TextModel>()
                {
                    new TextModel() {Language = uow.LanguagesRepository.Get().ToList()[0], Value = "Graduation.fr"},
                    new TextModel() {Language = uow.LanguagesRepository.Get().ToList()[1], Value = "Graduation.en"}
                }
            }.ToDto(uow.LanguagesRepository.Get().ToList());
            expected.Id = uow.HobbiesRepository.Get().Count() + 1;

            var nbHobbies = uow.HobbiesRepository.Get().ToList().Count;
            var result = new IntroductionController(uow).AddHobby(paramUser) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteValues["Action"], nameof(IntroductionController.ShowHobbies));
            Assert.AreEqual(nbHobbies + 1, uow.HobbiesRepository.Get().Count());
            AssertExtension.PropertyValuesAreEquals(expected, uow.HobbiesRepository.Get().Last());
        }

        [Test]
        public void GetHobbyImageTest()
        {
            // Act
            var uow = new UnitOfWorkFakeFactory().Uow.Object;
            var controller = new IntroductionController(uow);

            const int imgId = 1;
            var result = controller.GetHobbyImage(imgId);
            var expected = new ImageResult(uow.HobbiesRepository.Find(imgId).Content, "image/jpeg");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }
    }
}
