using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using PresentationWebSite.Dal.Model;
using PresentationWebSite.Dal.UnitOfWorks;
using PresentationWebSite.Dal.UnitOfWorks.Base;
using PresentationWebSite.UI.WebMvc.Controllers.CustomActionResult;
using PresentationWebSite.UI.WebMvc.Helpers;
using PresentationWebSite.UI.WebMvc.Helpers.Extensions;
using PresentationWebSite.UI.WebMvc.Models.Common;
using PresentationWebSite.UI.WebMvc.Models.Introduction;
using PresentationWebSite.UI.WebMvc.Models.Introduction.SkillChart;

namespace PresentationWebSite.UI.WebMvc.Controllers
{
    public class IntroductionController : Controller
    {
        private readonly IBusinessUnitOfWork _uow;

        public IntroductionController()
        {
            _uow = new BusinessUnitOfWork(ConfigurationManager.ConnectionStrings["PresentationWebSite"].ToString());
        }

        public IntroductionController(IBusinessUnitOfWork businessUnitOfWork)
        {
            _uow = businessUnitOfWork;
        }

        #region Graduations
        public ActionResult ShowGraduations()
        {
            return View(new GraduationsModel { Graduations = _uow.GradesRepository.Get().ToList() });
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteGraduation(int gradeId)
        {
            var gradeToRemove = _uow.GradesRepository.Find(gradeId);
            if (gradeToRemove != null)
            {
                _uow.GradesRepository.Delete(gradeToRemove);
                _uow.Save();
            }
            return RedirectToAction(nameof(ShowGraduations));
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult AddGraduation()
        {
            return View(new GraduationModel()
            {
                Texts = _uow.LanguagesRepository.Get().Select(language => new TextModel() { Language = language }).ToList()
            });
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult AddGraduation(GraduationModel model)
        {
            if (ModelState.IsValid)
            {
                _uow.GradesRepository.Insert(model.ToDto(_uow.LanguagesRepository.Get().ToList()));
                _uow.Save();
                return RedirectToAction(nameof(ShowGraduations));
            }
            return RedirectToAction(nameof(AddGraduation));
        }
        #endregion

        #region SkillCategories
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteSkillCategory(int skillCategoryId)
        {
            var skillCategoryToRemove = _uow.SkillCategoriesRepository.Find(skillCategoryId);
            if (skillCategoryToRemove != null)
            {
                foreach (var text in skillCategoryToRemove.Texts.ToList())
                {
                    _uow.TextsRepository.Delete(text);
                }
                foreach (var skill in skillCategoryToRemove.Skills.ToList())
                {
                    foreach (var skillText in skill.Texts.ToList())
                    {
                        _uow.TextsRepository.Delete(skillText);
                    }
                    _uow.SkillsRepository.Delete(skill);
                }

                _uow.SkillCategoriesRepository.Delete(skillCategoryToRemove);
                _uow.Save();
            }
            return RedirectToAction(nameof(ShowSkills));
        }

        public ActionResult EditSkillCategory(int skillcategoryid)
        {
            throw new NotImplementedException();
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult AddSkillCategory()
        {
            return View(new SkillCategoryModel
            {
                Texts = _uow.LanguagesRepository.Get().Select(language => new TextModel() { Language = language }).ToList()
            });
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult AddSkillCategory(SkillCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                _uow.SkillCategoriesRepository.Insert(model.ToDto(_uow.LanguagesRepository.Get().ToList()));
                _uow.Save();
                return RedirectToAction(nameof(ShowSkills));
            }
            return RedirectToAction(nameof(AddSkillCategory));
        }
        #endregion

        #region Skills
        public ActionResult ShowSkills()
        {
            var skills = new SkillCategoriesModel
            {
                Categories = _uow.SkillCategoriesRepository.Get().ToList(),
                ChartDatas = new ChartPie()
            };

            Func<SkillCategory, Color> getColorFromPalette = sk => skills.Palette[skills.Categories.ToList().IndexOf(sk)];
            
            while (skills.Palette.Count < skills.Categories.Count)
            {
                skills.Palette.Add(skills.Palette.Last().GetNextColor());
            }

            skills.ChartDatas.Slices = new List<ChartPieSlice>();
            
            skills.ChartDatas.Slices = skills.Categories.OrderByDescending(sc => sc.Skills.Count).Select(x => new ChartPieSlice()
            {
                Label = x.Texts.GetText(CultureInfo.CurrentUICulture),
                Color = getColorFromPalette(x).GetHexValue(),
                Value = skills.Categories.Count > 0 ? x.Skills.Count : 1,
                ToolTip = x.Texts.GetText(CultureInfo.CurrentUICulture),
                Slices = new List<ChartPieSlice>(x.Skills.OrderByDescending(s => Math.Sin(s.Texts.GetText(CultureInfo.CurrentUICulture).Length)).Select(y => new ChartPieSlice()
                {
                    Label = y.Texts.GetText(CultureInfo.CurrentUICulture),
                    Color = getColorFromPalette(x).GetHexValue(),
                    Value = y.KnowledgePercent,
                    ToolTip = y.Texts.GetText(CultureInfo.CurrentUICulture) + " " + Utilities.GetGlyphiconStarsFromPercents(y.KnowledgePercent,5)
                }))
            });
            return View(skills);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult AddSkill(int skillCategoryId)
        {
            var model = new AddSkillModel
            {
                Texts = _uow.LanguagesRepository.Get().Select(language => new TextModel() { Language = language }).ToList(),
                CategoryId = skillCategoryId
            };

            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult AddSkill(AddSkillModel model)
        {
            if (ModelState.IsValid)
            {
                _uow.SkillsRepository.Insert(model.ToDto(_uow.SkillCategoriesRepository.Get().ToList(), _uow.LanguagesRepository.Get().ToList()));
                _uow.Save();
                return RedirectToAction(nameof(ShowSkills));
            }
            return RedirectToAction(nameof(AddSkill));
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult EditSkill(int skillcategoryid)
        {
            throw new NotImplementedException();
        }
        public ActionResult DeleteSkill(int skillId)
        {
            var skillToRemove = _uow.SkillsRepository.Find(skillId);
            if (skillToRemove != null)
            {
                foreach (var text in skillToRemove.Texts.ToList())
                    _uow.TextsRepository.Delete(text);

                _uow.SkillsRepository.Delete(skillToRemove);
                _uow.Save();
            }
            return RedirectToAction(nameof(ShowSkills));
        }
        #endregion

        #region Works
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteWork(int workId)
        {
            var workToRemove = _uow.WorksRepository.Find(workId);
            if (workToRemove != null)
            {
                foreach (var text in workToRemove.Texts.ToList())
                    _uow.TextsRepository.Delete(text);

                _uow.WorksRepository.Delete(workToRemove);
                _uow.Save();
            }
            return RedirectToAction(nameof(ShowSkills));
        }

        public ActionResult EditWok(int workId)
        {
            throw new NotImplementedException();
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult AddWork(int jobId)
        {
            return View(new AddWorkModel()
            {
                Texts = _uow.LanguagesRepository.Get().Select(language => new TextModel() { Language = language }).ToList(),
                JobId = jobId
            });
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult AddWork(AddWorkModel model)
        {
            if (ModelState.IsValid)
            {
                _uow.WorksRepository.Insert(model.ToDto(_uow.JobsRepository.Get().ToList(), _uow.LanguagesRepository.Get().ToList()));
                _uow.Save();
                return RedirectToAction(nameof(ShowJobs));
            }
            return RedirectToAction(nameof(AddWork));
        }

        #endregion

        #region Jobs

        public ActionResult ShowJobs()
        {
            return View(new JobsModel() { Jobs = _uow.JobsRepository.Get().ToList() });
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteJob(int jobId)
        {
            var jobToRemove = _uow.JobsRepository.Find(jobId);
            if (jobToRemove != null)
            {
                foreach (var text in jobToRemove.Texts.ToList())
                    _uow.TextsRepository.Delete(text);

                foreach (var wk in jobToRemove.Works.ToList())
                {
                    foreach (var text in wk.Texts.ToList())
                        _uow.TextsRepository.Delete(text);
                    _uow.WorksRepository.Delete(wk);
                }

                _uow.JobsRepository.Delete(jobToRemove);
                _uow.Save();
            }
            return RedirectToAction(nameof(ShowJobs));
        }

        public ActionResult EditJob(int jobId)
        {
            throw new NotImplementedException();
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult AddJob()
        {
            return View(new JobModel()
            {
                Texts = _uow.LanguagesRepository.Get().Select(language => new TextModel() { Language = language }).ToList()
            });
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult AddJob(JobModel model)
        {
            if (ModelState.IsValid)
            {
                _uow.JobsRepository.Insert(model.ToDto(_uow.LanguagesRepository.Get().ToList()));
                _uow.Save();
                return RedirectToAction(nameof(ShowJobs));
            }
            return RedirectToAction(nameof(AddJob));
        }
        #endregion

        #region Hobbies
        public ActionResult ShowHobbies()
        {
            return View(new HobbiesModel() { Hobbies = _uow.HobbiesRepository.Get().ToList() });
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteHobby(int hobbyId)
        {
            var hobbyToRemove = _uow.HobbiesRepository.Find(hobbyId);
            if (hobbyToRemove != null)
            {
                _uow.HobbiesRepository.Delete(hobbyToRemove);
                _uow.Save();
            }
            return RedirectToAction(nameof(ShowHobbies));
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult AddHobby()
        {
            return View(new HobbyModel()
            {
                Texts = _uow.LanguagesRepository.Get().Select(language => new TextModel() { Language = language }).ToList()
            });
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult AddHobby(HobbyModel model)
        {
            if (ModelState.IsValid)
            {
                _uow.HobbiesRepository.Insert(model.ToDto(_uow.LanguagesRepository.Get().ToList()));
                _uow.Save();
                return RedirectToAction(nameof(ShowHobbies));
            }
            return RedirectToAction(nameof(AddHobby));
        }

        public ImageResult GetHobbyImage(int id)
        {
            var imageBuffer = _uow.HobbiesRepository.Find(id)?.Content;
            const string contentType = "image/jpeg";
            return this.Image(imageBuffer ?? new byte[0], contentType);
        }

        #endregion
    }

}