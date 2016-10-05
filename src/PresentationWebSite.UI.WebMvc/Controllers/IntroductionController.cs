using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using PresentationWebSite.Dal;
using PresentationWebSite.Dal.Model;
using PresentationWebSite.UI.WebMvc.Helpers.Extensions;
using PresentationWebSite.UI.WebMvc.Models.Common;
using PresentationWebSite.UI.WebMvc.Models.Introduction;

namespace PresentationWebSite.UI.WebMvc.Controllers
{
    public class IntroductionController : Controller
    {
        private PresentationDbContext _db = new PresentationDbContext();
        public ActionResult ShowGraduations()
        {
            return View(new GraduationsModel { Graduations = _db.Grades.ToList() });
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
            return RedirectToAction(nameof(ShowGraduations));
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult AddGraduation()
        {
            return View(new GraduationModel()
            {
                Texts = _db.Languages.Select(language => new TextModel() { Language = language }).ToList()
            });
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult AddGraduation(GraduationModel model)
        {
            if (ModelState.IsValid)
            {
                _db.Grades.Add(model.ToDto(ref _db));
                _db.SaveChanges();
                return RedirectToAction(nameof(ShowGraduations));
            }
            return RedirectToAction(nameof(AddGraduation));
        }
        #endregion

        #region SkillCategories
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteSkillCategory(int skillCategoryId)
        {
            var skillCategoryToRemove = _db.SkillGategories.Find(skillCategoryId);
            if (skillCategoryToRemove != null)
            {
                foreach (var skill in skillCategoryToRemove.Skills)
                {
                    _db.Texts.RemoveRange(skill.Texts);
                }
                _db.Texts.RemoveRange(skillCategoryToRemove.Texts);
                _db.Skills.RemoveRange(skillCategoryToRemove.Skills);
                _db.SkillGategories.Remove(skillCategoryToRemove);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(ShowSkills));
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult AddSkillCategory()
        {
            throw new NotImplementedException();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult AddSkillCategory(object model)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Skills
        public ActionResult ShowSkills()
        {
            return View(new SkillCategoriesModel() {Categories = _db.SkillGategories.ToList()});
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteSkill(int skillId)
        {
            throw new NotImplementedException();
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult AddSkill()
        {
            throw new NotImplementedException();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult AddSkill(object model)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

}