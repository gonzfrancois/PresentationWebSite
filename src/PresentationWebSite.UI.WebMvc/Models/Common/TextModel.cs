using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PresentationWebSite.Dal.Model;

namespace PresentationWebSite.UI.WebMvc.Models.Common
{
    public class TextModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Value { get; set; }

        public virtual Language Language { get; set; }
    }
}
