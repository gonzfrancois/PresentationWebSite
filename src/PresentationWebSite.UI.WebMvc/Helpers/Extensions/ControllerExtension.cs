using System.IO;
using System.Web.Mvc;
using PresentationWebSite.UI.WebMvc.Controllers.CustomActionResult;

namespace PresentationWebSite.UI.WebMvc.Helpers.Extensions
{
    public static class ControllerExtensions
    {
        public static ImageResult Image(this Controller controller, byte[] imageBytes, string contentType)
        {
            return new ImageResult(new MemoryStream(imageBytes), contentType);
        }
    }
}