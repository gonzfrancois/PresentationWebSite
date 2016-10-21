using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using PresentationWebSite.Dal.Model;
using PresentationWebSite.UI.WebMvc.Models.Common;
using PresentationWebSite.UI.WebMvc.Models.Home;
using PresentationWebSite.UI.WebMvc.Models.Introduction;
using WebGrease.Css.Extensions;

namespace PresentationWebSite.UI.WebMvc.Helpers.Extensions
{
    internal static class ConvertToExtension
    {
        internal static Grade ToDto(this GraduationModel model, List<Language> languages)
        {
            var newGrade = new Grade { Texts = new List<Text>() };
            foreach (var modelText in model.Texts)
            {
                newGrade.Texts.Add(new Text()
                {
                    Language = languages.First(x=>x.Id==modelText.Language.Id),
                    Value = modelText.Value
                });
            }
            newGrade.ObtainingDateTime = model.ObtainingDateTime;
            return newGrade;
        }

        internal static SkillCategory ToDto(this SkillCategoryModel model, List<Language> languages)
        {
            var newCategory = new SkillCategory() { Texts = new List<Text>() };
            foreach (var modelText in model.Texts)
            {
                newCategory.Texts.Add(new Text()
                {
                    Language = languages.First(x=>x.Id==modelText.Language.Id),
                    Value = modelText.Value
                });
            }
            newCategory.DisplayPriority = model.DisplayPriority;
            return newCategory;
        }

        internal static Skill ToDto(this AddSkillModel model, List<SkillCategory> skillCategories, List<Language> languages)
        {

            var dto = new Skill
            {
                Category = skillCategories.First(x=>x.Id == model.CategoryId),
                KnowledgePercent = model.KnowledgePercent,
                Texts = new List<Text>()
            };
            foreach (var modelText in model.Texts)
            {
                dto.Texts.Add(new Text()
                {
                    Language = languages.First(x=>x.Id== modelText.Language.Id),
                    Value = modelText.Value
                });
            }
            return dto;
        }

        internal static Work ToDto(this AddWorkModel model, List<Job> jobs, List<Language> languages)
        {

            var dto = new Work()
            {
                Job = jobs.First(x=>x.Id==model.JobId),
                DisplayPriority = model.DisplayPriority,
                Texts = new List<Text>()
            };
            foreach (var modelText in model.Texts)
            {
                dto.Texts.Add(new Text()
                {
                    Language = languages.First(x=>x.Id==modelText.Language.Id),
                    Value = modelText.Value
                });
            }
            return dto;
        }

        internal static Job ToDto(this JobModel model, List<Language> languages)
        {
            var newJob = new Job() { Texts = new List<Text>() };
            foreach (var modelText in model.Texts)
            {
                newJob.Texts.Add(new Text()
                {
                    Language = languages.First(x=>x.Id==modelText.Language.Id),
                    Value = modelText.Value
                });
            }
            newJob.StarterDate = model.StarterDate;
            if (model.IsNotActualJob)
            {
                newJob.EndDate = model.EndDate;
            }
            return newJob;
        }

        internal static Hobby ToDto(this HobbyModel model, List<Language> languages)
        {
            var newHobby = new Hobby() { Texts = new List<Text>() };
            foreach (var modelText in model.Texts)
            {
                newHobby.Texts.Add(new Text()
                {
                    Language = languages.First(x=>x.Id==modelText.Language.Id),
                    Value = modelText.Value
                });
            }

            if (model.Picture == null) return newHobby;

            var img = new WebImage(model.Picture.InputStream);
            if (img.Width != 350 || img.Height != 200)
                img.Resize(350, 200, false);
            
            newHobby.Content = img.GetBytes();
            return newHobby;
        }

        internal static ApplicationUser ToDto(this ApplicationUserModel model, List<Language> languages)
        { 
            var result = new ApplicationUser()
            {
                Id = model.Id,
                City = model.City,
                DateOfBirth = model.DateOfBirth,
                FamilyName = model.FamilyName,
                FirstName = model.FirstName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                ZipCode = model.ZipCode,
                LinkedInUrl = model.LinkedInUrl,
                TwitterName = model.TwitterName,
                DisplayWork = new List<Text>(),
                ApplicationUserPresentations = new List<Text>(),
                PresentationTitleTexts = new List<Text>(),
                PresentationSubTitleTexts = new List<Text>()
            };
            
            model.DisplayWork.ForEach(x=> result.DisplayWork.Add(new Text() { Language = languages.First(y=> y.Id == x.Language.Id), Value = x.Value }));
            model.PresentationSubTitleTexts.ForEach(x=> result.PresentationSubTitleTexts.Add(new Text() { Language = languages.First(y=> y.Id == x.Language.Id), Value = x.Value }));
            model.PresentationTitleTexts.ForEach(x=> result.PresentationTitleTexts.Add(new Text() { Language = languages.First(y=> y.Id == x.Language.Id), Value = x.Value }));
            model.PresentationTexts.ForEach(x=> result.ApplicationUserPresentations.Add(new Text() { Language = languages.First(y=> y.Id == x.Language.Id), Value = x.Value }));
            return result;
        }

        internal static SkillCategoryModel ToDto(this SkillCategory model)
        {
            var dto = new SkillCategoryModel()
            {
                Id = model.Id,
                DisplayPriority = model.DisplayPriority,
                Texts = new List<TextModel>(model.Texts.Select(x => x.ToDto())),
                Skills = new List<SkillModel>(model.Skills.Select(x => x.ToDto()))
            };
            return dto;
        }

        internal static SkillModel ToDto(this Skill model)
        {
            return new SkillModel()
            {
                KnowledgePercent = model.KnowledgePercent,
                Texts = new List<TextModel>(model.Texts.Select(x => x.ToDto())),
            };
        }

        internal static TextModel ToDto(this Text model)
        {
            return new TextModel()
            {
                Language = model.Language,
                Value = model.Value
            };
        }

        internal static ApplicationUserModel ToDto(this ApplicationUser model, IList<TextModel> textModels)
        {
            if (model!=null)
            {
                return new ApplicationUserModel
                {
                    Id = model.Id,
                    City = model.City,
                    DateOfBirth = model.DateOfBirth,
                    FamilyName = model.FamilyName,
                    FirstName = model.FirstName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    ZipCode = model.ZipCode,
                    LinkedInUrl = model.LinkedInUrl,
                    TwitterName = model.TwitterName,
                    DisplayWork = new List<TextModel>(model.DisplayWork.Select(x => x.ToDto())),
                    PresentationTexts = new List<TextModel>(model.ApplicationUserPresentations.Select(x => x.ToDto())),
                    PresentationSubTitleTexts = new List<TextModel>(model.PresentationSubTitleTexts.Select(x => x.ToDto())),
                    PresentationTitleTexts = new List<TextModel>(model.PresentationTitleTexts.Select(x => x.ToDto()))
                };
            }
            return new ApplicationUserModel()
            {
                DisplayWork = textModels,
                PresentationSubTitleTexts = textModels,
                PresentationTitleTexts = textModels,
                PresentationTexts = textModels
            };
        }
    }
}
