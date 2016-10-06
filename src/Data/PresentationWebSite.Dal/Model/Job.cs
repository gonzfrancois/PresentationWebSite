using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationWebSite.Dal.Model
{
    public class Job
    {
        public int Id { get; set; }
        public DateTime StarterDate { get; set; }
        public DateTime? EndDate { get; set; }
        public virtual ICollection<Text> Texts { get; set; }
        public virtual ICollection<Work> Works { get; set; }
    }
}
