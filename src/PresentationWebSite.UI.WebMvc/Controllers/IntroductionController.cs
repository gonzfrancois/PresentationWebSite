using System.Linq;
using System.Web.Mvc;
using PresentationWebSite.Dal;
using PresentationWebSite.UI.WebMvc.Models.Introduction;

namespace PresentationWebSite.UI.WebMvc.Controllers
{
    public class IntroductionController : Controller
    {
        private readonly PresentationDbContext _db = new PresentationDbContext();
        public ActionResult ShowGraduations()
        {
            var model = new GraduationsModel {Graduations = _db.Grades.ToList()};
            return View(model);
        }

    }

}