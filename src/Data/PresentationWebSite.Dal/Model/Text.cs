using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationWebSite.Dal.Model
{
    public class Text
    {
        public int Id { get; set; }
        public int TextKey { get; set; }
        public string Value { get; set; }

        public virtual Language Language { get; set; }
        public virtual ICollection<Grade> Graduations { get; set; }
    }
}
