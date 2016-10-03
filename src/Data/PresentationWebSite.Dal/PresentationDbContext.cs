using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PresentationWebSite.Dal.Model;

namespace PresentationWebSite.Dal
{
    public class PresentationDbContext : DbContext
    {
        public DbSet<Text> Texts { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Grade> Grades { get; set; }

        public PresentationDbContext() : base("PresentationWebSite")
        {
            Database.SetInitializer(new PresentationDbInitializer());
        }
    }
}
