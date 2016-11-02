using System;
using System.Web.Mvc;

namespace PresentationWebSite.UI.WebMvc.Controllers.CustomActionResult
{
    public class ImageResult : ActionResult, IEquatable<ImageResult>
    {
        public ImageResult(byte[] imageBuffer, string contentType)
        {
            if (imageBuffer == null)
                throw new ArgumentNullException(nameof(imageBuffer));
            if (contentType == null)
                throw new ArgumentNullException(nameof(contentType));

            ImageBuffer = imageBuffer;
            ContentType = contentType;
        }

        private byte[] ImageBuffer { get; }
        private string ContentType { get; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var response = context.HttpContext.Response;

            response.ContentType = ContentType;

            if (ImageBuffer.Length!=0)
                response.OutputStream.Write(ImageBuffer, 0, ImageBuffer.Length);
            
            response.End();
        }

        public bool Equals(ImageResult other)
        {
            return other != null && ContentType == other.ContentType && ImageBuffer == other.ImageBuffer;
        }
    }
}