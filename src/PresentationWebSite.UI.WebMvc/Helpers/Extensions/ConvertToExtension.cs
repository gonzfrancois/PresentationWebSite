using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PresentationWebSite.Dal;
using PresentationWebSite.Dal.Model;
using PresentationWebSite.UI.WebMvc.Models.Common;
using PresentationWebSite.UI.WebMvc.Models.Introduction;

namespace PresentationWebSite.UI.WebMvc.Helpers.Extensions
{
    internal static class ConvertToExtension
    {
        internal static Grade ToDto(this GraduationModel model, ref PresentationDbContext context)
        {
            var newGrade = new Grade { Texts = new List<Text>() };
            foreach (var modelText in model.Texts)
            {
                newGrade.Texts.Add(new Text()
                {
                    Language = context.Languages.Find(modelText.Language.Id),
                    Value = modelText.Value
                });
            }
            newGrade.ObtainingDateTime = model.ObtainingDateTime;
            return newGrade;
        }
    }
}
