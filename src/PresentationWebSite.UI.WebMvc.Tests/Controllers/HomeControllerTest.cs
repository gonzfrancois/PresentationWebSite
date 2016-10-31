using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;
using PresentationWebSite.UI.WebMvc.Controllers;
using PresentationWebSite.UI.WebMvc.Helpers.Extensions;
using PresentationWebSite.UI.WebMvc.Models.Common;
using PresentationWebSite.UI.WebMvc.Models.Home;
using PresentationWebSite.UI.WebMvc.Tests.Helpers;

namespace PresentationWebSite.UI.WebMvc.Tests.Controllers
{

    [TestFixture()]
    public class HomeControllerTest
    {
        [Test]
        public void IndexTest()
        {
            // Act
            var uow = new UnitOfWorkFakeFactory().Uow;
            var result = new HomeController(uow.Object).Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            AssertExtension.PropertyValuesAreEquals(
                result.Model,
                uow.Object.UsersRepository.Get().First().ToDto(uow.Object.LanguagesRepository.Get().ToList())
                );
        }

        [Test]
        public void IntroductionTest()
        {
            // Act
            var uow = new UnitOfWorkFakeFactory().Uow;
            var result = new HomeController(uow.Object).Introduction() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ContactTest()
        {
            // Act
            var uow = new UnitOfWorkFakeFactory().Uow;
            var result = new HomeController(uow.Object).Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            AssertExtension.PropertyValuesAreEquals(
                result.Model,
                uow.Object.UsersRepository.Get().FirstOrDefault().ToDto(uow.Object.LanguagesRepository.Get().ToList())
            );
        }

        [Test]
        public void EditApplicationUser_Get_Test()
        {
            // Act
            var uow = new UnitOfWorkFakeFactory().Uow;
            var result = new HomeController(uow.Object).EditApplicationUser() as ViewResult;
            var expected = uow.Object.UsersRepository.Get().FirstOrDefault().ToDto(uow.Object.LanguagesRepository.Get().ToList());
            expected.IsEditMode = true;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.ViewName, "Index");
            AssertExtension.PropertyValuesAreEquals(result.Model, expected);
        }

        [Test]
        public void EditApplicationUser_Post_Test()
        {
            // Act
            var uow = new UnitOfWorkFakeFactory().Uow;
            var usersCount = uow.Object.UsersRepository.Get().Count();
            var paramUser = new ApplicationUserModel
            {
                Id = 1,
                City = "Prades",
                DateOfBirth = new DateTime(1990, 9, 21),
                Email = "myaddress@email.com",
                FamilyName = "DURANT",
                FirstName = "Jacques",
                LinkedInUrl = "LinkedInUrl",
                TwitterName = "",
                DisplayWork = new List<TextModel>()
                {
                    new TextModel() {Language = uow.Object.LanguagesRepository.Get().ToList()[0], Value = "NewJob.fr"},
                    new TextModel() {Language = uow.Object.LanguagesRepository.Get().ToList()[1], Value = "NewJob.en"}
                },
                PresentationTexts = new List<TextModel>()
                {
                    new TextModel() {Language = uow.Object.LanguagesRepository.Get().ToList()[0], Value = "NewPresentation.fr"},
                    new TextModel() {Language = uow.Object.LanguagesRepository.Get().ToList()[1], Value = "NewPresentaiton.en"}
                },
                PresentationSubTitleTexts = new List<TextModel>()
                {
                    new TextModel() {Language = uow.Object.LanguagesRepository.Get().ToList()[0], Value = "NewPresentationSubTitle.fr"},
                    new TextModel() {Language = uow.Object.LanguagesRepository.Get().ToList()[1], Value = "NewPresentationSubTitle.en"}
                },
                PresentationTitleTexts = new List<TextModel>()
                {
                    new TextModel() {Language = uow.Object.LanguagesRepository.Get().ToList()[0], Value = "NewPresentationTitle.fr"},
                    new TextModel() {Language = uow.Object.LanguagesRepository.Get().ToList()[1], Value = "NewPresentationTitle.en"}
                }
            };

            var result = new HomeController(uow.Object).EditApplicationUser(paramUser) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteValues["Action"], nameof(HomeController.Index));

            Assert.AreEqual(usersCount, uow.Object.UsersRepository.Get().Count());
            var expected = paramUser.ToDto(uow.Object.LanguagesRepository.Get().ToList());
            var resultModel = uow.Object.UsersRepository.Get().First();

            AssertExtension.PropertyValuesAreEquals(resultModel, expected);
        }
    }
}
