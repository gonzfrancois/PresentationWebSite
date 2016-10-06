using System;
using System.Collections.Generic;

namespace PresentationWebSite.Dal.Model
{
    public class Grade
    {
        public int Id { get; set; }
        public DateTime ObtainingDateTime { get; set; }

        public virtual ICollection<Text> Texts { get; set; }
    }
}
