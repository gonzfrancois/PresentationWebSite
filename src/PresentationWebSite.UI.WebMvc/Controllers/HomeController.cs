using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using PresentationWebSite.Dal.UnitOfWorks;
using PresentationWebSite.Dal.UnitOfWorks.Base;
using PresentationWebSite.UI.WebMvc.Helpers.Extensions;
using PresentationWebSite.UI.WebMvc.Models.Home;

namespace PresentationWebSite.UI.WebMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBusinessUnitOfWork _uow;

        public HomeController()
        {
            _uow = new BusinessUnitOfWork(ConfigurationManager.ConnectionStrings["PresentationWebSite"].ToString());
        }

        public HomeController(IBusinessUnitOfWork businessUnitOfWork)
        {
            _uow = businessUnitOfWork;
        }

        public ActionResult Index()
        {
            var model = _uow.UsersRepository.Get().FirstOrDefault().ToDto(_uow.LanguagesRepository.Get().ToList());
            return View(model);
        }

        public ActionResult Introduction()
        {
            return View();
        }

        public ActionResult Contact()
        {
            var model = _uow.UsersRepository.Get().FirstOrDefault().ToDto(_uow.LanguagesRepository.Get().ToList());//_uow.LanguagesRepository.Get().Select(language => new TextModel() { Language = language }).ToList());
            return View(model);
        }

        [HttpPost]
        public ActionResult Contact(dynamic model)
        {
            if (model.success)
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("EditApplicationUser")]
        public ActionResult EditApplicationUser()
        {
            var model = _uow.UsersRepository.Get().FirstOrDefault().ToDto(_uow.LanguagesRepository.Get().ToList());
            model.IsEditMode = true;
            return View(nameof(Index),model);
        }

        [HttpPost]
        public ActionResult EditApplicationUser(ApplicationUserModel model)
        {
            var result = model.ToDto(_uow.LanguagesRepository.Get().ToList());
            var original = _uow.UsersRepository.Get().FirstOrDefault(x => x.Id == model.Id);
            if (original != null)
            {
                foreach (var text in original.DisplayWork.ToList())
                    _uow.TextsRepository.Delete(text);

                foreach (var text in original.ApplicationUserPresentations.ToList())
                    _uow.TextsRepository.Delete(text);

                foreach (var text in original.PresentationTitleTexts.ToList())
                    _uow.TextsRepository.Delete(text);

                foreach (var text in original.PresentationSubTitleTexts.ToList())
                    _uow.TextsRepository.Delete(text);

                _uow.UsersRepository.Delete(original);
            }
            
            _uow.UsersRepository.Insert(result);
            _uow.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}