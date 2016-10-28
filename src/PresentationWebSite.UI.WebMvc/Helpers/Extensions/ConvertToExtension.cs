using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using PresentationWebSite.Dal.Model;
using PresentationWebSite.UI.WebMvc.Models.Common;
using PresentationWebSite.UI.WebMvc.Models.Home;
using PresentationWebSite.UI.WebMvc.Models.Introduction;

namespace PresentationWebSite.UI.WebMvc.Helpers.Extensions
{
    public static class ConvertToExtension
    {

        public static ICollection<Text> ToDto(this IEnumerable<TextModel> self, List<Language> languages)
        {
            return self.Select(t => new Text()
                               {
                                   Language = languages.First(x => x.Id == t.Language.Id),
                                   Value = t.Value
                               }).ToList();
        }

        public static Grade ToDto(this GraduationModel model, List<Language> languages)
        {
            var newGrade = new Grade
            {
                Texts = model.Texts.ToDto(languages),
                ObtainingDateTime = model.ObtainingDateTime
            };
            return newGrade;
        }

        public static SkillCategory ToDto(this AddSkillCategoryModel model, List<Language> languages)
        {
            var newCategory = new SkillCategory
            {
                Texts = model.Texts.ToDto(languages),
                DisplayPriority = model.DisplayPriority,
            };
            return newCategory;
        }

        public static Skill ToDto(this AddSkillModel model, List<SkillCategory> skillCategories, List<Language> languages)
        {
            var dto = new Skill
            {
                Category = skillCategories.First(x=>x.Id == model.CategoryId),
                KnowledgePercent = model.KnowledgePercent,
                Texts = model.Texts.ToDto(languages),
            };
            return dto;
        }

        public static Work ToDto(this AddWorkModel model, List<Job> jobs, List<Language> languages)
        {
            var dto = new Work()
            {
                Job = jobs.First(x=>x.Id==model.JobId),
                DisplayPriority = model.DisplayPriority,
                Texts = model.Texts.ToDto(languages)
            };
            return dto;
        }

        public static Job ToDto(this JobModel model, List<Language> languages)
        {
            var newJob = new Job
            {
                Texts = model.Texts.ToDto(languages),
                StarterDate = model.StarterDate,
                EndDate = model.IsNotActualJob ? model.EndDate : (DateTime?)null,
            };
            return newJob;
        }

        public static Hobby ToDto(this HobbyModel model, List<Language> languages)
        {
            var newHobby = new Hobby() { Texts = model.Texts.ToDto(languages) };
            
            if (model.Picture == null) return newHobby;

            var img = new WebImage(model.Picture.InputStream);
            if (img.Width != 350 || img.Height != 200)
                img.Resize(350, 200, false);
            
            newHobby.Content = img.GetBytes();
            return newHobby;
        }

        public static ApplicationUser ToDto(this ApplicationUserModel model, List<Language> languages)
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
                DisplayWork = model.DisplayWork.ToDto(languages),
                ApplicationUserPresentations = model.PresentationTexts.ToDto(languages),
                PresentationTitleTexts = model.PresentationTitleTexts.ToDto(languages),
                PresentationSubTitleTexts = model.PresentationSubTitleTexts.ToDto(languages),
            };
            return result;
        }

        public static AddSkillCategoryModel ToDto(this SkillCategory model)
        {
            var dto = new AddSkillCategoryModel()
            {
                Id = model.Id,
                DisplayPriority = model.DisplayPriority,
                Texts = model.Texts.ToDto(),
                Skills = new List<SkillModel>(model.Skills.Select(x => x.ToDto()))
            };
            return dto;
        }

        public static SkillModel ToDto(this Skill model)
        {
            return new SkillModel()
            {
                KnowledgePercent = model.KnowledgePercent,
                Texts = model.Texts.ToDto()
            };
        }

        public static IEnumerable<TextModel> ToDto(this ICollection<Text> self)
        {
            return self.Select(text => new TextModel()
                               {
                                   Language = text.Language,
                                   Value = text.Value
                               }).ToList();
        }

        public static ApplicationUserModel ToDto(this ApplicationUser model, List<Language> languages)
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
                    DisplayWork = model.DisplayWork.ToDto().ToList(),
                    PresentationTexts = model.ApplicationUserPresentations.ToDto().ToList(),
                    PresentationSubTitleTexts = model.PresentationSubTitleTexts.ToDto().ToList(),
                    PresentationTitleTexts = model.PresentationTitleTexts.ToDto().ToList()
                };
            }
            Func<IList<TextModel>> getNewTextModelList = () => languages.Select(l => new TextModel() {Language = l, Value = string.Empty}).ToList();
            return new ApplicationUserModel()
            {
                DisplayWork = getNewTextModelList.Invoke(),
                PresentationSubTitleTexts = getNewTextModelList.Invoke(),
                PresentationTitleTexts = getNewTextModelList.Invoke(),
                PresentationTexts = getNewTextModelList.Invoke()
            };
        }
    }
}
