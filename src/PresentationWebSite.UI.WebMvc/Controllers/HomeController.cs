using System.Linq;
using System.Web.Mvc;
using PresentationWebSite.Dal;
using PresentationWebSite.UI.WebMvc.Helpers.Extensions;
using PresentationWebSite.UI.WebMvc.Models.Common;
using PresentationWebSite.UI.WebMvc.Models.Home;

namespace PresentationWebSite.UI.WebMvc.Controllers
{
    public class HomeController : Controller
    {
        private PresentationDbContext _db = new PresentationDbContext();

        public ActionResult Index()
        {
            var model = _db.Users.FirstOrDefault().ToDto(_db.Languages.Select(language => new TextModel() { Language = language }).ToList());
            return View(model);
        }

        public ActionResult Introduction()
        {
            return View();
        }

        public ActionResult Contact()
        {
            var model = _db.Users.FirstOrDefault().ToDto(_db.Languages.Select(language => new TextModel() { Language = language }).ToList());
            return View(model);
        }

        [HttpGet]
        [ActionName("EditApplicationUser")]
        public ActionResult EditApplicationUser()
        {
            var model = _db.Users.FirstOrDefault().ToDto(_db.Languages.Select(language => new TextModel() { Language = language }).ToList());
            model.IsEditMode = true;
            return View(nameof(Index),model);
        }

        [HttpPost]
        public ActionResult EditApplicationUser(ApplicationUserModel model)
        {
            var result = model.ToDto(ref _db);
            var original = _db.Users.FirstOrDefault(x => x.Id == model.Id);
            if (original != null)
            {
                _db.Texts.RemoveRange(original.DisplayWork);
                _db.Texts.RemoveRange(original.ApplicationUserPresentations);
                _db.Texts.RemoveRange(original.PresentationTitleTexts);
                _db.Texts.RemoveRange(original.PresentationSubTitleTexts);
                _db.Users.Remove(original);
            }
            
            _db.Users.Add(result);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}