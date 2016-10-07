﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationWebSite.Dal.Model
{
    public class Hobby
    {
        public int Id { get; set; }
        public byte[] Content { get; set; }

        public virtual ICollection<Text> Texts { get; set; }
    }
}
