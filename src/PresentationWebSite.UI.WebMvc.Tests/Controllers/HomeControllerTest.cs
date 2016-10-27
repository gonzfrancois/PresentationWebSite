using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using PresentationWebSite.Dal.Model;
using PresentationWebSite.Dal.UnitOfWorks.Base;
using PresentationWebSite.UI.WebMvc.Controllers;
using PresentationWebSite.UI.WebMvc.Helpers.Extensions;
using PresentationWebSite.UI.WebMvc.Models.Common;
using PresentationWebSite.UI.WebMvc.Tests.Data;
using PresentationWebSite.UI.WebMvc.Tests.Helpers;

namespace PresentationWebSite.UI.WebMvc.Tests.Controllers
{

    [TestFixture()]
    public class HomeControllerTest
    {
        private Mock<IBusinessUnitOfWork> _uow;

        private Mock<IBusinessUnitOfWork> Uow
        {
            get
            {
                if (_uow != null) return _uow;
                _uow = new Mock<IBusinessUnitOfWork>();
                _uow.Setup(u => u.LanguagesRepository)
                    .Returns(MockRepositoryFactory<Language>.Create(_languages).Object);
                _uow.Setup(u => u.TextsRepository).Returns(MockRepositoryFactory<Text>.Create(_texts).Object);
                _uow.Setup(u => u.GradesRepository).Returns(MockRepositoryFactory<Grade>.Create(_grades).Object);
                _uow.Setup(u => u.HobbiesRepository).Returns(MockRepositoryFactory<Hobby>.Create(_hobbies).Object);
                _uow.Setup(u => u.JobsRepository).Returns(MockRepositoryFactory<Job>.Create(_jobs).Object);
                _uow.Setup(u => u.SkillCategoriesRepository)
                    .Returns(MockRepositoryFactory<SkillCategory>.Create(_skillCategories).Object);
                _uow.Setup(u => u.SkillsRepository).Returns(MockRepositoryFactory<Skill>.Create(_skills).Object);
                _uow.Setup(u => u.UsersRepository).Returns(MockRepositoryFactory<ApplicationUser>.Create(_users).Object);
                _uow.Setup(u => u.WorksRepository).Returns(MockRepositoryFactory<Work>.Create(_works).Object);
                return _uow;
            }
        }

        private List<Language> _languages;
        private List<Text> _texts;
        private List<Grade> _grades;
        private List<Hobby> _hobbies;
        private List<Job> _jobs;
        private List<SkillCategory> _skillCategories;
        private List<Skill> _skills;
        private List<ApplicationUser> _users;
        private List<Work> _works;


        [SetUp]
        public void Initilize()
        {
            _languages = new List<Language>()
            {
                new Language() {Id = 1, CultureIsoCode = "fr-FR"},
                new Language() {Id = 2, CultureIsoCode = "en-US"},
            };

            _texts = new List<Text>()
            {
                new Text() {Id = 1, Language = _languages[0], Value = "DisplayWork1Fr"},
                new Text() {Id = 2, Language = _languages[1], Value = "DisplayWork1En"},
                new Text() {Id = 3, Language = _languages[0], Value = "PresentationTitleFr"},
                new Text() {Id = 4, Language = _languages[1], Value = "PresentationTitleEn"},
                new Text() {Id = 5, Language = _languages[0], Value = "PresentationSubTitleFr"},
                new Text() {Id = 6, Language = _languages[1], Value = "PresentationSubTitleEn"},
                new Text() {Id = 7, Language = _languages[0], Value = "PresentationFr"},
                new Text() {Id = 8, Language = _languages[1], Value = "PresentationEn"},
            };

            _users = new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    Id = 1,
                    DisplayWork = _texts.Where(t => t.Id == 1 || t.Id == 2).ToList(),
                    PresentationTitleTexts = _texts.Where(t => t.Id == 3 || t.Id == 4).ToList(),
                    PresentationSubTitleTexts = _texts.Where(t => t.Id == 5 || t.Id == 6).ToList(),
                    ApplicationUserPresentations = _texts.Where(t => t.Id == 7 || t.Id == 8).ToList(),
                    City = "MyCity",
                    LinkedInUrl = "Htpp://linkedInUrl.com/",
                    FamilyName = "MyFamilyName",
                    TwitterName = "MyTwitterName",
                    DateOfBirth = new DateTime(2000, 1, 15),
                    FirstName = "MyFirstName",
                    PhoneNumber = "0600000000",
                    ZipCode = "66000",
                    Email = "me@email.com"
                }
            };
        }

        [Test]
        public void IndexTest()
        {
            // Arrange
            HomeController controller = new HomeController(Uow.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            AssertExtension.PropertyValuesAreEquals(
                result.Model,
                _users.First()
                    .ToDto(
                        Uow.Object.LanguagesRepository.Get()
                            .Select(language => new TextModel() {Language = language})
                            .ToList())
            );
        }

    }
}
