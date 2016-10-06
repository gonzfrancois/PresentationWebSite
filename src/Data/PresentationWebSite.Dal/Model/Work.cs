using System.Collections.Generic;

namespace PresentationWebSite.Dal.Model
{
    public class Work
    {
        public int Id { get; set; }
        public int DisplayPriority { get; set; }

        public virtual ICollection<Text> Texts { get; set; }
        public virtual Job Job { get; set; }
    }
}