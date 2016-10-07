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
            var gradeToRemove = _db.Grades.Find(gradeId);
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
                Texts = _db.Languages.Select(language => new TextModel() { Language = language }).ToList(),
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

        #region Works
        //[Authorize(Roles = "Administrator")]
        public ActionResult DeleteWork(int workId)
        {
            var workToRemove = _db.Works.Find(workId);
            if (workToRemove != null)
            {
                _db.Texts.RemoveRange(workToRemove.Texts);
                _db.Works.Remove(workToRemove);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(ShowSkills));
        }

        public ActionResult EditWok(int workId)
        {
            throw new NotImplementedException();
        }

        //[Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult AddWork(int jobId)
        {
            return View(new AddWorkModel()
            {
                Texts = _db.Languages.Select(language => new TextModel() { Language = language }).ToList(),
                JobId = jobId
            });
        }

        //[Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult AddWork(AddWorkModel model)
        {
            if (ModelState.IsValid)
            {
                _db.Works.Add(model.ToDto(ref _db));
                _db.SaveChanges();
                return RedirectToAction(nameof(ShowJobs));
            }
            return RedirectToAction(nameof(AddWork));
        }

        #endregion

        #region Jobs

        public ActionResult ShowJobs()
        {
            return View(new JobsModel() { Jobs = _db.Jobs.ToList() });
        }

        //[Authorize(Roles = "Administrator")]
        public ActionResult DeleteJob(int jobId)
        {
            var jobToRemove = _db.Jobs.Find(jobId);
            if (jobToRemove != null)
            {
                foreach (var wk in jobToRemove.Works)
                {
                    _db.Texts.RemoveRange(wk.Texts);
                }
                _db.Texts.RemoveRange(jobToRemove.Texts);
                _db.Works.RemoveRange(jobToRemove.Works);
                _db.Jobs.Remove(jobToRemove);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(ShowSkills));
        }

        public ActionResult EditJob(int jobId)
        {
            throw new NotImplementedException();
        }

        //[Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult AddJob()
        {
            return View(new JobModel()
            {
                Texts = _db.Languages.Select(language => new TextModel() { Language = language }).ToList()
            });
        }

        //[Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult AddJob(JobModel model)
        {
            if (ModelState.IsValid)
            {
                _db.Jobs.Add(model.ToDto(ref _db));
                _db.SaveChanges();
                return RedirectToAction(nameof(ShowJobs));
            }
            return RedirectToAction(nameof(AddJob));
        }
        #endregion

        #region Hobbies
        public ActionResult ShowHobbies()
        {
            return View(new HobbiesModel() { Hobbies = _db.Hobbies.ToList() });
        }

        //[Authorize(Roles = "Administrator")]
        public ActionResult DeleteHobby(int hobbyId)
        {
            var hobbyToRemove = _db.Hobbies.Find(hobbyId);
            if (hobbyToRemove != null)
            {
                _db.Hobbies.Remove(hobbyToRemove);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(ShowHobbies));
        }

        //[Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult AddHobby()
        {
            return View(new HobbyModel()
            {
                Texts = _db.Languages.Select(language => new TextModel() { Language = language }).ToList()
            });
        }

        //[Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult AddHobby(HobbyModel model)
        {
            if (ModelState.IsValid)
            {
                _db.Hobbies.Add(model.ToDto(ref _db));
                _db.SaveChanges();
                return RedirectToAction(nameof(ShowHobbies));
            }
            return RedirectToAction(nameof(AddHobby));
        }
        #endregion
    }

}