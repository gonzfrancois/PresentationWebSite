using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PresentationWebSite.Dal;
using PresentationWebSite.Dal.Model;
using PresentationWebSite.UI.WebMvc.Models.Introduction;

namespace PresentationWebSite.UI.WebMvc.Controllers
{
    public class IntroductionController : Controller
    {
        private readonly PresentationDbContext _db = new PresentationDbContext();
        public ActionResult ShowGraduations()
        {
            var model = new GraduationsModel { Graduations = _db.Grades.ToList() };
            return View(model);
        }

        #region Graduations

        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteGraduation(int gradeId)
        {
            var gradeToRemove = _db.Grades.FirstOrDefault(x => x.Id == gradeId);
            if (gradeToRemove != null)
            {
                _db.Grades.Remove(gradeToRemove);
                _db.SaveChanges();
            }
            return RedirectToAction("ShowGraduations");
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult AddGraduation()
        {
            var model = new GraduationModel();
            return View(model);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult AddGraduation(GraduationModel model)
        {
            if (model.Texts.Any())
            {
                var grade = new Grade() {ObtainingDateTime = model.ObtainingDateTime};
                foreach (var text in model.Texts)
                {
                    var lang = _db.Languages.FirstOrDefault(x => x.CultureIsoCode == text.Key.TwoLetterISOLanguageName);
                    var t = new Text() {Language = lang, Value = text.Value, TextKey = model.TextKey};
                    grade.Texts.Add(t);
                }
                _db.Grades.Add(grade);
                _db.SaveChanges();
            }
            return View("ShowGraduations");
        }
        #endregion

        #region Texts



        #endregion
    }

}