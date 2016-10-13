using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using PresentationWebSite.UI.WebMvc.Models.Common;

namespace PresentationWebSite.UI.WebMvc.Models.Home
{
    public class ApplicationUserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string LinkedInUrl { get; set; }
        public string TwitterName { get; set; }

        public HttpPostedFileBase ProfilePicture { get; set; }

        public IList<TextModel> DisplayWork { get; set; }
        public IList<TextModel> PresentationTexts { get; set; }
        public IList<TextModel> PresentationSubTitleTexts { get; set; }
        public IList<TextModel> PresentationTitleTexts { get; set; }

        public bool IsEditMode { get; set; }
    }
}
