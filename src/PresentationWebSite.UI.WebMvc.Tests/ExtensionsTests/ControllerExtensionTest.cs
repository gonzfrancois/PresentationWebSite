using System;
using Moq;
using NUnit.Framework;
using PresentationWebSite.Dal.UnitOfWorks.Base;
using PresentationWebSite.UI.WebMvc.Controllers.CustomActionResult;

namespace PresentationWebSite.UI.WebMvc.Tests.ExtensionsTests
{
    [TestFixture()]
    public class ControllerExtensionTest
    {
        public Mock<IBusinessUnitOfWork> Uow => new Mock<IBusinessUnitOfWork>();

        [Test]
        public void ConstructorTest()
        {
            const string contentType = "image/jpeg";
            var imageBuffer = new byte[5];
            Assert.Throws<ArgumentNullException>(() => new ImageResult(null, contentType));
            Assert.Throws<ArgumentNullException>(() => new ImageResult(imageBuffer, null));
            Assert.Throws<ArgumentNullException>(() => new ImageResult(imageBuffer, contentType).ExecuteResult(null));
            Assert.DoesNotThrow(()=>new ImageResult(imageBuffer, contentType));
        }

        [Test]
        public void EqualsTest()
        {
            var buffer1 = new byte[5];
            var buffer2 = new byte[1];
            const string cType1 = "image/jpeg";
            const string cType2 = "image/png";

            Assert.AreNotEqual(new ImageResult(buffer1,cType1), new ImageResult(buffer2,cType2));
            Assert.AreNotEqual(new ImageResult(buffer1,cType2), new ImageResult(buffer2,cType2));
            Assert.AreNotEqual(new ImageResult(buffer2,cType1), new ImageResult(buffer2,cType2));
            Assert.AreEqual(new ImageResult(buffer1,cType1), new ImageResult(buffer1,cType1));
        }
    }
}
