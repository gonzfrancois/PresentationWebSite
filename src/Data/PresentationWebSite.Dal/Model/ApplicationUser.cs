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
        public DateTime DateOfBirth { get; set; }
        
        public virtual ICollection<Text> DisplayWork { get; set; }
        public virtual ICollection<Text> PresentationTexts { get; set; }

    }
}
