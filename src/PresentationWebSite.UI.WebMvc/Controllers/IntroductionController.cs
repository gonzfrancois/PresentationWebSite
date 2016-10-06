using System;
using System.Linq;
using System.Web.Mvc;
using PresentationWebSite.Dal;
using PresentationWebSite.UI.WebMvc.Helpers.Extensions;
using PresentationWebSite.UI.WebMvc.Models.Common;
using PresentationWebSite.UI.WebMvc.Models.Introduction;

namespace PresentationWebSite.UI.WebMvc.Controllers
{
    public class IntroductionController : Controller
    {
        private PresentationDbContext _db = new PresentationDbContext();
        
        #region Graduations
        public ActionResult ShowGraduations()
        {
            return View(new GraduationsModel { Graduations = _db.Grades.ToList() });
        }

        //[Authorize(Roles = "Administrator")]
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

        //[Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult AddGraduation()
        {
            return View(new GraduationModel()
            {
                Texts = _db.Languages.Select(language => new TextModel() { Language = language }).ToList()
            });
        }

        //[Authorize(Roles = "Administrator")]
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
        //[Authorize(Roles = "Administrator")]
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

        public ActionResult EditSkillCategory(int skillcategoryid)
        {
            throw new NotImplementedException();
        }
        
        //[Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult AddSkillCategory()
        {
            return View(new SkillCategoryModel
            {
                Texts = _db.Languages.Select(language => new TextModel() { Language = language }).ToList()
            });
        }

        //[Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult AddSkillCategory(SkillCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                _db.SkillGategories.Add(model.ToDto(ref _db));
                _db.SaveChanges();
                return RedirectToAction(nameof(ShowSkills));
            }
            return RedirectToAction(nameof(AddSkillCategory));
        }
        #endregion

        #region Skills
        public ActionResult ShowSkills()
        {
            return View(new SkillCategoriesModel() { Categories = _db.SkillGategories.ToList() });
        }

        //[Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult AddSkill(int skillCategoryId)
        {
            var model = new AddSkillModel
            {
                Texts = _db.Languages.Select(language => new TextModel() {Language = language}).ToList(),
                CategoryId = skillCategoryId
            };
            
            return View(model);
        }

        //[Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult AddSkill(AddSkillModel model)
        {
            if (ModelState.IsValid)
            {
                _db.Skills.Add(model.ToDto(ref _db));
                _db.SaveChanges();
                return RedirectToAction(nameof(ShowSkills));
            }
            return RedirectToAction(nameof(AddSkill));
        }

        //[Authorize(Roles = "Administrator")]
        public ActionResult EditSkill(int skillcategoryid)
        {
            throw new NotImplementedException();
        }
        public ActionResult DeleteSkill(int skillId)
        {
            var skillToRemove = _db.Skills.Find(skillId);
            if (skillToRemove != null)
            {
                _db.Texts.RemoveRange(skillToRemove.Texts);
                _db.Skills.Remove(skillToRemove);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(ShowSkills));
        }
        #endregion



    }

}