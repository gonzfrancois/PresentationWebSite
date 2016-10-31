using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;
using PresentationWebSite.Dal.Model;
using PresentationWebSite.UI.WebMvc.Controllers;
using PresentationWebSite.UI.WebMvc.Helpers.Extensions;
using PresentationWebSite.UI.WebMvc.Models.Common;
using PresentationWebSite.UI.WebMvc.Models.Introduction;
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

            var expected = new GraduationsModel {Graduations = uow.GradesRepository.Get().ToList()};

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
            expectedList.RemoveAll(x=>x.Id==idToRemove);

            // Act
            var expected = new GraduationsModel { Graduations = expectedList};
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
                Texts = uow.LanguagesRepository.Get().ToList().Select(l => new TextModel() {Language = l}).ToList()
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
                ObtainingDateTime = new DateTime(2010,9,1),
                Texts = new List<TextModel>()
                {
                    new TextModel() {Language = uow.LanguagesRepository.Get().ToList()[0], Value = "Graduation.fr"},
                    new TextModel() {Language = uow.LanguagesRepository.Get().ToList()[1], Value = "Graduation.en"}
                }
            };
            var expected = paramUser.ToDto(uow.LanguagesRepository.Get().ToList());
            expected.Id = uow.GradesRepository.Get().Count()+1;

            var nbGrades = uow.GradesRepository.Get().ToList().Count;
            var result = new IntroductionController(uow).AddGraduation(paramUser) as RedirectToRouteResult;
            
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteValues["Action"], nameof(IntroductionController.ShowGraduations));
            Assert.AreEqual(nbGrades+1, uow.GradesRepository.Get().Count());
            AssertExtension.PropertyValuesAreEquals(expected, uow.GradesRepository.Get().Last() );
        }
    }
}
