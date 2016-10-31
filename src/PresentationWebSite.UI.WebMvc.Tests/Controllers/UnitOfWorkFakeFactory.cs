using System;
using System.Collections.Generic;
using Moq;
using PresentationWebSite.Dal.Model;
using PresentationWebSite.Dal.UnitOfWorks.Base;
using PresentationWebSite.UI.WebMvc.Tests.Data;

namespace PresentationWebSite.UI.WebMvc.Tests.Controllers
{
    public class UnitOfWorkFakeFactory
    {
        private Mock<IBusinessUnitOfWork> _uow;

        public Mock<IBusinessUnitOfWork> Uow
        {
            get
            {
                if (_uow != null) return _uow;
                _uow = new Mock<IBusinessUnitOfWork>();
                _uow.Setup(u => u.LanguagesRepository).Returns(MockRepositoryFactory<Language>.Create(_languages).Object);
                _uow.Setup(u => u.TextsRepository).Returns(MockRepositoryFactory<Text>.Create(_texts).Object);
                _uow.Setup(u => u.GradesRepository).Returns(MockRepositoryFactory<Grade>.Create(Grades).Object);
                _uow.Setup(u => u.HobbiesRepository).Returns(MockRepositoryFactory<Hobby>.Create(_hobbies).Object);
                _uow.Setup(u => u.JobsRepository).Returns(MockRepositoryFactory<Job>.Create(_jobs).Object);
                _uow.Setup(u => u.SkillCategoriesRepository).Returns(MockRepositoryFactory<SkillCategory>.Create(_skillCategories).Object);
                _uow.Setup(u => u.SkillsRepository).Returns(MockRepositoryFactory<Skill>.Create(_skills).Object);
                _uow.Setup(u => u.UsersRepository).Returns(MockRepositoryFactory<ApplicationUser>.Create(_users).Object);
                _uow.Setup(u => u.WorksRepository).Returns(MockRepositoryFactory<Work>.Create(_works).Object);
                return _uow;
            }
        }

        private readonly List<Language> _languages = new List<Language>()
            {
                new Language() { Id = 1, CultureIsoCode = "fr-FR"},
                new Language() { Id = 2, CultureIsoCode = "en-US"}
            };


        private readonly List<Text> _texts = new List<Text>()
            {
                new Text() {Id = 1, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "DisplayWork1Fr"},
                new Text() {Id = 2, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "DisplayWork1En"},
                new Text() {Id = 3, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "PresentationTitleFr"},
                new Text() {Id = 4, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "PresentationTitleEn"},
                new Text() {Id = 5, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "PresentationSubTitleFr"},
                new Text() {Id = 6, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "PresentationSubTitleEn"},
                new Text() {Id = 7, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "PresentationFr"},
                new Text() {Id = 8, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "PresentationEn"},
                new Text() {Id = 9, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "grade1.fr"},
                new Text() {Id = 10, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "grade1.en"},
                new Text() {Id = 11, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "grade2.fr"},
                new Text() {Id = 12, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "grade2.en"}
            };

        private readonly List<ApplicationUser> _users = new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    Id = 1,
                    DisplayWork = new List<Text>()
                    {
                        new Text() {Id = 1, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "DisplayWork1Fr"},
                        new Text() {Id = 2, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "DisplayWork1En"}
                    },
                    PresentationTitleTexts = new List<Text>()
                    {
                        new Text() {Id = 3, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "PresentationTitleFr"},
                        new Text() {Id = 4, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "PresentationTitleEn"}
                    },
                    PresentationSubTitleTexts = new List<Text>()
                    {
                        new Text() {Id = 5, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "PresentationSubTitleFr"},
                        new Text() {Id = 6, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "PresentationSubTitleEn"}
                    },
                    ApplicationUserPresentations = new List<Text>()
                    {
                        new Text() {Id = 7, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "PresentationFr"},
                        new Text() {Id = 8, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "PresentationEn"}
                    },
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

        private List<Grade> _grd;
        private List<Grade> Grades => _grd ?? (_grd= new List<Grade>()
        {
            new Grade()
            {
                Id = 1,
                ObtainingDateTime = new DateTime(2010,1,15),
                Texts = new List<Text>()
                        {
                            new Text() {Id = 9, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "grade1.fr"},
                            new Text() {Id = 10, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "grade1.en"}
                        }
            },
            new Grade()
            {
                Id = 2,
                ObtainingDateTime = new DateTime(2010,1,15),
                Texts = new List<Text>()
                        {
                            new Text() {Id = 11, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "grade2.fr"},
                            new Text() {Id = 12, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "grade2.en"}
                        }
            }
        });

        private List<Work> _works;

        private List<Hobby> _hobbies;
        private List<Job> _jobs;
        private List<SkillCategory> _skillCategories;
        private List<Skill> _skills;
    }
}
