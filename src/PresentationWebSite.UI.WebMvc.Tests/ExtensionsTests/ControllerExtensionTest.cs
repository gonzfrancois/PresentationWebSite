using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Moq;
using NUnit.Framework.Internal;
using NUnit.Framework;
using PresentationWebSite.Dal.UnitOfWorks.Base;
using PresentationWebSite.UI.WebMvc.Controllers;
using PresentationWebSite.UI.WebMvc.Controllers.CustomActionResult;
using PresentationWebSite.UI.WebMvc.Helpers.Extensions;

namespace PresentationWebSite.UI.WebMvc.Tests.ExtensionsTests
{
    [TestFixture()]
    public class ControllerExtensionTest
    {
        public Mock<IBusinessUnitOfWork> Uow => new Mock<IBusinessUnitOfWork>();

        private class TestController : Controller { }

        [Ignore("not implemented")]
        public void Should_Return_Image_Result()
        {

        }
    }
}
