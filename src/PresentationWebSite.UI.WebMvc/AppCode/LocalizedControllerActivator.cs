using System;
using System.Collections;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;

namespace PresentationWebSite.UI.WebMvc.AppCode
{
    internal class LocalizedControllerActivator : IControllerActivator
    {
        private const string DefaultLanguage = "fr-FR";
        private readonly string[] _availablesLanguages = { "fr-FR", "en-GB", "es-ES", "ca-ES" };

        public IController Create(RequestContext requestContext, Type controllerType)
        {
            //Get the {language} parameter in the RouteData
            var language = requestContext.RouteData.Values["lang"]?.ToString();
            var lang = DefaultLanguage;
            if (!string.IsNullOrEmpty(language) && ((IList)_availablesLanguages).Contains(language))
                lang = language;

            if (lang == DefaultLanguage) return DependencyResolver.Current.GetService(controllerType) as IController;
            try
            {
                Thread.CurrentThread.CurrentUICulture =
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
            }
            catch (Exception)
            {
                throw new NotSupportedException($"ERROR: Invalid language code '{lang}'.");
            }

            return DependencyResolver.Current.GetService(controllerType) as IController;
        }
    }
}