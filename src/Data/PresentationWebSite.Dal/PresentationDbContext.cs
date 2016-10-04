using System.Data.Entity;
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
