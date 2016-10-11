using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationWebSite.Dal.Model
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string ZipCode  { get; set; }

        public ICollection<Text> DisplayWork { get; set; }
        public ICollection<PresentationText> PresentationTexts { get; set; }

    }

    

    public class PresentationText
    {
        public int Id { get; set; }
        public ICollection<Text> Texts { get; set; }
    }
}
